using FluentAssertions;
using Greenflux.Data;
using Greenflux.Models.Groups;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using static Greenflux.Tests.Hooks.Hooks;
using static Greenflux.Tests.Steps.RequestStepDefinitions;

namespace Greenflux.Tests.Steps
{
    [Binding]
    public class GroupStepDefinitions
    {
        [Then("The created group should exists in the database with the correct values")]
        public async Task TheCreatedGroupShouldExistsInTheDatabaseWithThePayloadValues()
        {
            var repo = (GroupRepository)_host.Services.GetService(typeof(GroupRepository));
            dynamic payload = JsonConvert.DeserializeObject<ExpandoObject>(_payload, new ExpandoObjectConverter());

            SGroup group = await repo.FindByIdAsync(int.Parse(_respBody.data.id.ToString()));
            group.Should().NotBeNull();

            group.Name.Should().Be(payload.name);
            group.Capacity.Should().Be(payload.capacity);
        }

        [Then("The group (\\d*) should not exist")]
        public async Task TheGroupShouldNotExists(int groupId)
        {
            var repo = (GroupRepository)_host.Services.GetService(typeof(GroupRepository));
            SGroup group = await repo.FindByIdAsync(groupId);
            group.Should().BeNull();
        }
    }
}
