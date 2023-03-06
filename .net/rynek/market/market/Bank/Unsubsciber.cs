using System;

namespace market.bank
{
    internal class Unsubscriber : IDisposable
    {
        private List<IObserver<double>> _observers;
        private IObserver<double> _observer;

        internal Unsubscriber(List<IObserver<double>> observers, IObserver<double> observer)
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



