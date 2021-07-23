using FluentValidation;

namespace Greenflux.Models.Connectors
{
    public record VUpdateConnectorReq(decimal? MaxCurrent);

    public class VUpdateConnectorReqValidator : AbstractValidator<VUpdateConnectorReq>
    {
        public VUpdateConnectorReqValidator()
        {
            RuleFor(m => m.MaxCurrent)
                .NotNull().WithMessage("Connector's Max Current is required")
                .GreaterThan(0).WithMessage("Connector's Max Current must be greater than 0");
        }
    }
}
