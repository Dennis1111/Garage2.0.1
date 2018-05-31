using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Garage2._0._1.Models
{
    public enum VehicleType
    {
        Car,
        MotorCycle,
        Boat,
        Airplane,
        Bus
    }

    public class ParkedVehicle
    {
        public VehicleType Type { get; set; }
        [Key]
        [Required]
        [StringLength(6, MinimumLength = 6)]
        public String RegistrationNumber { get; set; }
        [StringLength(20, MinimumLength = 3)]
        public String Color { get; set; }
        [StringLength(20, MinimumLength = 3)]
        public String Brand { get; set; }
        [Range(2,8)]
        public int Wheels { get; set; }
        [Display(Name = "Parking Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ParkingTime { get; set; }
    }
}