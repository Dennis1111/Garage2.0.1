using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garage2._0._1.Models
{
    public class ParkVehicleViewModel
    {
        public string SelectedVehicleType { get; set; }
        public ParkedVehicle ParkedVehicle { get; set; }
        public List<SelectListItem> VehicleTypeSelectList { get; set; }
    }
}