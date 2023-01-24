using System.Text.Json;
using Confluent.Kafka;
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
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.Run(() => StartConsumerLoop(stoppingToken), stoppingToken);
        }

        private void StartConsumerLoop(CancellationToken cancellationToken)
        {
            using var connection = _factory.GetConnection();
            _kafkaConsumer.Subscribe(nameof(ProductStatisticEvent));
            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _kafkaConsumer.Consume(cancellationToken);
                    var res = consumeResult.Message.Value;
                    if(res != null) continue;
                    using var writer = connection.CreateColumnWriter($"INSERT INTO product_read VALUES");
                    var columns = new object?[writer.FieldCount];

                    columns[writer.GetOrdinal("id")] = Guid.NewGuid();
                    columns[writer.GetOrdinal("page")] = res.Page;
                    columns[writer.GetOrdinal("production")] = res.Production;
                    columns[writer.GetOrdinal("category")] = res.Category;
                    columns[writer.GetOrdinal("producer")] = res.Producer;
                    columns[writer.GetOrdinal("username")] = res.UserName;
                    columns[writer.GetOrdinal("created")] = res.Created; 
                    writer.WriteTable(columns, 1);
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

        public override void Dispose()
        {
            _kafkaConsumer.Close(); 
            _kafkaConsumer.Dispose();
            base.Dispose();
        }
    }
}