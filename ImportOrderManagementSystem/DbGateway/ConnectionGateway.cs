using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportOrderManagementSystem.DbGateway
{
   public  class ConnectionGateway
    {
       
       protected  SqlConnection connection;
       //string connectionString = @"server=KYOTO-PC06\SQLSERVER2018; Integrated Security = SSPI; database =NewProductList33;User=sa;Password=SystemAdministrator;Connect Timeout=30";
        string connectionString = @"server=tcp:KyotoServer,49172; Integrated Security = SSPI; database =NewProductList1;Connect Timeout=30";
        //string connectionString = @"Data Source=tcp:KyotoServer,49172;Initial Catalog=NewProductList;User=sa;Password=SystemAdministrator;Persist Security Info=True;";
        //string connectionString = @"server=KYOTO-PC06\SQLEXPRESS; Integrated Security = SSPI; database =NewProductList;Connect Timeout=30";

    }
}
