using System;
using System.Collections.Generic;
using Vaelastrasz.Library.Models;
using Vaelastrasz.Library.Models.DataCite;
using Vaelastrasz.Library.Types;

namespace Vaelastrasz.Library.Extensions
{
    public static class DataCiteModelExtensions
    {
        #region CreateDataCiteModel

        public static CreateDataCiteModel AddFundingReference(this CreateDataCiteModel model, DataCiteFunderIdentifierType funderIdentifierType, string funderName, string awardNumber, string awardTitle, string awardUri, string funderIdentifier)
        {
            try
            {
                model.Data.Attributes.FundingReferences.Add(new DataCiteFundingReference(funderIdentifierType, funderName, awardNumber, awardTitle, awardUri, funderIdentifier));
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel AddCreator(this CreateDataCiteModel model, string firstname, string lastname)
        {
            try
            {
                model.Data.Attributes.Creators.Add(new DataCiteCreator(firstname, lastname));
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel AddCreator(this CreateDataCiteModel model, string name, DataCiteNameType type)
        {
            try
            {
                model.Data.Attributes.Creators.Add(new DataCiteCreator(name, type));
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel AddContributor(this CreateDataCiteModel model, string name, DataCiteNameType nameType, DataCiteContributorType contributorType)
        {
            try
            {
                model.Data.Attributes.Contributors.Add(new DataCiteContributor(name, nameType, contributorType));
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel AddCreator(this CreateDataCiteModel model, DataCiteCreator creator)
        {
            try
            {
                model.Data.Attributes.Creators.Add(creator);
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel AddSize(this CreateDataCiteModel model, string size)
        {
            try
            {
                model.Data.Attributes.Sizes.Add(size);
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel AddCreators(this CreateDataCiteModel model, List<DataCiteCreator> creators)
        {
            try
            {
                model.Data.Attributes.Creators.AddRange(creators);
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel AddTitle(this CreateDataCiteModel model, string title, string language, DataCiteTitleType type)
        {
            model.Data.Attributes.Titles.Add(new DataCiteTitle(title, language, type));
            return model;
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

        public static CreateDataCiteModel RemoveFundingReference(this CreateDataCiteModel model, int index)
        {
            try
            {
                model.Data.Attributes.FundingReferences.RemoveAt(index);
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel RemoveSize(this CreateDataCiteModel model, int index)
        {
            try
            {
                model.Data.Attributes.Sizes.RemoveAt(index);
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel RemoveContributor(this CreateDataCiteModel model, int index)
        {
            try
            {
                model.Data.Attributes.Contributors.RemoveAt(index);
                return model;
            }
            catch (Exception)
            {
                return model;
            }
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

        public static CreateDataCiteModel SetPublicationYear(this CreateDataCiteModel model, int publicationYear)
        {
            try
            {
                model.Data.Attributes.PublicationYear = publicationYear;
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel SetPublisher(this CreateDataCiteModel model, string name, string publisherIdentifier, string publisherIdentifierScheme, string schemeUri, string lang)
        {
            try
            {
                var publisher = new DataCitePublisher()
                {
                    Name = name,
                    PublisherIdentifier = publisherIdentifier,
                    PublisherIdentifierScheme = publisherIdentifierScheme,
                    SchemeUri = schemeUri,
                    Language = lang
                };

                model.Data.Attributes.Publisher = publisher;
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

        public static CreateDataCiteModel SetEvent(this CreateDataCiteModel model, DataCiteEventType eventType)
        {
            try
            {
                model.Data.Attributes.Event = eventType;
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel SetType(this CreateDataCiteModel model, DataCiteType type = DataCiteType.DOIs)
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

        public static CreateDataCiteModel SetTypes(this CreateDataCiteModel model, DataCiteResourceTypeGeneral resourceTypeGeneral, string bibtex, string citeproc, string resourceType, string ris, string schemaOrg)
        {
            try
            {
                model.Data.Attributes.Types = new DataCiteTypes(resourceTypeGeneral, bibtex, citeproc, resourceType, ris, schemaOrg);
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel SetUrl(this CreateDataCiteModel model, string url)
        {
            try
            {
                model.Data.Attributes.URL = url;
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel SetVersion(this CreateDataCiteModel model, string version)
        {
            try
            {
                model.Data.Attributes.Version = version;
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        #endregion CreateDataCiteModel
    }
}