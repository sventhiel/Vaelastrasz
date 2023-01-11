using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using Vaelastrasz.Library.Models.ORCID;

namespace Vaelastrasz.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class ORCIDController : ControllerBase
    {
        [HttpGet("orcid/{orcid}/person"), AllowAnonymous]
        public ORCIDPerson GetName(string orcid)
        {
            var client = new RestClient("https://pub.orcid.org/v3.0/");
            var request = new RestRequest($"{orcid}/person", RestSharp.Method.Get);

            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<ORCIDPerson>(response.Content);
        }
    }
}