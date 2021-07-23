using FluentValidation;

namespace Greenflux.Models.Groups
{
    public record VUpdateGroupNameReq(string Name);

    public class VUpdateGroupNameReqValidator : AbstractValidator<VUpdateGroupNameReq>
    {
        public VUpdateGroupNameReqValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name should have no more than 100 chars")
                ;
        }
    }
}