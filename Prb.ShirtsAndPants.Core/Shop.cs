using System;
using System.Collections.Generic;
using System.Text;

namespace Prb.ShirtsAndPants.Core
{
    public class Shop
    {
        private List<Customer> customers;
        private List<Sale> sales;
        private List<Sale> register;
        private List<string> sizes;
        private List<string> brands;

        public List<Customer> Customers
        {
            get { return customers; }
        }

        public List<Sale> Sales
        {
            get { return sales; }
            // hier zitten alle verkopen in 
        }

        public List<Sale> Register
        {
            get { return register; }
            // deze lijst wordt dan verzonden naar de list Sales
        }

        public List<string> Sizes
        {
            get { return sizes; }
        }

        public List<string> Brands
        {
            get { return brands; }
        }
        
        public Shop() // de vijf lists gaan instantiëren
        {
            customers = new List<Customer>();
            sales = new List<Sale>();
            register = new List<Sale>(); // is de kassa
            sizes = new List<string>();
            brands = new List<string>();
            DoSeeding();
        }

        private void DoSeeding()
        {
            customers.Add(new Customer("Kamiel", "Kafka", "Oostende", Gender.Male, "L", "XL"));
            customers.Add(new Customer("Guy", "Taar", "Beernem", Gender.Male, "XXL", "XXL"));
            customers.Add(new Customer("Marie", "Taar", "Beernem", Gender.Female, "S", "XS"));

            sizes.Add("XS");
            sizes.Add("S");
            sizes.Add("M");
            sizes.Add("L");
            sizes.Add("XL");
            sizes.Add("XXL");
            sizes.Add("XXXL");

            brands.Add("Adidas");
            brands.Add("Priba");
            brands.Add("BonMarche");
            brands.Add("Brooklyn");
            brands.Add("49");
            brands.Add("Denim");
            brands.Add("Louis Vuitton");
        }

        public List<Sale> CustomerHistory (Customer customer, out decimal totalSalePrice) //35:58
        {
            totalSalePrice = 0M;
            List<Sale> CustomerSales = new List<Sale>();
            foreach (Sale sale in sales)
            {
                if (sale.Customer == customer)
                {
                    CustomerSales.Add(sale);
                    totalSalePrice += sale.Price;
                }
            }
            return CustomerSales;
        }

        public decimal CurrentRegisterTotal()
        {
            decimal totalPrice = 0;
            foreach (Sale sale in register)
            {
                totalPrice += sale.Price;
            }
            return totalPrice;
        }

        public string RegisterSale(Customer customer, ProductType productType, string brand, string size, decimal price) // als je op het knopje verkoop hemd/broek drukt (40:55) (zelf salesDate toegevoegd..)
        {
            try
            {
                register.Add(new Sale(customer, productType, brand, size, price));
                return "";
            }
            catch (Exception exception)
            {
                return exception.Message;
            }
        }

        public void ConvertRegisterToSales()
        {
            foreach (Sale sale in register)
            {
                sales.Add(sale);
            }
            register.Clear();
        }

        public void UpdateCustomerSizes(Customer customer, string shirtSize, string pantsSize)
        {
            customer.ShirtSize = shirtSize;
            customer.PantsSize = pantsSize;
        }
    }
}
