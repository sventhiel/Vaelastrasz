using Newtonsoft.Json;
using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Library.Services;

namespace Vaelastrasz.Library.Tests.Services
{
    public class DataCiteServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var config = new Configuration("sventhiel", "sventhiel", "http://localhost:5041");

            var dataCiteService = new DataCiteService(config);

            var x = await dataCiteService.FindByDoiAsync("10.23720%2Fapitest005");
        }

        [Test]
        public async Task Test2()
        {
            var config = new Configuration("sventhiel", "sventhiel", "http://localhost:5041");

            string text = File.ReadAllText(@"C:/Projects/github.com/sventhiel/Vaelastrasz/Vaelastrasz.Server/Examples/doi_002.json");
            var data = JsonConvert.DeserializeObject<CreateDataCiteModel>(text);

            var dataCiteService = new DataCiteService(config);

            var x = await dataCiteService.CreateAsync(data);
        }

        [Test]
        public async Task Test3()
        {
            string text = File.ReadAllText(@"C:/Projects/github.com/sventhiel/Vaelastrasz/Vaelastrasz.Server/Examples/doi_001.json");
            var data = JsonConvert.DeserializeObject<CreateDataCiteModel>(text);
            var data2 = new StringContent(JsonConvert.SerializeObject(data));
        }
    }
}