using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace market
{
    public class Bank : IObservable<double>
    {
        public int allTransactions { get; set; } = 0;
        public double allTransactionSum { get; set; } = 0;
        public int transactionsTurn { get; set; } = 0;
        public double transactionSum{ get; set; } = 0;
        public double inflation { get; set; } = 1;

        List<IObserver<double>> observers;
        public Bank() {
            observers = new List<IObserver<double>>();
        }

        public IDisposable Subscribe(IObserver<double> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new bank.Unsubscriber(observers, observer);
        }

        private void updateInflation(double inflation)
        {
            this.inflation = inflation;
            foreach (var obsv in observers)
            {
                obsv.OnNext(inflation);
            }
        }

        public void transaction(double sum)
        {
            transactionsTurn++;
            allTransactions++;
            transactionSum += sum;
            allTransactionSum += sum;
        }

        public void turn()
        {
            updateInflation(inflation);
            if (allTransactions == 0) return;
            if (transactionSum / transactionsTurn <= allTransactionSum / allTransactions)
            {
                updateInflation(inflation * 1.05);
            }
            else
            {
                updateInflation(inflation * 0.96);
            }
            transactionsTurn = 0;
            transactionSum = 0;
        }
    }
}
