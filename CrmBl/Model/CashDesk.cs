using System;
using System.Collections.Generic;

namespace CrmBl.Model
{
    public class CashDesk
    {
        private CrmContext _db = new CrmContext();

        public CashDesk(int number, Seller seller)
        {
            Number = number;
            Seller = seller;
            Queue = new Queue<Cart>();
            IsModel = true;
            MaxQueueLength = 10;
        }

        public int Number { get; set; }
        public Seller Seller { get; set; }
        public Queue<Cart> Queue { get; set; }
        public int MaxQueueLength { get; set; }
        public int ExitCustomer { get; set; }
        public bool IsModel { get; set; }
        public int Count => Queue.Count;
        public event EventHandler<Check> CheckClosed;

        public void Enqueue(Cart cart)
        {
            if (Queue.Count <= MaxQueueLength)
            {
                Queue.Enqueue(cart);
            }
            else
            {
                ExitCustomer++;
            }
        }

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
