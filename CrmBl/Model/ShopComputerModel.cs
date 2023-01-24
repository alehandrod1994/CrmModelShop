using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrmBl.Model
{
    public class ShopComputerModel
    {
        private Generator _generator = new Generator();
        private Random _rnd = new Random();
        private bool _isWorking = false;

        public ShopComputerModel()
        {
            CashDesks = new List<CashDesk>();
            Carts = new List<Cart>();
            Checks = new List<Check>();
            Sells = new List<Sell>();
            Sellers = new Queue<Seller>();
            CustomerSpeed = 100;
            CashDeskSpeed = 100;

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
        public int CustomerSpeed { get; set; }
        public int CashDeskSpeed { get; set; }

        public void Start()
        {
            _isWorking = true;
            Task.Run(() => CreateCarts(10, CustomerSpeed));

            var cashDeskTasks = CashDesks.Select(c => new Task(() => CashDeskWork(c, CashDeskSpeed)));
            foreach (var task in cashDeskTasks)
            {
                task.Start();
            }
        }

        public void Stop()
        {
            _isWorking = false;
        }

        private void CashDeskWork(CashDesk cashDesk, int sleep)
        {
            while (_isWorking)
            {
                if (cashDesk.Count > 0)
                {
                    cashDesk.Dequeue();
                    Thread.Sleep(sleep);
                }
            }
        }

        private void CreateCarts(int customerCounts, int sleep)
        {
            while (_isWorking)
            {
                var customers = _generator.GetNewCustomers(customerCounts);

                foreach (var customer in customers)
                {
                    var cart = new Cart(customer);

                    foreach (var product in _generator.GetRandomProducts(10, 30))
                    {
                        cart.Add(product);
                    }

                    //var min = CashDesks.Min(c => c.Count);
                    //var cash = CashDesks.FirstOrDefault(c => c.Count == min);
                    var cash = CashDesks[_rnd.Next(CashDesks.Count)];
                    cash.Enqueue(cart);
                }

                Thread.Sleep(sleep);
            }
        }
    }
}
