namespace Garage2._0._1.Migrations
{
    using Garage2._0._1.Models;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Garage2._0._1.DataAccessLayer.RegisterContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        /*private List<VehicleType> GetVehleTypes(Garage2._0._1.DataAccessLayer.RegisterContext context) {
         
            return context.VehicleTypes.ToList();
        }*/
        int GetMemberId(Garage2._0._1.DataAccessLayer.RegisterContext context, string name)
        {
            var Id = context.Member.Where(m => m.FirstName == name).First().Id;
            return Id;
        }

        int GetVehicleTypeId(Garage2._0._1.DataAccessLayer.RegisterContext context, string name)
        {
            var Id = context.VehicleTypes.Where(m => m.Type == name).First().Id;
            return Id;
        }

        protected override void Seed(Garage2._0._1.DataAccessLayer.RegisterContext context)
        {
            //  This method will be called after migrating to the latest version.

            var vehicleTypes = new[] {
                new VehicleType { Type = "Car"},
                new VehicleType { Type = "MotorCycle"},
                new VehicleType { Type = "Boat"},
                new VehicleType { Type = "Bus"},
                new VehicleType { Type = "AirPlane"}
            };
            context.VehicleTypes.AddOrUpdate(s => new { s.Type }, vehicleTypes);

            var registeredPersons = new[] { new Member{ FirstName="Dennis",LastName="Nilsson"},
                new Member{ FirstName="Erik",LastName="Larsson"},
                new Member{ FirstName="Adam",LastName="Olsson"},
                new Member{ FirstName="Bereket",LastName="Alemeseged"},
                new Member{ FirstName="Mohamed",LastName="Almohsen"},
                new Member{ FirstName="Nisse",LastName="Hult"}
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
                    ParkingTime = DateTime.Now
                },
                new ParkedVehicle()
                {
                    RegistrationNumber = "XXX345",
                    Brand = "Saab",
                    Color = "Blue",
                    Wheels = 4,
                    VehicleTypeId = vehicleTypes[1].Id,
                    MembersId = registeredPersons[2].Id,
                    ParkingTime = DateTime.Now
                },
                   new ParkedVehicle()
                   {
                       RegistrationNumber = "CCC345",
                       Brand = "Saab",
                       Color = "Red",
                       Wheels = 4,
                       VehicleTypeId = vehicleTypes[2].Id,
                       MembersId = registeredPersons[3].Id,
                       ParkingTime = DateTime.Now
                   },
                      new ParkedVehicle()
                      {
                          RegistrationNumber = "ZXC345",
                          Brand = "Saab",
                          Color = "Blue",
                          Wheels = 4,
                          VehicleTypeId = vehicleTypes[1].Id,
                          MembersId = registeredPersons[4].Id,
                          ParkingTime = DateTime.Now
                      },
                new ParkedVehicle()
                {
                    RegistrationNumber = "DDD123",
                    Brand = "Kawazaki",
                    Color = "Red",
                    Wheels = 4,
                    VehicleTypeId = vehicleTypes[4].Id,
                    MembersId = registeredPersons[4].Id,

                    //Type = VehicleType.MotorCycle,
                    ParkingTime = DateTime.Now
                },

                new ParkedVehicle()
                {
                    RegistrationNumber = "GHY678",
                    Brand = "Scania",
                    Color = "Yellow",
                    Wheels = 4,
                    VehicleTypeId = vehicleTypes[3].Id,
                    MembersId = registeredPersons[4].Id,

                    //Type = VehicleType.Bus,
                    ParkingTime = DateTime.Now
                },
                new ParkedVehicle()
                {
                    RegistrationNumber = "ZZZ111",
                    Brand = "Boeing",
                    Color = "Purple",
                    Wheels = 2,
                    VehicleTypeId = vehicleTypes[4].Id,
                    MembersId = registeredPersons[3].Id,

                    //Type = VehicleType.Airplane,
                    ParkingTime = DateTime.Now
                }
                );
        }
    }
}
