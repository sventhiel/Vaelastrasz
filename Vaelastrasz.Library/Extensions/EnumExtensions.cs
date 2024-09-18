using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Vaelastrasz.Library.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumMemberValue(Enum enumValue)
        {
            // Get the type of the enum
            Type enumType = enumValue.GetType();

            // Get the member info for the specific enum value
            MemberInfo[] memberInfo = enumType.GetMember(enumValue.ToString());

            if (memberInfo.Length > 0)
            {
                // Look for EnumMemberAttribute
                object[] attributes = memberInfo[0].GetCustomAttributes(typeof(EnumMemberAttribute), false);

                if (attributes.Length > 0)
                {
                    // Return the value of EnumMember attribute
                    return ((EnumMemberAttribute)attributes[0]).Value;
                }
            }

            // Return null or the enum's default string representation if no EnumMember is found
            return enumValue.ToString();
        }
    }
}
