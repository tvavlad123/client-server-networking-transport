using System;
using System.Collections.Generic;
using System.Linq;
using Transport.Model;
using Transport.Repository;

namespace Transport.Service
{
    public abstract class AbstractService<ID, T> : IService<ID, T>, Util.IObservable<T> where T : IHasId<ID>
    {
        private readonly IRepository<ID, T> _repository;
        private readonly List<Util.IObserver<T>> _observers;

        protected AbstractService(IRepository<ID, T> repository)
        {
            _repository = repository;
            _observers = new List<Util.IObserver<T>>();
        }

        public void Add(T item)
        {
            _repository.Save(item);
            NotifyObservers();
        }

        public void Update(ID id, T item)
        {
            _repository.Update(id, item);
            NotifyObservers();
        }

        public void Remove(ID id)
        {
            _repository.Delete(id);
            NotifyObservers();
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.FindAll();
        }

        public T Find(ID id)
        {
            try
            {
                return _repository.FindOne(id);
            }
            catch
            {
                //ignored
            }
            return default(T);
        }

        public List<T> FilterAndSorter(List<T> lista, Func<T, bool> filter, Func<T, object> key)
        {
            return lista.Where(filter).OrderBy(key).ToList();
        }

        public int Size()
        {
            return _repository.Size();
        }

        public void AddObserver(Util.IObserver<T> e)
        {
            _observers.Add(e);
        }

        public void RemoveObserver(Util.IObserver<T> e)
        {
            _observers.Remove(e);
        }

        public void NotifyObservers()
        {
            _observers.ForEach(x => x.NotifyOnEvent());
        }
    }
}
