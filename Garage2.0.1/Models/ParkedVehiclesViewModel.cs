using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2._0._1.Models
{
    public class ParkedVehiclesViewModel
    {
        public string SearchName { get; set; }
        public string SortOrder { get; set; }
        public string Column { get; set; }
        //public string Owner { get; set; }
        //public string VehicleType { get; set; }
        //public string RegNum { get; set; }
        //public string ParkingTime { get; set; }
        public IEnumerable<ParkedVehicle> ParkedVehicles { get; set;}
    }
}