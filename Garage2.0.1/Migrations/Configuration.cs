namespace Garage2._0._1.Migrations
{
    using Garage2._0._1.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage2._0._1.DataAccessLayer.RegisterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Garage2._0._1.DataAccessLayer.RegisterContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.ParkedVehicle.AddOrUpdate(p => p.RegistrationNumber,
                new ParkedVehicle()
                {
                    RegistrationNumber = "ABC123",
                    Brand = "Volvo",
                    Color = "Greeen",
                    Wheels = 4,
                    Type = VehicleType.Car
                });
        }
    }
}
