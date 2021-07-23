using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using static Greenflux.Models.ChargeStations.VCreateStationReq;

namespace Greenflux.Models.ChargeStations
{
    public record VCreateStationReq(string Name, int? GroupId, List<VCreateStationConnectorReq> Connectors)
    {
        public record VCreateStationConnectorReq(int? Id, decimal? MaxCurrent);
    }

    public class VCreateStationReqValidator : AbstractValidator<VCreateStationReq>
    {
        public VCreateStationReqValidator()
        {
            RuleFor(m => m.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name should have no more than 100 chars")
                ;

            RuleFor(m => m.GroupId)
                .NotNull().WithMessage("Group's Id is required")
                .GreaterThan(0).WithMessage("Group's Id must be greater than 0");

            RuleFor(m => m.Connectors)
                .NotNull().WithMessage("At least one connector is required")
                .NotEmpty().WithMessage("At least one connector is required")
                .Must(c => c == null || c.Count <= 5).WithMessage("No more than 5 connector are allowed")
                .Must(c => c == null || c.Count == c.Select(i => i.Id).Distinct().Count()).WithMessage("Connector's Ids should be unique")
                ;

            RuleForEach(m => m.Connectors)
                .SetValidator(new VCreateStationConnectorReqValidator());
        }
    }

    public class VCreateStationConnectorReqValidator : AbstractValidator<VCreateStationConnectorReq>
    {
        public VCreateStationConnectorReqValidator()
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
