using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportOrderManagementSystem.DAL;
using ImportOrderManagementSystem.DbGateway;

namespace ImportOrderManagementSystem.Gateway
{
   public  class ProductGateway:ConnectionGateway
    {
       public Product SearchProduct(Int32  productId)
       {
           connection.Open();
           string selectQuery = string.Format("Select * from ProductListSummary Where Sl='{0}'", productId);
           SqlCommand cmd = new SqlCommand(selectQuery, connection);
           SqlDataReader daraReader = cmd.ExecuteReader();
           Product aProduct = new Product();
           while (daraReader.Read())
           {
               aProduct.ProductId = Convert.ToInt32(daraReader[0].ToString());
               aProduct.ProductName = daraReader[1].ToString();
               aProduct.ItemDescription = daraReader[2].ToString();
               aProduct.ItemCode = daraReader[3].ToString();
           }
           daraReader.Close();
           connection.Close();
           return aProduct;


       }
    }
}
