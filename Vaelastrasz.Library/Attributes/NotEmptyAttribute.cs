using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Vaelastrasz.Library.Attributes
{
    public class NotEmptyAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value.GetType().GetGenericTypeDefinition() == typeof(List<>))
            {
                var collection = (IList)value;
                return collection.Count > 0;
            }

            return false;
        }
    }
}
