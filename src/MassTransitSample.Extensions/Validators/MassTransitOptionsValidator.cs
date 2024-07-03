using System.Linq.Expressions;
using FluentValidation;
using MassTransitSample.Extensions.Models;

namespace MassTransitSample.Extensions.Validators;

public class MassTransitOptionsValidator : AbstractValidator<MassTransitOptions>
{
    
    public MassTransitOptionsValidator()
    {
        this.RuleFor(x => x.QueueName)
            .NotEmpty<MassTransitOptions, string>();
        
        this.RuleFor(x => x.ErrorQueueName)
            .NotEmpty<MassTransitOptions, string>();
        
        this.RuleFor(x => x.RabbitMq)
            .NotNull();
        
        this.RuleFor(x => x.RabbitMq.Url)
            .NotEmpty();
        
        this.RuleFor(x => x.MaximumConcurrencyLevel)
            .GreaterThanOrEqualTo(0);

        this.RuleFor(x => x.NumberOfRetries)
            .GreaterThanOrEqualTo(0);
    }

    private static bool ValidateMaximumConcurrencyLevel(int? arg)
    {
        return !arg.HasValue || arg.GetValueOrDefault() > 0;
    }
    
}