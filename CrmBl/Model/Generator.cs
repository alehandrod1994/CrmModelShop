using System;
using System.Collections.Generic;

namespace CrmBl.Model
{
    public class Generator
    {
        private Random _rnd = new Random();

        public Generator()
        {
            Customers = new List<Customer>();
            Products = new List<Product>();
            Sellers = new List<Seller>();
        }
      
        public List<Customer> Customers { get; set; }
        public List<Product> Products { get; set; }
        public List<Seller> Sellers { get; set; }

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

        private static string GetRandomText()
        {
            return Guid.NewGuid().ToString().Substring(0, 5);
        }
    }
}
