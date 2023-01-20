using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rk.Messages.Infrastructure.Kafka;
using RK.Messages.Shared.Contracts;

namespace Rk.Messages.Webapi.Middleware;

/// <summary>
/// Промежуточное ПО для сбора информации о запросах товаров
/// </summary>
public class StatisticMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<StatisticMiddleware> _logger;
    private readonly KafkaObjectProducer<Null, ProductStatisticEvent> _producer;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="next">следующий RequestDelegate</param>
    /// <param name="logger"> логгер</param>
    /// <param name="producer">продюсер, для отправки сообщения в кафку</param>
    public StatisticMiddleware(RequestDelegate next, ILogger<StatisticMiddleware> logger, 
        KafkaObjectProducer<Null, ProductStatisticEvent> producer)
    {
        _next = next;
        _logger = logger;
        _producer = producer;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/product") && context.Request.Method == "GET")
        {
            _producer.Produce(nameof(ProductStatisticEvent), 
                new Message<Null, ProductStatisticEvent>{Value = new ProductStatisticEvent()},
                DeliveryReportHandler);
        }
        await _next(context);
    }

    private void DeliveryReportHandler(DeliveryReport<Null, ProductStatisticEvent> deliveryReport)
    {
        if (deliveryReport.Status == PersistenceStatus.NotPersisted)
        {
            _logger.Log(LogLevel.Warning, $"Проблема отправки собщения: {deliveryReport.Message.Value}");
        }
    }
}