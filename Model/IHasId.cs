using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Model
{
    public interface IHasId<T>
    {
        T Id { get; set; }
    }
}
