using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Model;

namespace Transport.Repository
{
    public interface IRepository<ID, T> where T : IHasId<ID>
    {
        int Size();
        T FindOne(ID id);
        IEnumerable<T> FindAll();
        ID Save(T entity);
        void Delete(ID id);
        void Update(ID id, T entity);
    }

    public class RepositoryException : Exception
    {
        public RepositoryException(string msg) : base(msg)
        {

        }
    }
}
