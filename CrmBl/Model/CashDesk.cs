using System;
using System.Collections.Generic;

namespace CrmBl.Model
{
	/// <summary>
	/// Касса.
	/// </summary>
    public class CashDesk
    {
		/// <summary>
		/// Контекст БД.
		/// </summary>
        private readonly CrmContext _db;

		/// <summary>
		/// Инициализирует новый экземпляр класса CashDesk.
		/// </summary>
		/// <param name="number"> Номер. </param>
		/// <param name="seller"> Продавец. </param>
		/// <param name="db"> Контекст БД. </param>
        public CashDesk(int number, Seller seller, CrmContext db)
        {
            Number = number;
            Seller = seller;
            Queue = new Queue<Cart>();
            IsModel = true;
            MaxQueueLength = 10;
            _db = db ?? new CrmContext();
        }

		/// <summary>
		/// Номер.
		/// </summary>
		
        public int Number { get; set; }
		
		/// <summary>
		/// Продавец.
		/// </summary>
        public Seller Seller { get; set; }
		
		/// <summary>
		/// Очередь.
		/// </summary>
        public Queue<Cart> Queue { get; set; }
		
		/// <summary>
		/// Максимальная длина очереди.
		/// </summary>
        public int MaxQueueLength { get; set; }
		
		/// <summary>
		/// Количество покупателей, которые покинули магазин до оплаты ввиду максимальной очереди в кассе.
		/// </summary>
        public int ExitCustomer { get; set; }
		
		/// <summary>
		/// Указывает, включено ли компьютерное моделирование.
		/// </summary>
        public bool IsModel { get; set; }
		
		/// <summary>
		/// Количество покупателей в очереди.
		/// </summary>
        public int Count => Queue.Count;
		
		/// <summary>
		/// Событие, которое происходит после закрытия чека.
		/// </summary>
        public event EventHandler<Check> CheckClosed;

        /// <summary>
        /// Добавляет покупателей с товарами.
        /// </summary>
        /// <param name="cart"> Корзина покупателя с товарами. </param>
        public void Enqueue(Cart cart)
        {
            if (Queue.Count < MaxQueueLength)
            {
                Queue.Enqueue(cart);
            }
            else
            {
                ExitCustomer++;
            }
        }

        /// <summary>
        /// Производит оплату товаров.
        /// </summary>
        /// <returns> Стоимость. </returns>
        public decimal Dequeue()
        {
            decimal sum = 0;

            if (Queue.Count == 0)
            {
                return 0;
            }

            var cart = Queue.Dequeue();

            if (cart != null)
            {
                var check = new Check()
                {
                    Seller = Seller,
                    SellerID = Seller.ID,
                    Customer = cart.Customer,
                    CustomerID = cart.Customer.ID,
                    Created = DateTime.Now
                };

                if (!IsModel)
                {
                    _db.Checks.Add(check);
                    _db.SaveChanges();
                }
                else
                {
                    check.ID = 0;
                }

                var sells = new List<Sell>();
                foreach (Product product in cart)
                {
                    if (product.Count > 0)
                    {
                        var sell = new Sell()
                        {
                            CheckID = check.ID,
                            Check = check,
                            ProductID = product.ID,
                            Product = product
                        };
                        sells.Add(sell);

                        if (!IsModel)
                        {
                            _db.Sells.Add(sell);
                        }

                        product.Count--;
                        sum += product.Price;
                    }
                }

                check.Price = sum;

                if (!IsModel)
                {
                    _db.SaveChanges();
                }

                CheckClosed?.Invoke(this, check);
            }

            return sum;
        }

        public override string ToString()
        {
            return $"Касса №{Number}";
        }
    }
}
