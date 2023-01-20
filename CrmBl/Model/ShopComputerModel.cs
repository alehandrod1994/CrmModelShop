using System;
using System.Collections.Generic;
using System.Linq;

namespace CrmBl.Model
{
    public class ShopComputerModel
    {
        private Generator _generator = new Generator();
        private Random _rnd = new Random();

        public ShopComputerModel()
        {
            CashDesks = new List<CashDesk>();
            Carts = new List<Cart>();
            Checks = new List<Check>();
            Sells = new List<Sell>();
            Sellers = new Queue<Seller>();

            var sellers = _generator.GetNewSellers(20);
            _generator.GetNewProducts(1000);
            _generator.GetNewCustomers(100);

            foreach (var seller in sellers)
            {
                Sellers.Enqueue(seller);
            }

            for (int i = 0; i < 3; i++)
            {
                CashDesks.Add(new CashDesk(CashDesks.Count, Sellers.Dequeue()));
            }
        }

        public List<CashDesk> CashDesks { get; set; }
        public List<Cart> Carts { get; set; }
        public List<Check> Checks { get; set; }
        public List<Sell> Sells { get; set; }
        public Queue<Seller> Sellers { get; set; }

        public void Start()
        {
            var customers = _generator.GetNewCustomers(10);

            var carts = new Queue<Cart>();

            foreach (var customer in customers)
            {
                var cart = new Cart(customer);

                foreach (var product in _generator.GetRandomProducts(10, 30))
                {
                    cart.Add(product);
                }

                carts.Enqueue(cart);
            }

            while(carts.Count > 0)
            {
                var min = CashDesks.Min(c => c.Count);
                var cash = CashDesks.FirstOrDefault(c => c.Count == min);
                cash.Enqueue(carts.Dequeue());
            }

            while (true)
            {
                var cash = CashDesks[_rnd.Next(CashDesks.Count - 1)];
                var money = cash.Dequeue();
            }         
        }
    }
}
