using System;
using System.Collections.Generic;
using System.Text;

namespace Prb.ShirtsAndPants.Core
{
    public enum ProductType { Shirt, Pants}
   public class Sale
    {
        private Customer customer;
        private DateTime salesDate;
        private ProductType productType;
        private string brand;
        private string size;
        private decimal price;

        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }

        public DateTime SalesDate
        {
            get { return salesDate; }
            set { salesDate = value; }
        }

        public ProductType ProductType
        {
            get { return productType; }
            set { productType = value; }
        }

        public string Brand
        {
            get { return brand; }
            set
            {
                value = value.Trim();
                if (value == "")
                {
                    throw new Exception("brand : er werd geen merk meegegeven!");
                }
                else
                {
                    brand = value;
                }
            }
        }

        public string Size
        {
            get { return size; }
            set
            {
                value = value.Trim();
                if (value == "")
                {
                    throw new Exception("size : er werd geen maat meegegeven!");
                }
                else
                {
                    size = value;
                }
            }
        }

        public decimal Price
        {
            get { return price; }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("price : prijs ongeldig. De prijs moet groter zijn dan nul!");
                }
                else
                {
                    price = value;
                }
            }
        }

        public Sale(Customer customer, ProductType productType, string brand, string size, decimal price) //, DateTime salesDate)
        {
            this.customer = customer;
            this.productType = ProductType;
            Brand = brand;
            Size = size;
            Price = price;
            //this.salesDate = DateTime.Now;
        }

        public override string ToString()
        {
            return $"{productType.ToString()} - {brand} - € {price.ToString("#,##0.00")} - {DateTime.Now}";
        }
    }
}
