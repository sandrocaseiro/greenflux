using FluentAssertions;
using Greenflux.Data;
using Greenflux.Models.ChargeStations;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using static Greenflux.Tests.Hooks.Hooks;

namespace Greenflux.Tests.Steps
{
    [Binding]
    public class ChargeStationStepDefinitions
    {
        [Then("The charge stations for group (\\d*) should not exist")]
        public async Task TheGroupShouldNotExists(int groupId)
        {
            var repo = (ChargeStationRepository)_host.Services.GetService(typeof(ChargeStationRepository));
            IEnumerable<SChargeStation> stations = await repo.FindAllByGroupIdAsync(groupId);
            stations.Should().BeEmpty();
        }
    }
}
