using NameParser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Vaelastrasz.Library.Models.DataCite;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Converters
{
    public class DataCiteCreatorConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DataCiteCreator);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Lade das JSON in ein JObject
            var jsonObject = JObject.Load(reader);

            // Erstelle eine Instanz des Zielmodells
            var dataCiteCreator = existingValue as DataCiteCreator ?? new DataCiteCreator();

            // Populiere die Instanz mit Daten aus dem JSON
            serializer.Populate(jsonObject.CreateReader(), dataCiteCreator);

            if (dataCiteCreator.NameType == DataCiteNameType.Personal && !string.IsNullOrEmpty(dataCiteCreator.Name) && (string.IsNullOrEmpty(dataCiteCreator.GivenName) || string.IsNullOrEmpty(dataCiteCreator.FamilyName)))
            {
                var names = new HumanName(dataCiteCreator.Name);
                if (!names.IsUnparsable)
                {
                    dataCiteCreator.GivenName = names.First;
                    dataCiteCreator.FamilyName = names.Last;
                }
            }

            // Weitere Logik und Modifikationen
            return dataCiteCreator;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            serializer.Serialize(writer, value);
        }
    }
}
