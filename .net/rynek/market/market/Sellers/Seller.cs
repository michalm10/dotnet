using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace market
{
    public class Seller : IObserver<double>, IObservable<(Seller, Dictionary<Product, double>)>
    {
        public double income { get; set; } = 0;
        private static int _Id = 0;
        private Bank bank;
        public int Id { get; set; }
        public int maxItems { get; set; } = 5;
        public double inflation { get; set; }
        private IDisposable _unsubscriber;
        public Dictionary<Product, (int, int)> sales = new Dictionary<Product, (int, int)>();
        public Dictionary<Product, double> products { get; } = new Dictionary<Product, double>();
        public List<IObserver<(Seller, Dictionary<Product, double>)>> buyers;

        public Seller(IObservable<double> provider, int items) {
            Id = _Id++;
            _unsubscriber = provider.Subscribe(this);
            buyers = new List<IObserver<(Seller, Dictionary<Product, double>)>>();
            bank = (Bank)provider;
            maxItems= items;
        }

        public void Subscribe(IObservable<double> provider)
        {
            _unsubscriber ??= provider.Subscribe(this);
        }

        public void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }

        public IDisposable Subscribe(IObserver<(Seller, Dictionary<Product, double>)> observer)
        {
            if (!buyers.Contains(observer))
            {
                buyers.Add(observer);
            }
            return new seller.Unsubscriber(buyers, observer);
        }

        public void updateProducts()
        {
            foreach (var obsv in buyers)
            {
                obsv.OnNext((this, this.products));
            }
        }

        double calcMargin(Product product)
        {
            var random = new Random();
            (int, int) sale;            
            double margin = 1 + (double) random.Next(1, 10) / 100;
            if (sales.TryGetValue(product, out sale))
            {
                margin += (double)sale.Item2 / sale.Item1 / 4;
            }
            return margin;
        }

        double calcPrice(Product product)
        {
            ProductVisitor pv = new ProductVisitorImpl();
            double cost = product.Accept(pv);
            return cost * calcMargin(product) * (inflation);
        }

        public void turn()
        {
            var toRem = new List<Product>();
            foreach (var product in products)
            {
                (int, int) sale;
                if (sales.TryGetValue(product.Key, out sale))
                {
                    sales[product.Key] = (++sale.Item1, sale.Item2);
                    //Console.WriteLine((double)sale.Item2 / sale.Item1);
                    if ((double)(sale.Item2 + (double) product.Value / 40) / sale.Item1 < 2.5)
                        toRem.Add(product.Key);   
                }
            }
            foreach (var t in toRem)
            {
                //Console.WriteLine($"Sprzedawca {Id} usunął produkt {t.Id}, {sales[t]}, {t.GetType()}");
                products.Remove(t);
            }
            foreach (var kvp in products)
            {
                products[kvp.Key] = calcPrice(kvp.Key);
            }
            updateProducts();
        }

        public void addProduct(Product product)
        {
            if (!products.ContainsKey(product)) products.Add(product, calcPrice(product));
            if (!sales.ContainsKey(product)) sales.Add(product, (0, 0));
        }

        public string buyProduct(Product product, Buyer buyer)
        {
            if (products.ContainsKey(product))
            {
                var sale = sales[product];
                sales[product] = (sale.Item1, ++sale.Item2);
                bank.transaction(products[product]);
                return $"\tKupujący {buyer} kupił od sprzedawcy {Id} produkt {product.Id} za {products[product]}, {sales[product]}";
            }
            return "";
        }

        public void OnNext(double inflation) => this.inflation = inflation;

        public void OnCompleted() { }

        public void OnError(Exception error) { }

        public override string ToString()
        {
            return $"id: {Id}, maxitems: {maxItems}, inf: {inflation}, \nprod:\n{string.Join("\n", products)}, \nbuyers: {buyers.Count()})" ;
        }
    }
}
