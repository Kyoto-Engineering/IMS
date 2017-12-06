using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImportOrderManagementSystem.DbGateway
{
  public  class ConnectionString
    {
     // public string DBConn = @"Data Source=KYOTO-PC06\SQLSERVER2018;User=sa;Password=SystemAdministrator;Integrated Security=True;database=NewProductList33;Connect Timeout=30";
      public string DBConn = @"Data Source=tcp:KyotoServer,49172;Initial Catalog=ProductNRelatedDB_newforSpecialPrice;User=sa;Password=SystemAdministrator;Persist Security Info=True;";
        //string connectionString = @"Data Source=tcp:KyotoServer,49172;Initial Catalog=EmployeeMSDb;User=sa;Password=SystemAdministrator;Persist Security Info=True;";
        //public string DBConn = @"Data Source=KYOTO-PC06\SQLEXPRESS;Integrated Security=True;database=NewProductList;Connect Timeout=30";
    }
}
