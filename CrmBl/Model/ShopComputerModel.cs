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
        private List<Task> _tasks = new List<Task>();
        private CancellationTokenSource _cancelTokenSoure = new CancellationTokenSource();
        private CancellationToken _token;

        public ShopComputerModel()
        {
            CashDesks = new List<CashDesk>();
            Carts = new List<Cart>();
            Checks = new List<Check>();
            Sells = new List<Sell>();
            Sellers = new Queue<Seller>();

            _token = _cancelTokenSoure.Token;

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
                CashDesks.Add(new CashDesk(CashDesks.Count, Sellers.Dequeue(), null));
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
            _tasks.Add(new Task(() => CreateCarts(10)));
            _tasks.AddRange(CashDesks.Select(c => new Task(() => CashDeskWork(c))));           
            foreach (var task in _tasks)
            {
                task.Start();
            }
        }

        public void Stop()
        {
            _cancelTokenSoure.Cancel();           
        }

        private void CashDeskWork(CashDesk cashDesk)
        {
            while (!_token.IsCancellationRequested)
            {
                if (cashDesk.Count > 0)
                {
                    cashDesk.Dequeue();
                    Thread.Sleep(CashDeskSpeed);
                }
            }
        }

        private void CreateCarts(int customerCounts)
        {
            while (!_token.IsCancellationRequested)
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

                Thread.Sleep(CustomerSpeed);
            }
        }
    }
}
