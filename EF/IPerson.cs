using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFFunc_WithStartup.EF
{
    public interface IPerson
    {
        Task<int> InsertPerson(Person person);
    }

    public class PersonService : IPerson
    {
        private readonly EDCContext _eDCContext;

        public PersonService(EDCContext eDCContext)
        {
            _eDCContext = eDCContext;
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
