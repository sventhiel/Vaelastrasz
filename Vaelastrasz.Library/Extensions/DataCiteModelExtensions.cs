using System;
using System.Collections.Generic;
using System.Text;
using Vaelastrasz.Library.Models.DataCite;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Library.Extensions
{
    public static class DataCiteModelExtensions
    {
        #region CreateDataCiteModel

        public static CreateDataCiteModel AddCreator(this CreateDataCiteModel model, string firstname, string lastname)
        {
            try
            {
                model.Data.Attributes.Creators.Add(new DataCiteCreator(firstname, lastname));
                return model;
            }
            catch(Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel AddCreator(this CreateDataCiteModel model, string name, DataCiteCreatorType type)
        {
            try
            {
                model.Data.Attributes.Creators.Add(new DataCiteCreator(name, type));
                return model;
            }
            catch(Exception)
            {
                return model;
            }
            
        }

        public static CreateDataCiteModel RemoveCreator(this CreateDataCiteModel model, int index)
        {
            try
            {
                model.Data.Attributes.Creators.RemoveAt(index);
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel AddTitle(this CreateDataCiteModel model, string title, string language = null, DataCiteTitleType? type = null)
        {
            model.Data.Attributes.Titles.Add(new DataCiteTitle(title, language, type));
            return model;
        }

        public static CreateDataCiteModel RemoveTitle(this CreateDataCiteModel model, int index)
        {
            try
            {
                model.Data.Attributes.Titles.RemoveAt(index);
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel SetDoi(this CreateDataCiteModel model, string doi)
        {
            try
            {
                model.Data.Attributes.Doi = doi;
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel SetType(this CreateDataCiteModel model, DataCiteType? type)
        {
            try
            {
                model.Data.Type = type;
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }



        #endregion
    }
}
