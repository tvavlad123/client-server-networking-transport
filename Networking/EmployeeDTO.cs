using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    [Serializable]
    public sealed class EmployeeDTO
    {
        public string UserName { get; set; }
        public string Password { get; }

        public EmployeeDTO(string username, string password)
        {
            UserName = username;
            Password = password;
        }
    }
}
