using FluentAssertions;
using Greenflux.Data;
using Greenflux.Models.Connectors;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using static Greenflux.Tests.Hooks.Hooks;

namespace Greenflux.Tests.Steps
{
    [Binding]
    public class ConnectorStepDefinitions
    {
        [Then("The connectors for group (\\d*) should not exist")]
        public async Task TheGroupShouldNotExists(int groupId)
        {
            var repo = (ConnectorRepository)_host.Services.GetService(typeof(ConnectorRepository));
            IEnumerable<SConnector> stations = await repo.FindAllByGroupAsync(groupId);
            stations.Should().BeEmpty();
        }
    }
}
