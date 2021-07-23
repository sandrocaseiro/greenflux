using FluentValidation;

namespace Greenflux.Models.ChargeStations
{
    public record VUpdateChargeStationReq(string Name);

    public class VVUpdateChargeStationReqValidator : AbstractValidator<VUpdateChargeStationReq>
    {
        public VVUpdateChargeStationReqValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name should have no more than 100 chars")
                ;
        }
    }
}