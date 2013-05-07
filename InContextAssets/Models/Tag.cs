using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InContextAssets.Models
{
    public class Tag
    {
        public int TagID { get; set; }

        [Required(ErrorMessage="Enter a Key.")]
        public string Key { get; set; }

        [Required(ErrorMessage="Enter a Value.")]
        public string Value { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }

    }
}