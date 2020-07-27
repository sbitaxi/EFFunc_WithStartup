using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using EFFunc_WithStartup.EF;
using Microsoft.EntityFrameworkCore;

namespace EFFunc_WithStartup
{
    public class AddPerson
    {
        private readonly IPerson _iPerson;

        public AddPerson(IPerson iPerson)
        {
            _iPerson = iPerson;
        }

        [FunctionName("AddPerson")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Add Person Request");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Person person = JsonConvert.DeserializeObject<Person>(requestBody);
            name = name ?? person?.FirstName;

            await _iPerson.InsertPerson(person);

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
