using System.Text.Json;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RK.Messages.Shared.Contracts;
using RK.Messages.Shared.Kafka;
using RK.Statistic.Interfaces;

namespace RK.Statistic.Logic
{
    public class ProductReadEventConsumer : BackgroundService, IDisposable
    {
        private readonly ILogger<ProductReadEventConsumer> _logger;
        private readonly IClickHouseConnectionFactory _factory;
        private readonly IConsumer<Null, ProductStatisticEvent> _kafkaConsumer;
        private readonly IAdminClient _adminClient;

        public ProductReadEventConsumer(ILogger<ProductReadEventConsumer> logger, 
            IClickHouseConnectionFactory factory, IConfiguration config)
        {
            _logger = logger;
            _factory = factory;

            var conf = new ConsumerConfig();
            config.GetSection("Kafka:ConsumerSettings").Bind(conf);
            var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            var serializer = new ObjectSerializerDeserializer<ProductStatisticEvent>(jsonOptions);
            _kafkaConsumer = new ConsumerBuilder<Null, ProductStatisticEvent>(conf)
                .SetValueDeserializer(serializer!).Build();


            var adminConf = new ConsumerConfig();
            config.GetSection("Kafka:AdminSettings").Bind(adminConf);
            _adminClient = new AdminClientBuilder(adminConf).Build();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return StartConsumerLoop(stoppingToken);
        }

        private async Task StartConsumerLoop(CancellationToken cancellationToken)
        {
            await CreateTopicMaybe(nameof(ProductStatisticEvent), 1, 1, _adminClient);
            await using var connection = await _factory.GetConnectionAsync();
            _kafkaConsumer.Subscribe(nameof(ProductStatisticEvent));
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _kafkaConsumer.Consume(cancellationToken);
                    var res = consumeResult.Message.Value;
                    if(res == null) continue;
                    await using var writer = await connection.CreateColumnWriterAsync($"INSERT INTO product_read VALUES", cancellationToken);
                    var columns = new object?[writer.FieldCount];

                    columns[writer.GetOrdinal("id")] = new List<Guid>(1) { Guid.NewGuid() };
                    columns[writer.GetOrdinal("page")] = new List<string?>(1) {res.Page};
                    columns[writer.GetOrdinal("production")] = new List<string?>(1) {res.Production};
                    columns[writer.GetOrdinal("category")] = new List<string?>(1) {res.Category};
                    columns[writer.GetOrdinal("producer")] = new List<string?>(1) {res.Producer};
                    columns[writer.GetOrdinal("username")] = new List<string?>(1) {res.UserName};
                    columns[writer.GetOrdinal("created")] = new List<DateTime>(1) {res.Created };
                    await writer.WriteTableAsync(columns, 1, cancellationToken);
                    await writer.EndWriteAsync(cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (ConsumeException e)
                {
                    _logger.LogError(e, $"Consume error: {e.Error.Reason}");
                    if (e.Error.IsFatal) break;
                }
                catch (Exception e)
                {
                    _logger.LogError(e, $"Unexpected error: {e}");
                    break;
                }
            }
        }

        private async Task CreateTopicMaybe(string name, int numPartitions, short replicationFactor, IAdminClient adminClient)
        {
            try
            {
                await adminClient.CreateTopicsAsync(new List<TopicSpecification> {
                    new() { Name = name, NumPartitions = numPartitions, ReplicationFactor = replicationFactor } });
            }
            catch (CreateTopicsException e)
            {
                _logger.LogError(e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists
                    ? $"Ошибка создания темы {name}: {e.Results[0].Error.Reason}"
                    : "Тема уже существует");
            }
        }

        public override void Dispose()
        {
            _kafkaConsumer.Close(); 
            _kafkaConsumer.Dispose();
            base.Dispose();
        }
    }
}