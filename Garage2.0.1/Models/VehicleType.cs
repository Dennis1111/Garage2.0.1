using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0._1.Models
{
    public class VehicleType
    {
        [Key]
        public int Id { get; set; }
        public String Type { get; set; }
    }
}