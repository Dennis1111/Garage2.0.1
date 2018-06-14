namespace Garage2._0._1.Migrations
{
    using Garage2._0._1.Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DataAccessLayer.RegisterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        int GetMemberId(DataAccessLayer.RegisterContext context, string name)
        {
            var Id = context.Member.Where(m => m.FirstName == name).First().Id;
            return Id;
        }

        int GetVehicleTypeId(DataAccessLayer.RegisterContext context, string name)
        {
            var Id = context.VehicleTypes.Where(m => m.Type == name).First().Id;
            return Id;
        }
        private Random gen = new Random();

        DateTime GetParkingTime()
        {
            DateTime time = DateTime.Now;
            TimeSpan timeSpan = new TimeSpan(gen.Next(24), gen.Next(59), 0);
            return time.Subtract(timeSpan);
        }

        protected override void Seed(DataAccessLayer.RegisterContext context)
        {
            //  This method will be called after migrating to the latest version.

            var vehicleTypes = new[] {
                new VehicleType { Type = "Car"},
                new VehicleType { Type = "MotorCycle"},
                new VehicleType { Type = "Bus"}
                //new VehicleType { Type = "Boat"},
                //new VehicleType { Type = "AirPlane"}
            };
            context.VehicleTypes.AddOrUpdate(s => new { s.Type }, vehicleTypes);

            var registeredPersons = new[] { new Member{ FirstName="Dennis",LastName="Nilsson"},
                new Member{ FirstName="Erik",LastName="Larsson"},
                new Member{ FirstName="Adam",LastName="Olsson"},
                new Member{ FirstName="Bereket",LastName="Alemeseged"},
                new Member{ FirstName="Mohamed",LastName="Almohsen"},
                new Member{ FirstName="Robert",LastName="Hansson"},
                new Member{ FirstName="Berit",LastName="Persson"},
                new Member{ FirstName="Sten",LastName="Hansen"}

            };

            context.Member.AddOrUpdate(s => new { s.FirstName, s.LastName }, registeredPersons);
            context.SaveChanges();

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.ParkedVehicle.AddOrUpdate(p => p.RegistrationNumber,
                new ParkedVehicle()
                {
                    RegistrationNumber = "ABC123",
                    Brand = "Honda",
                    Color = "Yellow",
                    Wheels = 4,
                    VehicleTypeId = vehicleTypes[0].Id,
                    MembersId = registeredPersons[0].Id,
                    ParkingTime = GetParkingTime()
                },
                 new ParkedVehicle()
                 {
                     RegistrationNumber = "AAA000",
                     Brand = "BMW",
                     Color = "White",
                     Wheels = 4,
                     VehicleTypeId = vehicleTypes[0].Id,
                     MembersId = registeredPersons[1].Id,
                     ParkingTime = GetParkingTime()
                 },
                new ParkedVehicle()
                {
                    RegistrationNumber = "XXX345",
                    Brand = "Kawazaki",
                    Color = "Blue",
                    Wheels = 2,
                    VehicleTypeId = vehicleTypes[1].Id,
                    MembersId = registeredPersons[2].Id,
                    ParkingTime = GetParkingTime()
                },
                   new ParkedVehicle()
                   {
                       RegistrationNumber = "CCC246",
                       Brand = "Saab",
                       Color = "Red",
                       Wheels = 4,
                       VehicleTypeId = vehicleTypes[0].Id,
                       MembersId = registeredPersons[3].Id,
                       ParkingTime = GetParkingTime()
                   },
                      new ParkedVehicle()
                      {
                          RegistrationNumber = "ZXC345",
                          Brand = "Volvo",
                          Color = "Blue",
                          Wheels = 4,
                          VehicleTypeId = vehicleTypes[0].Id,
                          MembersId = registeredPersons[4].Id,
                          ParkingTime = GetParkingTime()
                      },
                new ParkedVehicle()
                {
                    RegistrationNumber = "DDD123",
                    Brand = "Tesla",
                    Color = "White",
                    Wheels = 4,
                    VehicleTypeId = vehicleTypes[0].Id,
                    MembersId = registeredPersons[5].Id,
                    ParkingTime = GetParkingTime()
                },

                new ParkedVehicle()
                {
                    RegistrationNumber = "GHY678",
                    Brand = "Scania",
                    Color = "Yellow",
                    Wheels = 4,
                    VehicleTypeId = vehicleTypes[2].Id,
                    MembersId = registeredPersons[6].Id,
                    ParkingTime = GetParkingTime()
                },
                new ParkedVehicle()
                {
                    RegistrationNumber = "ZZZ111",
                    Brand = "Volvo",
                    Color = "Purple",
                    Wheels = 4,
                    VehicleTypeId = vehicleTypes[0].Id,
                    MembersId = registeredPersons[7].Id,
                    ParkingTime = GetParkingTime()
                }
                );
        }
    }
}
