﻿using System.ComponentModel.DataAnnotations;
using Vaelastrasz.Library.Attributes;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Tests.Attributes
{
    public class CardinalityAttributeTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var model = new CreateDataCiteModel();

            model.Data.Attributes.Creators = null;

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);
            ValidationAttribute[] x = new ValidationAttribute[] { new CardinalityAttribute() };
            var isValid = Validator.TryValidateValue(model.Data.Attributes.Creators, validationContext, validationResults, x);

            Assert.That(isValid, Is.True);
        }
    }
}