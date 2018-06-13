using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Garage2._0._1.Models
{
    public class Member
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required][DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required][DataType(DataType.Text)]
        public string  LastName { get; set; }
    }
}