using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Library.Extensions;

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
    }
}
