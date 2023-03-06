using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace market
{
    public class Market
    {
        public Market() { }

        public int turn = 0;
        public Bank bank = new Bank();
        public List<Product> products = new List<Product>();
        public List<Seller> sellers = new List<Seller>();
        public List<Need> needs = new List<Need>();
        public List<Buyer> buyers = new List<Buyer>();

        public void setup()
        {

        }

        public void nextTurn()
        {
            var random = new Random();
            bank.turn();
            turn++;
            foreach (var seller in sellers) 
            {
                seller.turn();
                while (seller.products.Count() < seller.maxItems)
                {
                    seller.addProduct(products[random.Next(products.Count)]);
                }
            }
            foreach (var buyer in buyers.OrderBy(_ => random.Next()).ToList())
            {
                var transactions = buyer.turn();
                if (transactions.Count > 0)
                {
                    Console.WriteLine(string.Join('\n', transactions));
                }
            }
        }

        public int getTurn() => turn;

        public override string ToString()
        {
            return $"Tura: {this.turn}, Inflacja: {this.bank.inflation}";
        }
    }
}
