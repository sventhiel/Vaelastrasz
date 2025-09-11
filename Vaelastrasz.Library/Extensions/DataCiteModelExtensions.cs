using NameParser;
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

        public static CreateDataCiteModel Update(this CreateDataCiteModel model, List<string> jsonProperties)
        {
            try
            {
                foreach (var property in jsonProperties)
                {
                    switch (property)
                    {
                        case "Creators":
                            foreach (var creator in model.Data.Attributes.Creators)
                            {
                                if (creator.NameType == DataCiteNameType.Personal && !string.IsNullOrEmpty(creator.Name) && (string.IsNullOrEmpty(creator.GivenName) || string.IsNullOrEmpty(creator.FamilyName)))
                                {
                                    var names = new HumanName(creator.Name);
                                    if (!names.IsUnparsable && !string.IsNullOrEmpty(names.First) && !string.IsNullOrEmpty(names.Last))
                                    {
                                        creator.GivenName = names.First;
                                        creator.FamilyName = names.Last;
                                        creator.Name = null;
                                    }
                                }
                            }
                            break;

                        case "Contributors":
                            foreach (var contributor in model.Data.Attributes.Contributors)
                            {
                                if (contributor.NameType == DataCiteNameType.Personal && !string.IsNullOrEmpty(contributor.Name) && (string.IsNullOrEmpty(contributor.GivenName) || string.IsNullOrEmpty(contributor.FamilyName)))
                                {
                                    var names = new HumanName(contributor.Name);
                                    if (!names.IsUnparsable && !string.IsNullOrEmpty(names.First) && !string.IsNullOrEmpty(names.Last))
                                    {
                                        contributor.GivenName = names.First;
                                        contributor.FamilyName = names.Last;
                                        contributor.Name = null;
                                    }
                                }
                            }
                            break;

                        default:
                            break;
                    }
                }

                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel AddContributor(this CreateDataCiteModel model, string name, DataCiteNameType? nameType, DataCiteContributorType contributorType)
        {
            try
            {
                model.Data.Attributes.Contributors.Add(new DataCiteContributor()
                {
                    Name = name,
                    ContributorType = contributorType,
                    NameType = nameType
                });
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
                model.Data.Attributes.Creators.Add(new DataCiteCreator()
                {
                    GivenName = firstname,
                    FamilyName = lastname
                });
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel AddCreator(this CreateDataCiteModel model, string name, DataCiteNameType? nameType)
        {
            try
            {
                model.Data.Attributes.Creators.Add(new DataCiteCreator()
                {
                    Name = name,
                    NameType = nameType
                });
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

        public static CreateDataCiteModel AddDescription(this CreateDataCiteModel model, DataCiteDescription description)
        {
            try
            {
                model.Data.Attributes.Descriptions.Add(description);
                return model;
            }
            catch (Exception)
            {
                return model;
            }
        }

        public static CreateDataCiteModel AddFundingReference(this CreateDataCiteModel model, DataCiteFunderIdentifierType funderIdentifierType, string funderName, string awardNumber, string awardTitle, string awardUri, string funderIdentifier)
        {
            try
            {
                model.Data.Attributes.FundingReferences.Add(new DataCiteFundingReference()
                {
                    AwardNumber = awardNumber,
                    AwardTitle = awardTitle,
                    AwardUri = awardUri,
                    FunderIdentifier = funderIdentifier,
                    FunderIdentifierType = funderIdentifierType,
                    FunderName = funderName
                });
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

        public static CreateDataCiteModel AddTitle(this CreateDataCiteModel model, string title, string language, DataCiteTitleType? type)
        {
            model.Data.Attributes.Titles.Add(new DataCiteTitle()
            {
                Title = title,
                Language = language,
                TitleType = type
            });
            return model;
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
                model.Data.Attributes.Types = new DataCiteTypes()
                {
                    Bibtex = bibtex,
                    Citeproc = citeproc,
                    ResourceType = resourceType,
                    ResourceTypeGeneral = resourceTypeGeneral,
                    Ris = ris,
                    SchemaOrg = schemaOrg
                };

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

        #region ReadDataCiteModel

        public static string GetPrefix(this ReadDataCiteModel model)
        {
            try
            {
                return model.Data.Attributes.Doi.Split('/')[0];
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetSuffix(this ReadDataCiteModel model)
        {
            try
            {
                return model.Data.Attributes.Doi.Split('/')[1];
            }
            catch (Exception)
            {
                return "";
            }
        }

        #endregion ReadDataCiteModel

        #region UpdateDataCiteModel

        public static UpdateDataCiteModel SetUrl(this UpdateDataCiteModel model, string url)
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

        public static UpdateDataCiteModel SetVersion(this UpdateDataCiteModel model, string version)
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

        #endregion UpdateDataCiteModel
    }
}