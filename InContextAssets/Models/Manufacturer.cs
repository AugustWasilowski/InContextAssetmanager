using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InContextAssets.Models
{
    public class Manufacturer
    {   
        public int ManufacturerID { get; set; }

        [Required(ErrorMessage="Enter a name.")]
        public string Name { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
    }
}