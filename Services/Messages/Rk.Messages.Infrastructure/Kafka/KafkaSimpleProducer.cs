using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Rk.Messages.Infrastructure.Kafka;

/// <summary>
/// Продюсер для отправки данных простых типов
/// </summary>
public class KafkaSimpleProducer<TK, TV>
{
    private readonly IProducer<TK, TV> _kafkaHandle;

    public KafkaSimpleProducer(KafkaClientHandle handle)
    {
        _kafkaHandle = new DependentProducerBuilder<TK, TV>(handle.Handle).Build();
    }

    /// <summary>
    ///     Асинхронно создать сообщение и предоставить информацию о
    ///     доставке через возвращенную задачу. Используйте этот метод отправки,
    ///     если вы хотите дождаться результата, прежде чем поток выполнения продолжится.
    /// <summary>
    public Task ProduceAsync(string topic, Message<TK, TV> message)
        => this._kafkaHandle.ProduceAsync(topic, message);

    /// <summary>
    ///     Асинхронно создать сообщение и предоставить информацию о доставке
    ///     через предоставленную функцию обратного вызова.Используйте этот метод для отправки,
    ///     если вы хотите чтоб поток выполнения продолжился немедленно, и обработать результат за пределами потока
    /// </summary>
    public void Produce(string topic, Message<TK, TV> message, Action<DeliveryReport<TK, TV>> deliveryHandler = null)
        => this._kafkaHandle.Produce(topic, message, deliveryHandler);

    public void Flush(TimeSpan timeout)
        => this._kafkaHandle.Flush(timeout);
}