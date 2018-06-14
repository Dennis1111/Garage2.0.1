using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Garage2._0._1.Models
{
    public class ReceiptViewModel
    {
        [Display(Name = "Total price")]
        public string TotalPrice => (int)Price + "Kr";

        [Display(Name = "Price per hour")]
        public string PricePerHour => MinutePrice*60 + "Kr";
        [Display(Name = "Total parked time")]
        public string TotalTime => "Hours: " + Hours + " Minutes: "+ Minutes;
        [StringLength(6, MinimumLength = 6)]
        public String RegistrationNumber { get; set; }
        [Display(Name = "Parking Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ParkingTime { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Checkout Date")]
        public DateTime CheckOutTime { get; set; }
        public int Minutes { get; set;}
        public int Hours { get; set; }
        [Display(Name = "Customer")]
        public string Owner => FirstName + " " + LastName;
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public double Price { get; set; }
        public double MinutePrice { get; set; }
    }
}