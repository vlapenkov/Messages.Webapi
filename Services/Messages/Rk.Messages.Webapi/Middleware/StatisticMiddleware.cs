﻿using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Rk.Messages.Infrastructure.Kafka;
using RK.Messages.Shared;

namespace Rk.Messages.Webapi.Middleware;

/// <summary>
/// Промежуточное ПО для сбора информации о запросах товаров
/// </summary>
public class StatisticMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<StatisticMiddleware> _logger;
    private readonly KafkaObjectProducer<Null, ProductViewStatisticEvent> _producer;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="next">следующий RequestDelegate</param>
    /// <param name="logger"> логгер</param>
    /// <param name="producer">продюсер, для отправки сообщения в кафку</param>
    public StatisticMiddleware(RequestDelegate next, ILogger<StatisticMiddleware> logger, 
        KafkaObjectProducer<Null, ProductViewStatisticEvent> producer)
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
                var msg = new ProductViewStatisticEvent
                {
                    Created = DateTime.Now,
                    Page = context.Request.Path,
                    Production = res.RootElement.GetProperty("name").GetString(),
                    ProductionId = res.RootElement.GetProperty("id").GetInt64(),
                    CategoryId = res.RootElement.GetProperty("catalogSectionId").GetInt64(),
                    Producer = res.RootElement.GetProperty("organization").GetProperty("name").GetString() ?? "undefined",
                    UserName = userName ?? "Anonymous"
                };
                _producer.Produce(nameof(ProductViewStatisticEvent), 
                    new Message<Null, ProductViewStatisticEvent>{Value = msg},
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

    private void DeliveryReportHandler(DeliveryReport<Null, ProductViewStatisticEvent> deliveryReport)
    {
        if (deliveryReport.Status == PersistenceStatus.NotPersisted)
        {
            _logger.Log(LogLevel.Warning, $"Проблема отправки собщения: {deliveryReport.Message.Value}");
        }
    }
}