using FluentValidation;
using MassTransit;
using MassTransit.RabbitMqTransport.Topology;
using MassTransitSample.Extensions.Exceptions;
using MassTransitSample.Extensions.Models;
using MassTransitSample.Extensions.Serialization;
using MassTransitSample.Extensions.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace MassTransitSample.Extensions;

public static class MassTransitExtensions
{

    public static IServiceCollection AddMassTransitConfiguration(this IServiceCollection services, 
        IConfiguration configuration,
        Action<IBusRegistrationConfigurator> configureBusRegistration,
        Action<IBusRegistrationContext, IRabbitMqReceiveEndpointConfigurator> configureConsumers)
    {

        services.AddMassTransit(busRegistrationConfigurator =>
        {

            var options = GetAndValidateMassTransitOptions(configuration);

            configureBusRegistration(busRegistrationConfigurator);
            
            busRegistrationConfigurator.UsingRabbitMq((busContext, busConfigurator) =>
            {

                var url = new Uri(options.RabbitMq.Url);

                busConfigurator.Host(url);
                busConfigurator.UseInstrumentation(serviceName: "MassTransit");
                
                busConfigurator
                    .ConfigureSerialization()
                    .ConfigureErrorQueue(options)
                    .ConfigureMaximumConcurrencyLevel(options);
                
                busConfigurator.ReceiveEndpoint(options.QueueName, endpointConfigurator =>
                {
                    
                    endpointConfigurator.PublishFaults = false;
                    endpointConfigurator.SetQuorumQueue();

                    configureConsumers(busContext, endpointConfigurator);
                    
                    endpointConfigurator.UseMessageRetry(r =>
                    {
                        r.Exponential(options.NumberOfRetries,
                            TimeSpan.FromMilliseconds(350),
                            TimeSpan.FromMilliseconds(1000),
                            TimeSpan.FromMilliseconds(150));
                        r.Ignore(typeof(AggregateCannotBeCreatedException));
                    });
                    
                });

            });

        });
        
        return services;

    }
    
    private static MassTransitOptions GetAndValidateMassTransitOptions(IConfiguration configuration)
    {
        
        var options = configuration.GetSection(MassTransitOptions.DefaultSectionKey)
            .Get<MassTransitOptions>();

        new MassTransitOptionsValidator().ValidateAndThrow(options);

        return options;
        
    }

    private static IRabbitMqBusFactoryConfigurator ConfigureSerialization(
        this IRabbitMqBusFactoryConfigurator configurator)
    {
        
        configurator.ClearSerialization();
        configurator.AddRawJsonSerializer(RawSerializerOptions.All);
        
        configurator.ConfigureNewtonsoftJsonSerializer(settings =>
        {
            settings.TypeNameHandling = TypeNameHandling.Auto;
            return settings;
        });
                                
        configurator.ConfigureNewtonsoftJsonDeserializer(settings =>
        {
            settings.Converters.Add(new JsonTypeNameHandlingConverter(TypeNameHandling.Objects));
            return settings;
        });
        
        configurator.SupportNServiceBusJsonDeserializer();
        
        return configurator;
    }

    private static IRabbitMqBusFactoryConfigurator ConfigureErrorQueue(
        this IRabbitMqBusFactoryConfigurator configurator, MassTransitOptions options)
    {
        
        configurator.SendTopology.ConfigureErrorSettings = settings =>
        {
            if (settings is not RabbitMqErrorSettings rabbitMqErrorSettings) return;
            rabbitMqErrorSettings.QueueName = options.ErrorQueueName;
            rabbitMqErrorSettings.SetQuorumQueue(null);
        };

        return configurator;

    }
    
    private static IRabbitMqBusFactoryConfigurator ConfigureMaximumConcurrencyLevel(
        this IRabbitMqBusFactoryConfigurator configurator, MassTransitOptions options)
    {
        
        if (options.MaximumConcurrencyLevel is > 0)
        {
            configurator.UseConcurrencyLimit(options.MaximumConcurrencyLevel);
            configurator.PrefetchCount = options.MaximumConcurrencyLevel * 2;
        }

        return configurator;

    }

}