using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garage2._0._1.Models
{
    public class ParkedVehiclesViewModel
    {
        public int Rows { get; set; }
        public int Page { get; set; }
        public string SearchName { get; set; }
        public string SortOrder { get; set; }
        public string Column { get; set; }
        public string SelectedVehicleType { get; set; }
        public List<SelectListItem> VehicleTypeSelectList { get; set; }
        public IEnumerable<ParkedVehicle> ParkedVehicles { get; set;}
    }
}