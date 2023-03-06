using market;
using System;

public class Test
{
    public static void Main()
    {
        Market market= new Market();

        Product p1 = new Smartphone("as", 133, 50);
        Product p2 = new Smartphone("as", 243, 71);
        Product p3 = new Grocery("as", 32, 12);
        Product p4 = new Grocery("as", 32, 12);
        Product p5 = new Jacket("as", 90, 15);
        Product p6 = new Jacket("asd", 101, 10);
        Product p7 = new Computer("dfs", 322, 123);
        Product p8 = new Computer("dfs", 322, 123);
        Product p9 = new Shoes("sadsf", 123, 12);
        Product p10 = new Shoes("sadsf", 123, 12);
        Product p11 = new FoodDelivery("asdf", 45, 8);
        Product p12 = new FoodDelivery("asdf", 45, 8);

        market.products.Add(p1);
        market.products.Add(p2);
        market.products.Add(p3);
        market.products.Add(p4);
        market.products.Add(p5);
        market.products.Add(p6);
        market.products.Add(p7);
        market.products.Add(p8);
        market.products.Add(p9);
        market.products.Add(p10);
        market.products.Add(p11);
        market.products.Add(p12);

        Seller s1 = new Seller(market.bank, 6);
        Seller s2 = new Seller(market.bank, 5);
        Seller s3 = new Seller(market.bank, 4);

        market.sellers.Add(s1);
        market.sellers.Add(s2);
        market.sellers.Add(s3);

        Need n1 = new market.Needs.Clothes("asdf", new List<Type>{ typeof(Shoes), typeof(Jacket) }, 0.4);
        Need n2 = new market.Needs.Electronics("asdf", new List<Type> { typeof(Smartphone), typeof(Computer) }, 0.3);
        Need n3 = new market.Needs.Food("asdf", new List<Type> { typeof(Grocery), typeof(FoodDelivery) }, 0.6);

        Buyer b1 = new Buyer(market.bank);
        Buyer b2 = new Buyer(market.bank);
        Buyer b3 = new Buyer(market.bank);
        Buyer b4 = new Buyer(market.bank);
        Buyer b5 = new Buyer(market.bank);
        Buyer b6 = new Buyer(market.bank);
        Buyer b7 = new Buyer(market.bank);
        Buyer b8 = new Buyer(market.bank);
        Buyer b9 = new Buyer(market.bank);

        market.buyers.Add(b1);
        market.buyers.Add(b2);
        market.buyers.Add(b3);
        market.buyers.Add(b4);
        market.buyers.Add(b5);
        market.buyers.Add(b6);
        market.buyers.Add(b7);
        //market.buyers.Add(b8);
        //market.buyers.Add(b9);

        b1.addNeed(n1);
        b1.addNeed(n2);
        b1.addNeed(n3);

        b2.addNeed(n3);
        b2.addNeed(n1);

        b3.addNeed(n2);
        b3.addNeed(n3);

        b4.addNeed(n2);

        b5.addNeed(n1);

        b6.addNeed(n3);

        b7.addNeed(n2);
        b7.addNeed(n1);

        b8.addNeed(n1);
        b8.addNeed(n2);
        b8.addNeed(n3);

        b9.addNeed(n1);
        b9.addNeed(n2);
        b9.addNeed(n3);

        b1.Subscribe(new List<Seller> { s1, s2, s3 });
        b2.Subscribe(new List<Seller> { s1, s2, s3 });
        b3.Subscribe(new List<Seller> { s1, s2, s3 });
        b4.Subscribe(new List<Seller> { s1, s2, s3 });
        b5.Subscribe(new List<Seller> { s1, s2, s3 });
        b6.Subscribe(new List<Seller> { s1, s2, s3 });
        b7.Subscribe(new List<Seller> { s1, s2, s3 });
        b8.Subscribe(new List<Seller> { s1, s2, s3 });
        b9.Subscribe(new List<Seller> { s1, s2, s3 });

        for (int i = 0; i < 1000; i++)
        {
            market.nextTurn();
            Console.WriteLine(market);
        }
    }
}
