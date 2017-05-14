using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseManagementSystem.DAL
{
    public class Product
    {
        private int productId;
        private string productName;
        private string itemDescription;
        private string itemCode;
        private decimal price;
        private string quantity;

        public int ProductId
        {
            get { return productId; }
            set { productId = value; }
        }

        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }

        public string ItemDescription
        {
            get { return itemDescription; }
            set { itemDescription = value; }
        }

        public string ItemCode
        {
            get { return itemCode; }
            set { itemCode = value; }
        }

        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public string Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }

    }
}
