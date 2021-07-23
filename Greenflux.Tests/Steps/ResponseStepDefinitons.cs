using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using static Greenflux.Tests.Steps.RequestStepDefinitions;

namespace Greenflux.Tests.Steps
{
    [Binding]
    public class ResponseStepDefinitons
    {
        [Then("I should receive the status code (\\d*)")]
        public void IShouldReceiveStatusCode(int statusCode)
        {
            _respMessage.StatusCode.Should().Be(statusCode);
        }

        [Then("The response errors should have (\\d*) items")]
        public void TheResponseErrorsShouldHaveItems(int qty)
        {
            (_respBody.errors as List<dynamic>).Should().NotBeEmpty().And.HaveCount(qty);
        }

        [Then("The response errors should have the code (\\d*)")]
        public void TheReponseDataAtIndexShouldHaveAPropertyContaining(int code)
        {
            (_respBody.errors as List<dynamic>).Any(e => e.code == code).Should().BeTrue();
        }

        [Then("The response has (\\d*) errors with code (\\d*) containing (.*)")]
        public void TheReponseDataAtIndexShouldHaveAPropertyContaining(int qty, int code, string value)
        {
            (_respBody.errors as List<dynamic>).Count(e => e.code == code && (e.description as string).Contains(value, System.StringComparison.OrdinalIgnoreCase))
                .Should().Be(qty);
        }

        [Then("The response data should have (\\d*) items")]
        public void TheResponseDataShouldHaveItems(int qty)
        {
            (_respBody.data as List<dynamic>).Should().NotBeEmpty().And.HaveCount(qty);
        }

        [Then("The response data should be empty")]
        public void TheResponseDataShouldBeEmpty()
        {
            (_respBody.data as List<dynamic>).Should().BeEmpty();
        }

        [Then("The response data should be null")]
        public void TheResponseDataShouldBeNull()
        {
            (_respBody.data as object).Should().BeNull();
        }

        [Then("The response data at index (\\d*) should have a (.*) property containing (.*)")]
        public void TheReponseDataAtIndexShouldHaveAPropertyContaining(int index, string property, string value)
        {
            var temp = (_respBody.data as List<dynamic>)[index];
            (temp as IDictionary<string, object>)[property].ToString().Should().Contain(value);
        }

        [Then("The response data at index (\\d*) should have a (.*) property with the value (.*)")]
        public void TheReponseDataAtIndexShouldHaveAPropertyEquals(int index, string property, string value)
        {
            var temp = (_respBody.data as List<dynamic>)[index];
            (temp as IDictionary<string, object>)[property].ToString().Should().Equals(value);
        }

        [Then("The response data should have a (.*) property containing (.*)")]
        public void TheReponseDataShouldHaveAPropertyContaining(string property, string value)
        {
            (_respBody.data as IDictionary<string, object>)[property].ToString().Should().Contain(value);
        }

        [Then("The response data should have a (.*) property with the value (.*)")]
        public void TheReponseDataShouldHaveAPropertyEquals(string property, string value)
        {
            (_respBody.data as IDictionary<string, object>)[property].ToString().Should().Equals(value);
        }
    }
}
