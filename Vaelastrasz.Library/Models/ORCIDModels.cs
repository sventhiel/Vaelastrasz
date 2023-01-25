using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Vaelastrasz.Library.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ActivitiesSummary
    {
        [JsonPropertyName("last-modified-date")]
        public LastModifiedDate lastmodifieddate { get; set; }
        public Distinctions distinctions { get; set; }
        public Educations educations { get; set; }
        public Employments employments { get; set; }
        public Fundings fundings { get; set; }

        [JsonPropertyName("invited-positions")]
        public InvitedPositions invitedpositions { get; set; }
        public Memberships memberships { get; set; }

        [JsonPropertyName("peer-reviews")]
        public PeerReviews peerreviews { get; set; }
        public Qualifications qualifications { get; set; }

        [JsonPropertyName("research-resources")]
        public ResearchResources researchresources { get; set; }
        public Services services { get; set; }
        public Works works { get; set; }
        public string path { get; set; }
    }

    public class Address
    {
        [JsonPropertyName("created-date")]
        public CreatedDate createddate { get; set; }

        [JsonPropertyName("last-modified-date")]
        public LastModifiedDate lastmodifieddate { get; set; }
        public Source source { get; set; }
        public Country country { get; set; }
        public string visibility { get; set; }
        public string path { get; set; }

        [JsonPropertyName("put-code")]
        public int putcode { get; set; }

        [JsonPropertyName("display-index")]
        public int displayindex { get; set; }
        public string city { get; set; }
        public string region { get; set; }
    }

    public class Addresses
    {
        [JsonPropertyName("last-modified-date")]
        public LastModifiedDate lastmodifieddate { get; set; }
        public List<Address> address { get; set; }
        public string path { get; set; }
    }

    public class AffiliationGroup
    {
        [JsonPropertyName("last-modified-date")]
        public LastModifiedDate lastmodifieddate { get; set; }

        [JsonPropertyName("external-ids")]
        public ExternalIds externalids { get; set; }
        public List<Summary> summaries { get; set; }
    }

    public class Country
    {
        public string value { get; set; }
    }

    public class CreatedDate
    {
        public long value { get; set; }
    }

    public class Day
    {
        public string value { get; set; }
    }

    public class DisambiguatedOrganization
    {
        [JsonPropertyName("disambiguated-organization-identifier")]
        public string disambiguatedorganizationidentifier { get; set; }

        [JsonPropertyName("disambiguation-source")]
        public string disambiguationsource { get; set; }
    }

    public class Distinctions
    {
        [JsonPropertyName("last-modified-date")]
        public object lastmodifieddate { get; set; }

        [JsonPropertyName("affiliation-group")]
        public List<object> affiliationgroup { get; set; }
        public string path { get; set; }
    }

    public class Educations
    {
        [JsonPropertyName("last-modified-date")]
        public LastModifiedDate lastmodifieddate { get; set; }

        [JsonPropertyName("affiliation-group")]
        public List<AffiliationGroup> affiliationgroup { get; set; }
        public string path { get; set; }
    }

    public class EducationSummary
    {
        [JsonPropertyName("created-date")]
        public CreatedDate createddate { get; set; }

        [JsonPropertyName("last-modified-date")]
        public LastModifiedDate lastmodifieddate { get; set; }
        public Source source { get; set; }

        [JsonPropertyName("put-code")]
        public int putcode { get; set; }

        [JsonPropertyName("department-name")]
        public object departmentname { get; set; }

        [JsonPropertyName("role-title")]
        public string roletitle { get; set; }

        [JsonPropertyName("start-date")]
        public StartDate startdate { get; set; }

        [JsonPropertyName("end-date")]
        public EndDate enddate { get; set; }
        public Organization organization { get; set; }
        public object url { get; set; }

        [JsonPropertyName("external-ids")]
        public object externalids { get; set; }

        [JsonPropertyName("display-index")]
        public string displayindex { get; set; }
        public string visibility { get; set; }
        public string path { get; set; }
    }

    public class Emails
    {
        [JsonPropertyName("last-modified-date")]
        public object lastmodifieddate { get; set; }
        public List<object> email { get; set; }
        public string path { get; set; }
    }

    public class Employments
    {
        [JsonPropertyName("last-modified-date")]
        public LastModifiedDate lastmodifieddate { get; set; }

        [JsonPropertyName("affiliation-group")]
        public List<AffiliationGroup> affiliationgroup { get; set; }
        public string path { get; set; }
    }

    public class EmploymentSummary
    {
        [JsonPropertyName("created-date")]
        public CreatedDate createddate { get; set; }

        [JsonPropertyName("last-modified-date")]
        public LastModifiedDate lastmodifieddate { get; set; }
        public Source source { get; set; }

        [JsonPropertyName("put-code")]
        public int putcode { get; set; }

        [JsonPropertyName("department-name")]
        public object departmentname { get; set; }

        [JsonPropertyName("role-title")]
        public object roletitle { get; set; }

        [JsonPropertyName("start-date")]
        public StartDate startdate { get; set; }

        [JsonPropertyName("end-date")]
        public EndDate enddate { get; set; }
        public Organization organization { get; set; }
        public object url { get; set; }

        [JsonPropertyName("external-ids")]
        public object externalids { get; set; }

        [JsonPropertyName("display-index")]
        public string displayindex { get; set; }
        public string visibility { get; set; }
        public string path { get; set; }
    }

    public class EndDate
    {
        public Year year { get; set; }
        public Month month { get; set; }
        public Day day { get; set; }
    }

    public class ExternalIdentifiers
    {
        [JsonPropertyName("last-modified-date")]
        public object lastmodifieddate { get; set; }

        [JsonPropertyName("external-identifier")]
        public List<object> externalidentifier { get; set; }
        public string path { get; set; }
    }

    public class ExternalIds
    {
        [JsonPropertyName("external-id")]
        public List<object> externalid { get; set; }
    }

    public class FamilyName
    {
        public string value { get; set; }
    }

    public class Fundings
    {
        [JsonPropertyName("last-modified-date")]
        public object lastmodifieddate { get; set; }
        public List<object> group { get; set; }
        public string path { get; set; }
    }

    public class GivenNames
    {
        public string value { get; set; }
    }

    public class History
    {
        [JsonPropertyName("creation-method")]
        public string creationmethod { get; set; }

        [JsonPropertyName("completion-date")]
        public object completiondate { get; set; }

        [JsonPropertyName("submission-date")]
        public SubmissionDate submissiondate { get; set; }

        [JsonPropertyName("last-modified-date")]
        public LastModifiedDate lastmodifieddate { get; set; }
        public bool claimed { get; set; }
        public object source { get; set; }

        [JsonPropertyName("deactivation-date")]
        public object deactivationdate { get; set; }

        [JsonPropertyName("verified-email")]
        public bool verifiedemail { get; set; }

        [JsonPropertyName("verified-primary-email")]
        public bool verifiedprimaryemail { get; set; }
    }

    public class InvitedPositions
    {
        [JsonPropertyName("last-modified-date")]
        public object lastmodifieddate { get; set; }

        [JsonPropertyName("affiliation-group")]
        public List<object> affiliationgroup { get; set; }
        public string path { get; set; }
    }

    public class Keywords
    {
        [JsonPropertyName("last-modified-date")]
        public object lastmodifieddate { get; set; }
        public List<object> keyword { get; set; }
        public string path { get; set; }
    }

    public class LastModifiedDate
    {
        public long value { get; set; }
    }

    public class Memberships
    {
        [JsonPropertyName("last-modified-date")]
        public object lastmodifieddate { get; set; }

        [JsonPropertyName("affiliation-group")]
        public List<object> affiliationgroup { get; set; }
        public string path { get; set; }
    }

    public class Month
    {
        public string value { get; set; }
    }

    public class Name
    {
        [JsonPropertyName("created-date")]
        public CreatedDate createddate { get; set; }

        [JsonPropertyName("last-modified-date")]
        public LastModifiedDate lastmodifieddate { get; set; }

        [JsonPropertyName("given-names")]
        public GivenNames givennames { get; set; }

        [JsonPropertyName("family-name")]
        public FamilyName familyname { get; set; }

        [JsonPropertyName("credit-name")]
        public object creditname { get; set; }
        public object source { get; set; }
        public string visibility { get; set; }
        public string path { get; set; }
    }

    public class OrcidIdentifier
    {
        public string uri { get; set; }
        public string path { get; set; }
        public string host { get; set; }
    }

    public class Organization
    {
        public string name { get; set; }
        public Address address { get; set; }

        [JsonPropertyName("disambiguated-organization")]
        public DisambiguatedOrganization disambiguatedorganization { get; set; }
    }

    public class OtherName
    {
        [JsonPropertyName("created-date")]
        public CreatedDate createddate { get; set; }

        [JsonPropertyName("last-modified-date")]
        public LastModifiedDate lastmodifieddate { get; set; }
        public Source source { get; set; }
        public string content { get; set; }
        public string visibility { get; set; }
        public string path { get; set; }

        [JsonPropertyName("put-code")]
        public int putcode { get; set; }

        [JsonPropertyName("display-index")]
        public int displayindex { get; set; }
    }

    public class OtherNames
    {
        [JsonPropertyName("last-modified-date")]
        public LastModifiedDate lastmodifieddate { get; set; }

        [JsonPropertyName("other-name")]
        public List<OtherName> othername { get; set; }
        public string path { get; set; }
    }

    public class PeerReviews
    {
        [JsonPropertyName("last-modified-date")]
        public object lastmodifieddate { get; set; }
        public List<object> group { get; set; }
        public string path { get; set; }
    }

    public class ORCIDPerson
    {
        [JsonPropertyName("last-modified-date")]
        public LastModifiedDate lastmodifieddate { get; set; }
        public Name name { get; set; }

        [JsonPropertyName("other-names")]
        public OtherNames othernames { get; set; }
        public object biography { get; set; }

        [JsonPropertyName("researcher-urls")]
        public ResearcherUrls researcherurls { get; set; }
        public Emails emails { get; set; }
        public Addresses addresses { get; set; }
        public Keywords keywords { get; set; }

        [JsonPropertyName("external-identifiers")]
        public ExternalIdentifiers externalidentifiers { get; set; }
        public string path { get; set; }
    }

    public class Preferences
    {
        public string locale { get; set; }
    }

    public class Qualifications
    {
        [JsonPropertyName("last-modified-date")]
        public object lastmodifieddate { get; set; }

        [JsonPropertyName("affiliation-group")]
        public List<object> affiliationgroup { get; set; }
        public string path { get; set; }
    }

    public class ResearcherUrls
    {
        [JsonPropertyName("last-modified-date")]
        public object lastmodifieddate { get; set; }

        [JsonPropertyName("researcher-url")]
        public List<object> researcherurl { get; set; }
        public string path { get; set; }
    }

    public class ResearchResources
    {
        [JsonPropertyName("last-modified-date")]
        public object lastmodifieddate { get; set; }
        public List<object> group { get; set; }
        public string path { get; set; }
    }

    public class ORCIDModel
    {
        [JsonPropertyName("orcid-identifier")]
        public OrcidIdentifier orcididentifier { get; set; }
        public Preferences preferences { get; set; }
        public History history { get; set; }
        public ORCIDPerson person { get; set; }

        [JsonPropertyName("activities-summary")]
        public ActivitiesSummary activitiessummary { get; set; }
        public string path { get; set; }
    }

    public class Services
    {
        [JsonPropertyName("last-modified-date")]
        public object lastmodifieddate { get; set; }

        [JsonPropertyName("affiliation-group")]
        public List<object> affiliationgroup { get; set; }
        public string path { get; set; }
    }

    public class Source
    {
        [JsonPropertyName("source-orcid")]
        public SourceOrcid sourceorcid { get; set; }

        [JsonPropertyName("source-client-id")]
        public object sourceclientid { get; set; }

        [JsonPropertyName("source-name")]
        public SourceName sourcename { get; set; }

        [JsonPropertyName("assertion-origin-orcid")]
        public object assertionoriginorcid { get; set; }

        [JsonPropertyName("assertion-origin-client-id")]
        public object assertionoriginclientid { get; set; }

        [JsonPropertyName("assertion-origin-name")]
        public object assertionoriginname { get; set; }
    }

    public class SourceName
    {
        public string value { get; set; }
    }

    public class SourceOrcid
    {
        public string uri { get; set; }
        public string path { get; set; }
        public string host { get; set; }
    }

    public class StartDate
    {
        public Year year { get; set; }
        public Month month { get; set; }
        public Day day { get; set; }
    }

    public class SubmissionDate
    {
        public long value { get; set; }
    }

    public class Summary
    {
        [JsonPropertyName("education-summary")]
        public EducationSummary educationsummary { get; set; }

        [JsonPropertyName("employment-summary")]
        public EmploymentSummary employmentsummary { get; set; }
    }

    public class Works
    {
        [JsonPropertyName("last-modified-date")]
        public object lastmodifieddate { get; set; }
        public List<object> group { get; set; }
        public string path { get; set; }
    }

    public class Year
    {
        public string value { get; set; }
    }


}