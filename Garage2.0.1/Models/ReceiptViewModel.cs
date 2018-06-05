using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0._1.Models
{
    public class ReceiptViewModel
    {
        [StringLength(6, MinimumLength = 6)]
        public String RegistrationNumber { get; set; }
        [Display(Name = "Parking Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ParkingTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CheckOutTime { get; set; }
        public int Minutes { get; set;}
        public double Price { get; set; }
    }
}