using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EFFunc_WithStartup.EF
{
    public interface IPerson
    {
        Task<int> InsertPerson(Person person);
    }
}
