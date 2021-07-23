using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Greenflux.Tests.Hooks
{
    [Binding]
    public class Hooks
    {
        private readonly ScenarioContext _scenarioContext;
        public static IHost _host;

        public Hooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            File.Delete("greenflux.db");
            _host = Program.CreateHostBuilder(Enumerable.Empty<string>().ToArray()).Build();
            _host.Start();
        }

        [AfterTestRun]
        public static async Task AfterTestRun()
        {
            await _host.StopAsync();
        }

        [BeforeFeature]
        public static void BeforeFeature()
        {
            var env = (IWebHostEnvironment)_host.Services.GetService(typeof(IWebHostEnvironment));
            var createScriptPath = env.ContentRootPath + "/Scripts/createdb.sql";
            var dataScriptPath = env.ContentRootPath + "/Scripts/testdata.sql";
            var conn = (IDbConnection)_host.Services.GetService(typeof(IDbConnection));
            var createQuery = File.ReadAllText(createScriptPath);
            var dataQuery = File.ReadAllText(dataScriptPath);
            conn.Execute(createQuery);
            conn.Execute(dataQuery);
        }

        [AfterFeature]
        public static void AfterFeature()
        {
            var env = (IWebHostEnvironment)_host.Services.GetService(typeof(IWebHostEnvironment));
            var scriptPath = env.ContentRootPath + "/Scripts/dropdb.sql";
            var conn = (IDbConnection)_host.Services.GetService(typeof(IDbConnection));
            var createQuery = File.ReadAllText(scriptPath);
            conn.Execute(createQuery);
        }
    }
}