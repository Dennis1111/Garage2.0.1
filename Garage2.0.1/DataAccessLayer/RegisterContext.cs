using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Garage2._0._1.DataAccessLayer
{
    public class RegisterContext : DbContext
    {
        public DbSet<Models.ParkedVehicle> ParkedVehicle { get; set; }

        public RegisterContext() : base("ParkedVehicleConn")
        {

        }
    
    }
}