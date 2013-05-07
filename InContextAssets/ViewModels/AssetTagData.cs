using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InContextAssets.ViewModels
{
    public class AssetTagData
    {
        public int TagID { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public bool Assigned { get; set; }
    }
}