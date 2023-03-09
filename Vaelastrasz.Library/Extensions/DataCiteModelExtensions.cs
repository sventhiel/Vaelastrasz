using System;
using System.Collections.Generic;
using System.Text;
using Vaelastrasz.Library.Models.DataCite;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Extensions
{
    public static class DataCiteModelExtensions
    {
        public static CreateDataCiteModel AddCreator(this CreateDataCiteModel model, string firstname, string lastname)
        {
            model.Data.Attributes.Creators.Add(new DataCiteCreator(firstname, lastname));
            return model;
        }

        public static CreateDataCiteModel AddCreator(this CreateDataCiteModel model, string name, DataCiteCreatorType type)
        {
            model.Data.Attributes.Creators.Add(new DataCiteCreator(name, type));
            return model;
        }

        public static CreateDataCiteModel AddTitle(this CreateDataCiteModel model, string title, string language = null, DataCiteTitleType? type = null)
        {
            model.Data.Attributes.Titles.Add(new DataCiteTitle(title, language, type));
            return model;
        }
    }
}
