using FluentValidation;

namespace Greenflux.Models.Groups
{
    public record VUpdateGroupCapacityReq(decimal? Capacity);

    public class VUpdateGroupCapacityReqValidator : AbstractValidator<VUpdateGroupCapacityReq>
    {
        public VUpdateGroupCapacityReqValidator()
        {
            RuleFor(m => m.Capacity)
                .NotNull().WithMessage("Capacity is required")
                .GreaterThan(0).WithMessage("Capacity must be greater than 0");
        }
    }
}