﻿using System;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace Rk.Messages.Infrastructure.Kafka;

/// <summary>
/// Класс обертка на kafka клиентом для регистрации в DI
/// </summary>
public class KafkaClientHandle : IDisposable
{
    private readonly IProducer<byte[], byte[]> _kafkaProducer;

    public KafkaClientHandle(IConfiguration config)
    {
        var conf = new ProducerConfig();
        config.GetSection("Kafka:ProducerSettings").Bind(conf);
        _kafkaProducer = new ProducerBuilder<byte[], byte[]>(conf).Build();
    }

    public Handle Handle => _kafkaProducer.Handle;

    public void Dispose()
    {
        _kafkaProducer.Flush(); 
        _kafkaProducer.Dispose();
    }
}