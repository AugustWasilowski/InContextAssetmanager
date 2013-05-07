using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InContextAssets.Models
{
    public class Asset
    {        
        public int AssetID { get; set; }

        public decimal? Height { get; set; }
        public decimal? Width { get; set; }
        public decimal? Depth { get; set; }
        public decimal? Radius { get; set; }

        [MaxLength(100)]
        [Required(ErrorMessage = "Enter a name.")]
        public string Name { get; set; }
                
        [Required(ErrorMessage = "Select a Packag Type.")]
        public int PackageTypeID { get; set; }

        [Required(ErrorMessage = "Select a manufacturer.")]
        public int ManufacturerID { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        public virtual PackageType PackageType { get; set; }        
        public virtual ICollection<Tag> Tags { get; set; }

    }
}