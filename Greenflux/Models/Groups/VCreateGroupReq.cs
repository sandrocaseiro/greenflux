using FluentValidation;

namespace Greenflux.Models.Groups
{
    public record VCreateGroupReq(string Name, decimal? Capacity);

    public class VCreateGroupReqValidator : AbstractValidator<VCreateGroupReq>
    {
        public VCreateGroupReqValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name should have no more than 100 chars")
                ;

            RuleFor(m => m.Capacity)
                .NotNull().WithMessage("Capacity is required")
                .GreaterThan(0).WithMessage("Capacity must be greater than 0");
        }
    }
}