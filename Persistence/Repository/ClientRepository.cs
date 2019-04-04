using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Model;

namespace Transport.Repository
{
    public class ClientRepository : AbstractRepository<int, Client>
    {
        public ClientRepository(IValidator<Client> validator) : base(validator)
        {
        }
    }
}
