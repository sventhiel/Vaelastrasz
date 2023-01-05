﻿using NameParser;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Nozdormu.Library.Models.DataCite
{
    public class DataCiteCreator
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("givenName")]
        public string GivenName { get; set; }

        [JsonProperty("familyName")]
        public string FamilyName { get; set; }

        [JsonProperty("nameType", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public DataCiteCreatorType NameType { get; set; }

        [JsonProperty("affiliation")]
        public List<DataCiteAffiliation> Affiliation { get; set; }

        [JsonProperty("nameIdentifiers")]
        public List<DataCiteNameIdentifier> NameIdentifiers { get; set; }

        [JsonConstructor]
        public DataCiteCreator()
        {
            Affiliation = new List<DataCiteAffiliation>();
            NameIdentifiers = new List<DataCiteNameIdentifier>();
        }

        public DataCiteCreator(string name, DataCiteCreatorType type)
        {
            switch (type)
            {
                case DataCiteCreatorType.Personal:
                    var person = new HumanName(name);

                    //GivenName = name.Substring(0, name.IndexOf(" ")),
                    GivenName = (person.Middle.Length > 0) ? $"{person.First} {person.Middle}" : $"{person.First}";
                    //FamilyName = name.Substring(name.IndexOf(" ") + 1),
                    FamilyName = person.Last;
                    Name = $"{GivenName} {FamilyName}";
                    NameType = type;
                    break;

                case DataCiteCreatorType.Organizational:
                    Name = name;
                    NameType = type;
                    break;

                default:
                    Name = name;
                    break;
            }
        }

        public DataCiteCreator(string firstname, string lastname)
        {
            GivenName = firstname;
            FamilyName = lastname;
            NameType = DataCiteCreatorType.Personal;
        }
    }

    [System.Text.Json.Serialization.JsonConverter(typeof(System.Text.Json.Serialization.JsonStringEnumMemberConverter))]
    public enum DataCiteCreatorType
    {
        [EnumMember(Value = "Personal")]
        Personal = 1,
        [EnumMember(Value = "Organizational")]
        Organizational = 2
    }
}
