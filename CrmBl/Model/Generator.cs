using System;
using System.Collections.Generic;

namespace CrmBl.Model
{
	/// <summary>
	/// Генератор.
	/// </summary>
    public class Generator
    {
		/// <summary>
		/// Генератор случайных чисел.
		/// </summary>
        private readonly Random _rnd = new Random();

		/// <summary>
		/// Инициализирует новый экземпляр класса Generator.
		/// </summary>
        public Generator()
        {
            Customers = new List<Customer>();
            Products = new List<Product>();
            Sellers = new List<Seller>();
        }
      
		/// <summary>
		/// Покупатели.
		/// </summary>
        public List<Customer> Customers { get; set; }

        /// <summary>
        /// Товары.
        /// </summary>
        public List<Product> Products { get; set; }
		
		/// <summary>
		/// Продавцы.
		/// </summary>
        public List<Seller> Sellers { get; set; }

		/// <summary>
		/// Генерирует список новых покупателей.
		/// </summary>
		/// <param name="count"> Количество. </param>
		/// <returns> Список покупателей. </returns>
        public List<Customer> GetNewCustomers(int count)
        {
            var result = new List<Customer>();

            for (int i = 0; i < count; i++)
            {
                var customer = new Customer()
                {
                    ID = Customers.Count,
                    Name = GetRandomText()
                };
                Customers.Add(customer);
                result.Add(customer);
            }

            return result;
        }

		/// <summary>
		/// Генерирует список новых продавцов.
		/// </summary>
		/// <param name="count"> Количество. </param>
		/// <returns> Список продавцов. </returns>
        public List<Seller> GetNewSellers(int count)
        {
            var result = new List<Seller>();

            for (int i = 0; i < count; i++)
            {
                var seller = new Seller()
                {
                    ID = Sellers.Count,
                    Name = GetRandomText()
                };
                Sellers.Add(seller);
                result.Add(seller);
            }

            return result;
        }

        /// <summary>
        /// Генерирует список новых товаров.
        /// </summary>
        /// <param name="count"> Количество. </param>
        /// <returns> Список товаров. </returns>
        public List<Product> GetNewProducts(int count)
        {
            var result = new List<Product>();

            for (int i = 0; i < count; i++)
            {
                var product = new Product()
                {
                    ID = Products.Count,
                    Name = GetRandomText(),
                    Count = _rnd.Next(10, 1000),
                    Price = Convert.ToDecimal(_rnd.Next(5, 100000) + _rnd.NextDouble())
                };
                Products.Add(product);
                result.Add(product);
            }

            return result;
        }

        /// <summary>
        /// Получает список случайных товаров.
        /// </summary>
        /// <param name="min"> Минимальное количество. </param>
        /// <param name="max"> Максимальное количество. </param>
        /// <returns> Список товаров. </returns>
        public List<Product> GetRandomProducts(int min, int max)
        {
            var result = new List<Product>();

            var count = _rnd.Next(min, max);
            for (int i = 0; i < count; i++)
            {
                result.Add(Products[_rnd.Next(Products.Count - 1)]);
            }

            return result;
        }

		/// <summary>
		/// Генерирует случайный текст.
		/// </summary>
		/// <returns> Текст. </returns>
        private static string GetRandomText()
        {
            return Guid.NewGuid().ToString().Substring(0, 5);
        }
    }
}
