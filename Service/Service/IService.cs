using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Model;

namespace Transport.Service
{
        public interface IService<in ID, T> where T : IHasId<ID>
        {
            void Add(T item);
            void Update(ID id, T item);
            void Remove(ID id);
            IEnumerable<T> GetAll();
            T Find(ID id);
            List<T> FilterAndSorter(List<T> lista, Func<T, bool> filter, Func<T, object> key);
            int Size();
        }
}
