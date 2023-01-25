using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Vaelastrasz.Library.Models;

namespace Vaelastrasz.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class ORCIDController : ControllerBase
    {
        [HttpGet("orcid/{orcid}/person"), Authorize]
        public IActionResult GetPerson(string orcid)
        {
            var client = new RestClient("https://pub.orcid.org/v3.0/");
            var request = new RestRequest($"{orcid}/person", RestSharp.Method.Get);

            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            return StatusCode(200, System.Text.Json.JsonSerializer.Deserialize<ORCIDPerson>(response.Content).name);
        }

        [Obsolete]
        [HttpGet("orcid/{orcid}/employments"), AllowAnonymous]
        public ORCIDPerson GetEmployments(string orcid)
        {
            var client = new RestClient("https://pub.orcid.org/v3.0/");
            var request = new RestRequest($"{orcid}/employments", RestSharp.Method.Get);

            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            return System.Text.Json.JsonSerializer.Deserialize<ORCIDPerson>(response.Content);
        }
    }
}