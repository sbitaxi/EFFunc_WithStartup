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
        private readonly EDCContext _eDCContext;

        public AddPerson(EDCContext eDCContext)
        {
            _eDCContext = eDCContext;
        }

        [FunctionName("AddPerson")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Person person = JsonConvert.DeserializeObject<Person>(requestBody);
            name = name ?? person?.FirstName;

            await InsertPerson(person);

            return name != null
                ? (ActionResult)new OkObjectResult($"Hello, {name}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }

        public async Task<int> InsertPerson(Person person)
        {
            try
            {
                return await _eDCContext.Database.ExecuteSqlRawAsync(
                @"EXEC AddNewPerson @FirstName = {0},
                                @LastName = {1}, 
                                @Email = {2}, 
                                @Address = {3}, 
                                @City = {4}, 
                                @Province = {5},
                                @PostalCode = {6}",
                        person.FirstName,
                        person.LastName,
                        person.Email,
                        person.Address,
                        person.City,
                        person.Province,
                        person.PostalCode
                        );

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
