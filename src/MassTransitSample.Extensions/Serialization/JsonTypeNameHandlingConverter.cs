using MassTransit;
using Newtonsoft.Json;

namespace MassTransitSample.Extensions.Serialization;

public class JsonTypeNameHandlingConverter(TypeNameHandling typeNameHandling) : JsonConverter
{
    
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) 
        => new JsonSerializer
        {
            TypeNameHandling = typeNameHandling
        }.Serialize(writer, value);

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) 
        => new JsonSerializer
        {
            TypeNameHandling = typeNameHandling
        }.Deserialize(reader, objectType);

    public override bool CanConvert(Type objectType) 
        => !IsMassTransitOrSystemType(objectType);

    private static bool IsMassTransitOrSystemType(Type objectType)
        => objectType.Assembly == typeof(IConsumer).Assembly ||
           objectType.Assembly.IsDynamic ||
           objectType.Assembly == typeof(object).Assembly;
    
}