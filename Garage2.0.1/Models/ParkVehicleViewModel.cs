using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garage2._0._1.Models
{
    public class ParkVehicleViewModel
    {
        //public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RegistrationNumber{ get; set; }
        public string Color { get; set;}
        public string Brand { get; set;}
        public int Wheels { get; set; }
        public string SelectedVehicleType { get; set; }
        public int SelectedVehicleTypeId { get; set; }
        //public Member Member { get; set; }
        
        public VehicleType VehicleType { get; set; }
        //public ParkedVehicle ParkedVehicle { get; set; }
        public IEnumerable<VehicleType> VehicleTypeSelectList { get; set; }

        public bool MemberFound { get; set; }
        public bool Post { get; set; }
        public bool RegNrTaken { get; set; }
    }
}