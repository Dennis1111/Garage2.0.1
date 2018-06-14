using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Garage2._0._1.Models
{

    public class ParkedVehicle
    {
        //public VehicleType Type { get; set; }
        //We dont have a member when we create an instance in controller
        [Display(Name = "Customer")]
        public string Owner => Member ==null ? "" : Member.FirstName + " " + Member.LastName; 
        [Key]
        [Required]
        [StringLength(6, MinimumLength = 6)]
        [RegularExpression(@"^[A-Z]+[0-9]*$", ErrorMessage = "Only UpperCase or Numbers")]
        public String RegistrationNumber { get; set; }
        [StringLength(20, MinimumLength = 3)]
        public String Color { get; set; }
        [StringLength(20, MinimumLength = 3)]
        public String Brand { get; set; }
        [Range(0, 8)]
        public int Wheels { get; set; }
        [Display(Name = "Parking Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm }", ApplyFormatInEditMode = true)]
        public DateTime ParkingTime { get; set; }

        //Navigational Data
        [ForeignKey("VehicleType")]
        public int VehicleTypeId { get; set; }
        [Display(Name = "Vehicle Type")]
        public virtual VehicleType VehicleType { get; set; }
        //Naviagational property
        [ForeignKey("Member")]
        public int MembersId { get; set; }
        public virtual Member Member { get; set; }
    }
}