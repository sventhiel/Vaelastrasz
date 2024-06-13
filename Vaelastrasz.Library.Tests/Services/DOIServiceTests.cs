using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Library.Services;

namespace Vaelastrasz.Library.Tests.Services
{
    public class DOIServiceTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            try
            {
                var config = new Configuration("bexis2test", "bexis2test", "https://doi.bexis2.uni-jena.de");

                var doiService = new DOIService(config);

                var dict = new Dictionary<string, string>
                {
                    { "{DatasetId}", "1" }
                };

                var createSuffixModel = new CreateSuffixModel()
                {
                    Placeholders = dict
                };

                var doi = await doiService.GenerateAsync(createSuffixModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}