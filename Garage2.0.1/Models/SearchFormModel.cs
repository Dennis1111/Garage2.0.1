using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Garage2._0._1.Models
{
    public enum Sorting{Descending,Ascending,None}

    public class SearchFormModel
    {
        [DisplayName("Search Name")]
        public String SearchName { get; set; }
        public String Column { get; set; }
        public Sorting SortingMethod { get; set; }
    }
}