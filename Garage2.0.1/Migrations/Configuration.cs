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
                    Color = "Yellow",
                    Wheels = 4,
                    Type = VehicleType.Car
                },
                new ParkedVehicle()
                {
                    RegistrationNumber = "XXX345",
                    Brand = "Saab",
                    Color = "Blue",
                    Wheels = 4,
                    Type = VehicleType.Car
                },
                new ParkedVehicle()
                {
                    RegistrationNumber = "DD123",
                    Brand = "Volvo",
                    Color = "Red",
                    Wheels = 4,
                    Type = VehicleType.MotorCycle
                },
                new ParkedVehicle()
                {
                    RegistrationNumber = "HELLO",
                    Brand = "Scania",
                    Color = "Yellow",
                    Wheels = 4,
                    Type = VehicleType.Bus
                }, new ParkedVehicle()
                {
                    RegistrationNumber = "ZZZZ44",
                    Brand = "Volvo",
                    Color = "Purple",
                    Wheels = 2,
                    Type = VehicleType.Airplane
                }
                );
        }
    }
}
