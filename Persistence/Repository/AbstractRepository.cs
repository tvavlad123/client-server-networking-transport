using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transport.Model;

namespace Transport.Repository
{
    public abstract class AbstractRepository<ID, T> : IRepository<ID, T> where T : class, IHasId<ID>
    {
        private readonly IDictionary<ID, T> _items;
        private readonly IValidator<T> _validator;

        protected AbstractRepository(IValidator<T> validator)
        {
            _items = new Dictionary<ID, T>();
            _validator = validator;
        }

        public void Delete(ID id)
        {
            _items.Remove(id);
        }

        public void Update(ID id, T entity)
        {
            _validator.Validate(entity);
            if (!Equals(id, entity.Id))
                if (!_items.ContainsKey(entity.Id))
                    Delete(id);
                else
                    throw new RepositoryException("Cannot update entity");
            _items[entity.Id] = entity;
        }

        public IEnumerable<T> FindAll()
        {
            return _items.Values;
        }

        public int Size()
        {
            return _items.Count;
        }

        public T FindOne(ID id)
        {
            if (!_items.ContainsKey(id))
                throw new RepositoryException("Item not found");
            return _items[id];
        }

        public ID Save(T entity)
        {
            _validator.Validate(entity);
            _items[entity.Id] = entity;
            return entity.Id;
        }
    }
}
