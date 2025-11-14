using Newtonsoft.Json;
using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Library.Services;

namespace Vaelastrasz.Library.Tests.Services
{
    public class SuffixServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test2()
        {
            var config = new Configuration("sventhiel", "sventhiel", "https://taerar.infinite-trajectory.de");

            string text = File.ReadAllText(@"C:/Projects/github.com/sventhiel/Vaelastrasz/Vaelastrasz.Server/Examples/doi_002.json");
            var data = JsonConvert.DeserializeObject<CreateDataCiteModel>(text);

            var dataCiteService = new DataCiteService(config);

            var x = await dataCiteService.CreateAsync(data);
            var y = await dataCiteService.GetCitationStyleByDoiAsync("10.23720%2Fapitest005", Types.DataCiteCitationStyleType.APA);
            var z = await dataCiteService.GetMetadataFormatByDoiAsync("10.23720%2Fapitest005", Types.DataCiteMetadataFormatType.BibTeX);
        }
    }
}