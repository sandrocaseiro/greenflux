using FluentValidation;

namespace Greenflux.Models.Connectors
{
    public record VCreateConnectorReq(int? Id, decimal? MaxCurrent);

    public class VCreateConnectorReqValidator : AbstractValidator<VCreateConnectorReq>
    {
        public VCreateConnectorReqValidator()
        {
            RuleFor(m => m.Id)
                .NotNull().WithMessage("Connector's Id is required")
                .InclusiveBetween(1, 5).WithMessage("Connector's Id should be a number between 1 and 5")
                ;

            RuleFor(m => m.MaxCurrent)
                .NotNull().WithMessage("Connector's Max Current is required")
                .GreaterThan(0).WithMessage("Connector's Max Current must be greater than 0");
        }
    }
}
