using Vaelastrasz.Library.Configurations;
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
    }
}