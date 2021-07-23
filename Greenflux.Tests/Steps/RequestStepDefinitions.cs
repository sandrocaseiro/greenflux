using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Dynamic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Greenflux.Tests.Steps
{
    [Binding]
    public class RequestStepDefinitions
    {
        private static HttpClient client = new HttpClient()
        {
            BaseAddress = new System.Uri("http://localhost:5000")
        };

        private readonly ScenarioContext _scenarioContext;
        private readonly Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        public static string _payload;
        public static HttpResponseMessage _respMessage;
        public static dynamic _respBody;

        public RequestStepDefinitions(ScenarioContext scenarioContext, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            _scenarioContext = scenarioContext;
            _testOutputHelper = testOutputHelper;
        }

        [When("I use the payload")]
        public void IUseThePayload(string payload)
        {
            _payload = payload;
        }

        [When("I GET to (.*)")]
        public async Task IGETUrl(string url)
        {
            _respMessage = await client.GetAsync(url);
            var bodyString = await _respMessage.Content.ReadAsStringAsync();
            _respBody = JsonConvert.DeserializeObject<ExpandoObject>(bodyString, new ExpandoObjectConverter());
            _testOutputHelper.WriteLine(bodyString);
        }

        [When("I POST to (.*)")]
        public async Task IPOSTUrl(string url)
        {
            _respMessage = await client.PostAsync(url, new StringContent(_payload, Encoding.UTF8, "application/json"));
            var bodyString = await _respMessage.Content.ReadAsStringAsync();
            _respBody = JsonConvert.DeserializeObject<ExpandoObject>(bodyString, new ExpandoObjectConverter());
            _testOutputHelper.WriteLine(bodyString);
        }

        [When("I DELETE to (.*)")]
        public async Task IDELETEUrl(string url)
        {
            _respMessage = await client.DeleteAsync(url);
            var bodyString = await _respMessage.Content.ReadAsStringAsync();
            _respBody = JsonConvert.DeserializeObject<ExpandoObject>(bodyString, new ExpandoObjectConverter());
            _testOutputHelper.WriteLine(bodyString);
        }

        [When("I call group list endpoint")]
        public async Task ICallGroupListEndpoint()
        {
            _respMessage = await client.GetAsync("/v1/groups");
            var bodyString = await _respMessage.Content.ReadAsStringAsync();
            _respBody = JsonConvert.DeserializeObject<ExpandoObject>(bodyString, new ExpandoObjectConverter());
            _testOutputHelper.WriteLine(bodyString);
        }
    }
}