using Newtonsoft.Json;
using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Extensions;
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
            var config = new Configuration("sventhiel", "proq3dm6", "http://localhost:5041");

            string text = File.ReadAllText(@"C:/Projects/github.com/sventhiel/Vaelastrasz/Vaelastrasz.Server/Examples/doi_002.json");
            var data = JsonConvert.DeserializeObject<CreateDataCiteModel>(text);

            var dataCiteService = new DataCiteService(config);

            var x = await dataCiteService.CreateAsync(data);
        }

        [Test]
        public async Task Test3()
        {
            //string text = File.ReadAllText(@"C:/Projects/github.com/sventhiel/Vaelastrasz/Vaelastrasz.Server/Examples/doi_001.json");

            var model = new CreateDataCiteModel();
            model.AddCreator("Sven", "Thiel");
            model.AddTitle("Test", "English", Types.DataCiteTitleType.Subtitle);
            model.SetDoi("10.23720/aaaaaaabaasdasd");
            model.SetEvent(Types.DataCiteEventType.Hide);
            model.SetPublicationYear(2024);
            model.SetPublisher("test", "resd", "sdfsdf", "sdfsdf", "English");
            model.SetUrl("https://google.de");
            model.SetType(Types.DataCiteType.DOIs);

            var config = new Configuration("bexis2test", "bexis2test", "http://localhost:5041");
            var dataCiteService = new DataCiteService(config);

            var response = await dataCiteService.CreateAsync(model);
        }

        [Test]
        public async Task Test4()
        {
            var config = new Configuration("sventhiel", "proq3dm6", "http://localhost:5041");
            var dataCiteService = new DataCiteService(config);
            var doiService = new DOIService(config);

            var model = new CreateDataCiteModel();

            model.Data.Attributes.Doi = "";

            //var x = await dataCiteService.CreateAsync(data);
        }
    }
}