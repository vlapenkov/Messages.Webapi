using System.Text.Json;
using Confluent.Kafka;

namespace RK.Messages.Shared.Serializers;

/// <summary>
/// Класс сериализатор\десириализатор объектов
/// </summary>
/// <typeparam name="T"></typeparam>
public class ObjectSerializerDeserializer<T> : ISerializer<T?>, IDeserializer<T?>
{

    /// <summary>
    /// </summary>
    public ObjectSerializerDeserializer(JsonSerializerOptions defaultOptions) => DefaultOptions = defaultOptions;

    private JsonSerializerOptions DefaultOptions { get; }

    /// <summary>
    /// Сериализовать объект
    /// </summary>
    public byte[] Serialize(T? data, SerializationContext context)
        => JsonSerializer.SerializeToUtf8Bytes(data, DefaultOptions);

    /// <summary>
    /// Десериализовать объект
    /// </summary>
    public T? Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
        => JsonSerializer.Deserialize<T>(data, DefaultOptions);
}