using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InContextAssets.Models
{
    public class PackageType
    {
        public int PackageTypeID { get; set; }

        [Required(ErrorMessage = "Enter a type.")]
        public string Type { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
    }
}