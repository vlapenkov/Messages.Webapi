using System.Text.Json;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RK.Messages.Shared;
using RK.Messages.Shared.Serializers;
using RK.Statistic.Interfaces.StatisticWriters;

namespace RK.Statistic.Logic.Consumers
{
    /// <summary>
    /// Консьюмер для обработки события просмотра продукта
    /// </summary>
    public class ProductReadEventConsumer : BackgroundService
    {
        private readonly ILogger<ProductReadEventConsumer> _logger;
        private readonly IProductViewStatisticWriter _writer;
        private readonly IConsumer<Null, ProductViewStatisticEvent> _kafkaConsumer;
        private readonly IAdminClient _adminClient;

        /// <inheritdoc />
        public ProductReadEventConsumer(ILogger<ProductReadEventConsumer> logger, 
            IProductViewStatisticWriter writer, IConfiguration config)
        {
            _logger = logger;
            _writer = writer;

            var conf = new ConsumerConfig();
            config.GetSection("Kafka:ConsumerSettings").Bind(conf);
            var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
            var serializer = new ObjectSerializerDeserializer<ProductViewStatisticEvent>(jsonOptions);
            _kafkaConsumer = new ConsumerBuilder<Null, ProductViewStatisticEvent>(conf)
                .SetValueDeserializer(serializer!).Build();


            var adminConf = new ConsumerConfig();
            config.GetSection("Kafka:AdminSettings").Bind(adminConf);
            _adminClient = new AdminClientBuilder(adminConf).Build();
        }

        /// <inheritdoc />
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return StartConsumerLoop(stoppingToken);
        }

        private async Task StartConsumerLoop(CancellationToken cancellationToken)
        {
            await CreateTopicMaybe(nameof(ProductViewStatisticEvent), 1, 1, _adminClient);
            _kafkaConsumer.Subscribe(nameof(ProductViewStatisticEvent));
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _kafkaConsumer.Consume(cancellationToken);
                    var res = consumeResult.Message.Value;
                    if(res == null) continue;
                    await _writer.InsertRowAsync(res, cancellationToken);
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
                if (e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists)
                {
                    _logger.LogError($"Ошибка создания темы {name}: {e.Results[0].Error.Reason}");
                }
                else
                {
                    _logger.LogInformation("Тема уже существует");
                }
                
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            _kafkaConsumer.Close(); 
            _kafkaConsumer.Dispose();
            base.Dispose();
        }
    }
}