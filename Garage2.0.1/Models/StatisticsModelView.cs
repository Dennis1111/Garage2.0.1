using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Garage2._0._1.Models
{
    public class StatisticsModelView
    {
        public IQueryable<Tuple<String,int>> Statistics { get; set; }
    }
}