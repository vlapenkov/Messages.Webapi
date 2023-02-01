using System;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace Rk.Messages.Interfaces;

public interface IKafkaProducer<TK, TV>
{
    Task ProduceAsync(string topic, Message<TK, TV> message);
    void Produce(string topic, Message<TK, TV> message, Action<DeliveryReport<TK, TV>> deliveryHandler = null);
    void Flush(TimeSpan timeout);
}