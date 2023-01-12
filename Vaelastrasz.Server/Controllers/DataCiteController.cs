using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;
using System.Security.Cryptography;
using Vaelastrasz.Library.Models.ORCID;

namespace Vaelastrasz.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public class DataCiteController : ControllerBase
    {
        [HttpGet("datacite/{doi}")]
        public ORCIDPerson GetPerson(string doi)
        {


            var client = new RestClient($"");
            var request = new RestRequest($"", RestSharp.Method.Get);

            request.AddHeader("Accept", "application/json");

            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<ORCIDPerson>(response.Content);
        }
    }
}
