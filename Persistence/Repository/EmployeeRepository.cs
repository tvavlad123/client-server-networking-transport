using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Model;

namespace Transport.Repository
{
    public class EmployeeRepository : AbstractRepository<int, Employee>
    {
        public EmployeeRepository(IValidator<Employee> validator) : base(validator)
        {
        }

    }
}
