using market;
using NUnit.Framework;
using System;

namespace TestMarket
{
    [TestFixture]
    public class MarketTest
    {
        Market? market;
        Product? p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12;
        Seller? s1, s2, s3;
        Need? n1, n2, n3;
        Buyer? b1, b2, b3;

        [SetUp]
        public void setup()
        {
            market = new Market();

            p1 = new Smartphone("as", 43, 21);
            p2 = new Smartphone("as", 43, 21);
            p3 = new Grocery("as", 32, 12);
            p4 = new Grocery("as", 32, 12);
            p5 = new Jacket("as", 38, 10);
            p6 = new Jacket("as", 38, 10);
            p7 = new Computer("dfs", 322, 123);
            p8 = new Computer("dfs", 322, 123);
            p9 = new Shoes("sadsf", 123, 12);
            p10 = new Shoes("sadsf", 123, 12);
            p11 = new FoodDelivery("asdf", 45, 8);
            p12 = new FoodDelivery("asdf", 45, 8);

            market.products.Add(p1);
            market.products.Add(p2);
            market.products.Add(p3);
            market.products.Add(p4);
            market.products.Add(p5);
            market.products.Add(p6);
            market.products.Add(p7);
            market.products.Add(p8);

            s1 = new Seller(market.bank, 6);
            s2 = new Seller(market.bank, 5);
            s3 = new Seller(market.bank, 4);

            market.sellers.Add(s1);
            market.sellers.Add(s2);
            market.sellers.Add(s3);

            n1 = new market.Needs.Clothes("asdf", new List<Type> { typeof(Shoes), typeof(Jacket) }, 0.4);
            n2 = new market.Needs.Electronics("asdf", new List<Type> { typeof(Smartphone), typeof(Computer) }, 0.3);
            n3 = new market.Needs.Food("asdf", new List<Type> { typeof(Grocery), typeof(FoodDelivery) }, 0.6);

            b1 = new Buyer(market.bank);
            b2 = new Buyer(market.bank);
            b3 = new Buyer(market.bank);

            market.buyers.Add(b1);
            market.buyers.Add(b2);
            market.buyers.Add(b3);

            b1.addNeed(n1);
            b1.addNeed(n2);
            b1.addNeed(n3);

            b2.addNeed(n3);
            b2.addNeed(n1);
        }

        [Test]
        public void updateInflationTest()
        {
            market.bank.inflation = 89;
            market.bank.turn();
            Assert.AreEqual(89, b2.inflation);
            Assert.AreEqual(89, s2.inflation);
        }

        [Test]
        public void productVisitorTest()
        {
            ProductVisitor productVisitor = new ProductVisitorImpl();
            Assert.AreEqual(133 + 50, p1.Accept(productVisitor));
        }

        [Test]
        public void needVisitorTest()
        {
            NeedVisitor needVisitor = new NeedVisitorImpl();
            Assert.AreEqual((new List<Type> { typeof(Shoes), typeof(Jacket) }, 0.4, 3), n1.Accept(needVisitor));
        }

        [Test]
        public void sellersObserverUpdate()
        {
            b1.products.Clear();
            s1.buyers.Clear();
            s1.products.Clear();
            b1.Subscribe(s1);
            s1.products.Add(p8, 87);
            s1.updateProducts();
            Assert.AreEqual(87, b1.products[s1][p8]);
            Assert.AreEqual(p8, b1.products[s1].FirstOrDefault(x => x.Value == 87).Key);
        }

        [Test]
        public void sellersObserver()
        {
            b1.products.Clear();
            s1.buyers.Clear();
            b1.Subscribe(s1);
            Assert.AreEqual(s1.buyers[0], b1);
        }

        [Test]
         public void buyTest()
        {
            s1.addProduct(p1);
            b1.Subscribe(s1);
            s1.updateProducts();
            var pr = s1.products.ElementAt(0).Key;
            Console.WriteLine(b1.buy(s1, pr));
            Assert.AreEqual((0, 1), s1.sales[pr]);
        }

        [Test]
        public void runTest()
        {
            for (int i = 0; i < 200; i++)
            {
                market.nextTurn();
            }
        }
    }
}