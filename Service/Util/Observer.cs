using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transport.Util
{
    public interface IObserver<T>
    {
        void NotifyOnEvent();
    }

    public interface IObservable<T>
    {
        void AddObserver(IObserver<T> e);
        void RemoveObserver(IObserver<T> e);
        void NotifyObservers();
    }
}
