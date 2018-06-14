using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Garage2._0._1.Models
{
    public class ParkedVehicleAllViewModel
    {
        public int Rows { get; set; }
        public int Page { get; set; }
        public string SearchName { get; set; }
        public string SortOrder { get; set; }
        public string Column { get; set; }
        public string SelectedColumn { get; set; }
        public List<SelectListItem> ColumnSelectList { get; set; }
        public string SelectedSorting { get; set; }

        public List<SelectListItem> SortingSelectList
        {
            get
            {
                var list = new List<SelectListItem>();
                list.Add(new SelectListItem() { Text = "Ascending", Value = "Ascending" });
                list.Add(new SelectListItem() { Text = "Descending", Value = "Descending" });
                return list;
            }
        }

        public IEnumerable<ParkedVehicle> ParkedVehicles { get; set; }

    }
}