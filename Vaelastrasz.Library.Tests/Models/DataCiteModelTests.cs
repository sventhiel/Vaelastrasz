using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Tests.Models
{
    public class DataCiteModelTests
    {
        [SetUp]
        public void Setup()
        {
        }

        private static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        }

        [Test]
        public void Test1()
        {
            var model = new CreateDataCiteModel();

            var errors = new List<ValidationResult>();
            Validator.TryValidateProperty(model.Data.Attributes.Creators, new ValidationContext(model.Data.Attributes.Creators), errors);
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

        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [Test]
        public void Test3()
        {
            var xml = "<data><type>DOIs</type><attributes><titles><title>test</title></titles></attributes></data>";

            CreateDataCiteDataModel response = new CreateDataCiteDataModel();

            XmlSerializer serializer = new XmlSerializer(typeof(CreateDataCiteDataModel));
            using (StringReader xmlReader = new StringReader(xml))
            {
                response = (CreateDataCiteDataModel)serializer.Deserialize(xmlReader);
            }
        }
    }
}