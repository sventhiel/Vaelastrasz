﻿using Newtonsoft.Json;
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
            //var config = new Configuration("Test_DOIProxy_User", "s~c9<evQ#%^h4Uyb", "https://taerar.infinite-trajectory.de", true);
            var config = new Configuration("sventhiel", "sventhiel", "https://taerar.infinite-trajectory.de", true);

            var dataCiteService = new DataCiteService(config);

            var x = await dataCiteService.FindAsync();
        }

        [Test]
        public async Task Test2()
        {
            try
            {
                var config = new Configuration("sventhiel", "sventhiel", "https://taerar.infinite-trajectory.de", true);

                string text = File.ReadAllText(@"C:/Projects/github.com/sventhiel/Vaelastrasz/Vaelastrasz.Server/Examples/doi_002.json");
                var data = JsonConvert.DeserializeObject<CreateDataCiteModel>(text);

                var dataCiteService = new DataCiteService(config);

                var x = await dataCiteService.CreateAsync(data);
                var y = await dataCiteService.GetCitationStyleByDoiAsync("10.23720%2Fapitest005", Types.DataCiteCitationStyleType.APA);
                var z = await dataCiteService.GetMetadataFormatByDoiAsync("10.23720%2Fapitest005", Types.DataCiteMetadataFormatType.BibTeX);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR", ex.ToString());
            }
        }

        [Test]
        public async Task Test3()
        {
            //string text = File.ReadAllText(@"C:/Projects/github.com/sventhiel/Vaelastrasz/Vaelastrasz.Server/Examples/doi_001.json");

            var model = new CreateDataCiteModel();
            model.SetType(Types.DataCiteType.DOIs);
            model.SetTypes(Types.DataCiteResourceTypeGeneral.Dataset, "", "", "", "", "");
            model.AddCreator("Sven Thiel", Types.DataCiteNameType.Personal);
            model.AddTitle("Test", "English", Types.DataCiteTitleType.Subtitle);
            model.SetDoi("10.25829/45b58z45");
            model.SetEvent(Types.DataCiteEventType.Hide);
            model.SetPublicationYear(2024);
            model.SetPublisher("test", "resd", "sdfsdf", "sdfsdf", "English");
            model.SetUrl("https://google.de");

            //var config = new Configuration("Test_DOIProxy_User", "s~c9<evQ#%^h4Uyb", "https://taerar.infinite-trajectory.de", true);
            var config = new Configuration("biodivbank2", "biodivbank2", "https://doi.bexis2.uni-jena.de", true);

            var dataCiteService = new DataCiteService(config);

            var response = await dataCiteService.CreateAsync(model);
        }

        [Test]
        public async Task Test4()
        {
            var config = new Configuration("sventhiel", "proq3dm6", "http://localhost:5041", true);
            var dataCiteService = new DataCiteService(config);
            var doiService = new DOIService(config);

            var model = new CreateDataCiteModel();

            model.Data.Attributes.Doi = "";

            //var x = await dataCiteService.CreateAsync(data);
        }
    }
}