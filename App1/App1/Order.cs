using System;
using System.ComponentModel;
using System.Collections.Generic;

namespace App1.DataModel
{
    public class Order : ModelObject
    {
        DateTime date;
        bool shipped;
        Product product;
        int quantity;

        public Order()
        {
            this.date = DateTime.Today;
            this.shipped = false;
            this.product = new Product("", 0);
            this.Quantity = 0;
        }

        public Order(DateTime date, bool shipped, Product product, int quantity)
        {
            this.date = date;
            this.shipped = shipped;
            this.product = product;
            this.quantity = quantity;
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    RaisePropertyChanged("Date");
                }
            }
        }

        public bool Shipped
        {
            get { return shipped; }
            set
            {
                if (shipped != value)
                {
                    shipped = value;
                    RaisePropertyChanged("Shipped");
                }
            }
        }

        public Product Product
        {
            get { return product; }
            set
            {
                if (product != value)
                {
                    product = value;
                    RaisePropertyChanged("Product");
                }
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    RaisePropertyChanged("Quantity");
                }
            }
        }
    }

    public class Product : ModelObject
    {
        string name;
        int unitPrice;

        public Product(string name, int unitPrice)
        {
            this.name = name;
            this.unitPrice = unitPrice;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int UnitPrice
        {
            get { return unitPrice; }
            set { unitPrice = value; }
        }
    }

    public class ModelObject : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
    public abstract class OrderRepository
    {
        readonly BindingList<Order> orders;

        public OrderRepository()
        {
            this.orders = new BindingList<Order>();
        }

        public BindingList<Order> Orders
        {
            get { return orders; }
        }
    }

    public class TestOrderRepository : OrderRepository
    {

        const int orderCount = 100;
        readonly List<Product> products;
        readonly Random random;

        public TestOrderRepository() : base()
        {
            this.random = new Random((int)DateTime.Now.Ticks);
            this.products = new List<Product>();

            GenerateProducts();

            for (int i = 0; i < orderCount; i++)
                Orders.Add(GenerateOrder(i));
        }

        Order GenerateOrder(int number)
        {
            Order order = new Order(new DateTime(2020, 1, 1).AddDays(random.Next(0, 60)),
                number % 3 == 0, RandomItem<Product>(products), random.Next(1, 100));
            return order;
        }

        T RandomItem<T>(IList<T> list)
        {
            int index = (int)(random.NextDouble() * 0.99 * (list.Count));
            return list[index];
        }

        void GenerateProducts()
        {
            products.Add(new Product("Tofu", 50));
            products.Add(new Product("Chocolade", 34));
            products.Add(new Product("Ikura", 70));
            products.Add(new Product("Chai", 3));
            products.Add(new Product("Boston Crab Meat", 36));
            products.Add(new Product("Ravioli Angelo", 18));
            products.Add(new Product("Ipon Coffee", 10));
            products.Add(new Product("Queso Cabrales", 25));
        }
    }
}
