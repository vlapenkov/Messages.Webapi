using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rk.Messages.Infrastructure.Kafka;
using Rk.Messages.Logic.ProductsNS.Dto;
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
        if (context.Request.Path.StartsWithSegments("/api/v1/Products") && context.Request.Method == "GET")
        {
            var originalBody = context.Response.Body;
            var userName = context.User?.Identity?.Name;
            try
            {
                await using var memStream = new MemoryStream();
                context.Response.Body = memStream;

                await _next(context);

                memStream.Position = 0; 
                var res = await JsonDocument.ParseAsync(memStream); 
                memStream.Position = 0;
                await memStream.CopyToAsync(originalBody);
                var msg = new ProductStatisticEvent
                {
                    Created = DateTime.Now,
                    Page = context.Request.Path,
                    Production = res.RootElement.GetProperty("name").GetString(),
                    Category = res.RootElement.GetProperty("catalogSectionId").GetRawText(),
                    Producer = res.RootElement.GetProperty("organization").GetProperty("name").GetString() ?? "undefined",
                    UserName = userName ?? "Anonymous"
                };
                _producer.Produce(nameof(ProductStatisticEvent), 
                    new Message<Null, ProductStatisticEvent>{Value = msg},
                    DeliveryReportHandler);
                
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
        else
        {
            await _next(context);
        }
    }

    private void DeliveryReportHandler(DeliveryReport<Null, ProductStatisticEvent> deliveryReport)
    {
        if (deliveryReport.Status == PersistenceStatus.NotPersisted)
        {
            _logger.Log(LogLevel.Warning, $"Проблема отправки собщения: {deliveryReport.Message.Value}");
        }
    }
}