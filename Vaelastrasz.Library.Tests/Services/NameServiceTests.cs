using Vaelastrasz.Library.Configurations;
using Vaelastrasz.Library.Services;

namespace Vaelastrasz.Library.Tests.Services
{
    public class NameServiceTests
    {
        [Test]
        public async Task Test1()
        {
            //var config = new Configuration("Test_DOIProxy_User", "s~c9<evQ#%^h4Uyb", "https://doi-proxy.bgc-jena.mpg.de");
            var config = new Configuration("Test_DOIProxy_User", "s~c9<evQ#%^h4Uyb", "https://taerar.infinite-trajectory.de", []);

            var nameService = new NameService(config);

            var x = await nameService.GetByNameAsync("Hans Werner Olm");
        }
    }
}