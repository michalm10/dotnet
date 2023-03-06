using System;

namespace market.seller
{
    internal class Unsubscriber : IDisposable
    {
        private List<IObserver<(Seller, Dictionary<Product, double>)>> _observers;
        private IObserver<(Seller, Dictionary<Product, double>)> _observer;

        internal Unsubscriber(List<IObserver<(Seller, Dictionary<Product, double>)>> observers, IObserver<(Seller, Dictionary<Product, double>)> observer)
        {
            this._observers = observers;
            this._observer = observer;
        }

        public void Dispose()
        {
            if (_observers.Contains(_observer))
                _observers.Remove(_observer);
        }
    }
}


