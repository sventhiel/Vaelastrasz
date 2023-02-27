using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using Vaelastrasz.Library.Extensions;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Tests.Models
{
    public class DataCiteModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            List<ValidationResult> results = new List<ValidationResult>();
            CreateDataCiteModel x = new CreateDataCiteModel();

            var result = x.Validate(out results);

            Assert.IsTrue(result);
        }

        [Test]
        public void Test2()
        {
            var json = "{\r\n  \"data\": {\r\n    \"id\": \"string\",\r\n    \"type\": \"dois\",\r\n    \"attributes\": {\r\n      \"doi\": \"10.23720/20230125004\",\r\n      \"event\": \"hide\",\r\n      \"creators\": [\r\n        {\r\n          \"name\": \"Sven Thiel\",\r\n          \"nameType\": \"Personal\"\r\n        }\r\n      ],\r\n      \"titles\": [\r\n        {\r\n          \"title\": \"This is a automatic generated doi\",\r\n          \"lang\": \"German\",\r\n          \"titleType\": 1\r\n        }\r\n      ],\r\n      \"publisher\": \"string\",\r\n      \"publicationYear\": 2023,\r\n      \"url\": \"https://google.de\"\r\n    }\r\n  }\r\n}";

            var options = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var model = JsonSerializer.Deserialize<CreateDataCiteModel>(json, options);

            var json2 = JsonSerializer.Serialize(model, options);
        }
    }
}