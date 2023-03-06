using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace market
{
    public class Buyer : IObserver<double>, IObserver<(Seller, Dictionary<Product, double>)>
    {
        public class buyerNeed
        {
            internal Need need { get; set; }
            internal List<Type> types { get; set; } = new List<Type>();
            internal double frequency { get; set; } = 0;
            internal double times { get; set; } = 0;

            public buyerNeed(Need need)
            {
                NeedVisitor nv = new NeedVisitorImpl();
                (List<Type>, double, int) n = need.Accept(nv);
                types = n.Item1;
                frequency = n.Item2;
                this.need = need;
                times = n.Item3;
            }
        }
        private static int _Id = 0;
        public int Id { get; set; } 
        public double inflation { get; set; }
        private IDisposable _unsubscriberBank;
        public Dictionary<Seller, Dictionary<Product, double>> products { get; } = new Dictionary<Seller, Dictionary<Product, double>>();
        public List<buyerNeed> needs { get; } = new List<buyerNeed>();

        public Buyer(IObservable<double> provider)
        {
            Id = _Id++;
            _unsubscriberBank = provider.Subscribe(this);
        }

        public void Subscribe(IObservable<double> provider)
        {
            _unsubscriberBank ??= provider.Subscribe(this);
        }

        public void Subscribe(Seller seller)
        {
            if (!products.ContainsKey(seller))
            {
                seller.Subscribe(this);
                products.Add(seller, new Dictionary<Product, double>());
            }
        }

        public void Subscribe(List<Seller> sellers)
        {
            foreach (var seller in sellers)
                if (!products.ContainsKey(seller))
                {
                    seller.Subscribe(this);
                    products.Add(seller, new Dictionary<Product, double>());
                }
        }

        public void UnsubscribeBank()
        {
            _unsubscriberBank.Dispose();
        }

        public void UnsubscribeSeller(Seller seller)
        {
            if (products.ContainsKey(seller))
            {
                products.Remove(seller);
            }
        }

        public string buy(Seller seller, Product product)
        {
            if (products.ContainsKey(seller))
            {
                if (products[seller].ContainsKey(product))
                {
                    return seller.buyProduct(product, this);
                }
            }
            return "";
        }

        public List<string> turn()
        {
            var random = new Random();
            List<string> transactions = new List<string>();
            var offers = new Dictionary<buyerNeed, List<(Seller, Product, double)>>();
            foreach (var need in needs)
            {
                var off = new List<(Seller, Product, double)>();
                foreach (var seller in products.OrderBy(_ => random.Next()).ToList())
                {
                    foreach (var pr in seller.Value)
                    {
                        if (need.types.Any(x=> x == pr.Key.GetType()))
                            off.Add((seller.Key, pr.Key, pr.Value));
                    }
                }
                offers.Add(need, off);
            }

            foreach (var offer in offers)
            {
                var times = offer.Key.times;
                while (times > 0)
                {
                    if ((double)random.Next(1, 100) / 100 <= offer.Key.frequency && offer.Value.Count > 0)
                    {
                        var transaction = offer.Value.MinBy(x=> x.Item3);
                        transactions.Add(buy(transaction.Item1, transaction.Item2) + $", ({offer.Key.need.GetType()})");
                    }
                    times--;
                }
            }
            return transactions;
        }

        public void addNeed(Need need) { if (!needs.Any(x => x.need == need)) needs.Add(new buyerNeed(need)); }

        public void OnNext(double inflation) => this.inflation = inflation;

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public void OnNext((Seller, Dictionary<Product, double>)a) => products[a.Item1] = a.Item2;

        public override string ToString()
        {
            //return $"id: {Id}, inf: {inflation}, \nprod:\n{products.Count()}, \nneeds:\n{string.Join("\n", needs)}";
            return $"{Id}";
        }
    }
}
