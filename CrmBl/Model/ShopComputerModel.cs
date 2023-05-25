using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrmBl.Model
{
	/// <summary>
	/// Компьютерное моделирование.
	/// </summary>
    public class ShopComputerModel
    {
		/// <summary>
		/// Генератор.
		/// </summary>
        private readonly Generator _generator = new Generator();
		
		/// <summary>
		/// Генератор случайных чисел.
		/// </summary>
        private readonly Random _rnd = new Random();
		
		/// <summary>
		/// Список асинхронных задач.
		/// </summary>
        private readonly List<Task> _tasks = new List<Task>();
		
		/// <summary>
		/// Токен для остановки моделирования.
		/// </summary>
        private readonly CancellationTokenSource _cancelTokenSource;
        private CancellationToken _token;

		/// <summary>
		/// Инициализирует новый экземпляр класса ShopComputerModel.
		/// </summary>
        public ShopComputerModel()
        {
            CashDesks = new List<CashDesk>();
            Carts = new List<Cart>();
            Checks = new List<Check>();
            Sells = new List<Sell>();
            Sellers = new Queue<Seller>();

            _cancelTokenSource = new CancellationTokenSource();
            _token = _cancelTokenSource.Token;

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

		/// <summary>
		/// Кассы.
		/// </summary>
        public List<CashDesk> CashDesks { get; set; }
		
		/// <summary>
		/// Корзины.
		/// </summary>
        public List<Cart> Carts { get; set; }
		
		/// <summary>
		/// Чеки.
		/// </summary>
        public List<Check> Checks { get; set; }
		
		/// <summary>
		/// Продажи.
		/// </summary>
        public List<Sell> Sells { get; set; }
		
		/// <summary>
		/// Продавцы.
		/// </summary>
        public Queue<Seller> Sellers { get; set; }
		
		/// <summary>
		/// Скорость покупателей.
		/// </summary>
        public int CustomerSpeed { get; set; }
		
		/// <summary>
		/// Скорость кассы.
		/// </summary>
        public int CashDeskSpeed { get; set; }

		/// <summary>
		/// Начинает моделирование.
		/// </summary>
        public void Start()
        {
            _tasks.Add(new Task(() => CreateCarts(10)));
            _tasks.AddRange(CashDesks.Select(c => new Task(() => CashDeskWork(c))));           
            foreach (var task in _tasks)
            {
                task.Start();
            }
        }

		/// <summary>
		/// Останавливает моделирование.
		/// </summary>
        public void Stop()
        {
            _cancelTokenSource.Cancel();
            Thread.Sleep(1000);
            _cancelTokenSource.Dispose();
        }

		/// <summary>
		/// Идёт работа кассы.
		/// </summary>
		/// <param name="cashDesk"> Касса. </param>
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

		/// <summary>
		/// Создаёт корзины.
		/// </summary>
		/// <param name="customerCounts"> Количество покупателей. </param>
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

                    var cash = CashDesks[_rnd.Next(CashDesks.Count)];
                    cash.Enqueue(cart);
                }

                Thread.Sleep(CustomerSpeed);
            }
        }
    }
}
