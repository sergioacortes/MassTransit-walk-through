using System.Runtime.Serialization;

namespace MassTransitSample.Extensions.Exceptions;

public class AggregateCannotBeCreatedException : Exception
{
    protected AggregateCannotBeCreatedException(SerializationInfo serializationInfo, StreamingContext streamingContext)
        : base(serializationInfo, streamingContext)
    {
    }
    
    public AggregateCannotBeCreatedException(string propertyName) 
        : base($"Invalid property {propertyName}")
    {
    }
}