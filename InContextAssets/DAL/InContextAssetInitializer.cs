using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using InContextAssets.Models;

namespace InContextAssets.DAL
{
    public class InContextAssetInitializer : DropCreateDatabaseIfModelChanges<InContextAssetsContext>
    {
        protected override void Seed(InContextAssetsContext context)
        {
            var manufactueres = new List<Manufacturer>
            {
                new Manufacturer { Name = "General Mills", Assets = new List<Asset>()},
                new Manufacturer { Name = "Proctor and Gamble", Assets = new List<Asset>()},
                new Manufacturer { Name = "Rick Astley Group", Assets = new List<Asset>()},
                new Manufacturer { Name = "AugTec Industries", Assets = new List<Asset>()},
                new Manufacturer { Name = "Mustache Inc.", Assets = new List<Asset>()}
            };

            manufactueres.ForEach(x => context.Manufacturers.Add(x));
            context.SaveChanges();

            var packagetypes = new List<PackageType>
            {
                new PackageType { Type = "Box", Assets = new List<Asset>()},
                new PackageType { Type = "Can", Assets = new List<Asset>()},
                new PackageType { Type = "Soft Pack", Assets = new List<Asset>()}
            };

            packagetypes.ForEach(x => context.PackageTypes.Add(x));
            context.SaveChanges();

            var assets = new List<Asset>
            {
                new Asset { Depth = 3, Height= 10, Width = 7, Name="Michael Jordan Flavored Flakes", Tags = new List<Tag>(), ManufacturerID = 1, PackageTypeID = 1},
                new Asset { Depth = 3, Height= 10, Width = 7, Name="Michael Phelps Flavored Flakes", Tags = new List<Tag>(), ManufacturerID = 1, PackageTypeID = 1},
                new Asset { Depth = 3, Height= 10, Width = 7, Name="Box 'O Cereal", Tags = new List<Tag>(), ManufacturerID = 5, PackageTypeID = 1},
                new Asset { Depth = 5, Height= 12, Width = 9, Name="Iron Fist of Justice", Tags = new List<Tag>(), ManufacturerID = 1, PackageTypeID = 1},
                new Asset { Height= 5, Radius = 4, Name="Ground Beef", Tags = new List<Tag>(),ManufacturerID = 1, PackageTypeID = 3},
                new Asset { Height= 5, Radius = 4, Name="Pork Loin", Tags = new List<Tag>(), ManufacturerID = 1, PackageTypeID = 3},
                new Asset { Height= 5, Radius = 4, Name="Tomatos", Tags = new List<Tag>(), ManufacturerID = 1, PackageTypeID = 3},
                new Asset { Height= 5, Radius = 4, Name="Jalapenos", Tags = new List<Tag>(), ManufacturerID = 1, PackageTypeID = 3},
                new Asset { Height= 5, Radius = 4, Name="Canned Corn", Tags = new List<Tag>(), ManufacturerID = 3, PackageTypeID = 2},
                new Asset { Height= 7, Radius = 5, Name="Cilantro", Tags = new List<Tag>(), ManufacturerID = 2, PackageTypeID = 2},
                new Asset { Depth = 1, Height= 5, Width = 5, Name="Novelty Pen Set", Tags = new List<Tag>(), ManufacturerID = 1, PackageTypeID = 1},
                new Asset { Depth = 1, Height= 5, Width = 1, Name="Laser Pointer", Tags = new List<Tag>(), ManufacturerID = 4, PackageTypeID = 1},
                new Asset { Depth = 1, Height= 1, Width = 1, Name="Key Ring", Tags = new List<Tag>(), ManufacturerID = 4, PackageTypeID = 1},
                new Asset { Depth = 5, Height= 36, Width = 5, Name="Mop and Bucket Hobby Kit", Tags = new List<Tag>(), ManufacturerID = 5, PackageTypeID = 1},
                new Asset { Depth = 50, Height= 50, Width = 100, Name="Breaking Bad Home Game", Tags = new List<Tag>(), ManufacturerID = 5, PackageTypeID = 1}
            };

            assets.ForEach(x => context.Assets.Add(x));
            context.SaveChanges();

            var tags = new List<Tag>
            {
                new Tag{ Key = "Brand", Value = "Wheaties"},
                new Tag{ Key = "Brand", Value = "Augie O's" },
                new Tag{ Key = "Brand", Value = "Justice Flakes" },
                new Tag{ Key = "Brand", Value = "Brawn D'Ohs" },
                new Tag{ Key = "Brand", Value = "Brookview Beef" },
                new Tag{ Key = "Brand", Value = "Stone Cross Farms" },
                new Tag{ Key = "Brand", Value = "Irving Produce" },
                new Tag{ Key = "Brand", Value = "Widgets 'R Us" },                
                new Tag{ Key = "Category", Value = "Cereal" },
                new Tag{ Key = "Category", Value = "Meat" },
                new Tag{ Key = "Category", Value = "Produce" },
                new Tag{ Key = "Category", Value = "Chotchkies" },
                new Tag{ Key = "Category", Value = "Household Implements" },
                new Tag{ Key = "Target Market", Value = "Children" },
                new Tag{ Key = "Target Market", Value = "Elderly" },
                new Tag{ Key = "Target Market", Value = "Hipsters" },
                new Tag{ Key = "Target Market", Value = "The Infirm" },
                new Tag{ Key = "Target Market", Value = "Males 18 - 35" },
                new Tag{ Key = "May Cause", Value = "Excessive euphoria" },
                new Tag{ Key = "May Cause", Value = "Rickets" },
                new Tag{ Key = "May Cause", Value = "A swelling national pride" },
                new Tag{ Key = "May Cause", Value = "Early voting" },
                new Tag{ Key = "May Cause", Value = "Tree hugging" }
            };

            tags.ForEach(x => context.Tags.Add(x));
            context.SaveChanges();

            assets[0].Tags.Add(tags[0]);
            assets[0].Tags.Add(tags[8]);
            assets[0].Tags.Add(tags[17]);
            assets[0].Tags.Add(tags[19]);
            assets[1].Tags.Add(tags[0]);
            assets[1].Tags.Add(tags[8]);
            assets[1].Tags.Add(tags[17]);
            assets[1].Tags.Add(tags[19]);
            assets[2].Tags.Add(tags[1]);
            assets[2].Tags.Add(tags[8]);
            assets[2].Tags.Add(tags[15]);
            assets[2].Tags.Add(tags[21]);
            assets[3].Tags.Add(tags[7]);
            assets[3].Tags.Add(tags[11]);
            assets[3].Tags.Add(tags[15]);
            assets[4].Tags.Add(tags[19]);
            assets[4].Tags.Add(tags[4]);
            assets[4].Tags.Add(tags[9]);
            assets[4].Tags.Add(tags[17]);
            assets[4].Tags.Add(tags[18]);
            assets[5].Tags.Add(tags[6]);
            assets[5].Tags.Add(tags[10]);
            assets[5].Tags.Add(tags[14]);
            assets[6].Tags.Add(tags[21]);
            assets[6].Tags.Add(tags[6]);
            assets[6].Tags.Add(tags[10]);
            assets[6].Tags.Add(tags[14]);
            assets[6].Tags.Add(tags[21]);
            assets[7].Tags.Add(tags[6]);
            assets[7].Tags.Add(tags[10]);
            assets[7].Tags.Add(tags[14]);
            assets[7].Tags.Add(tags[21]);
            assets[8].Tags.Add(tags[6]);
            assets[8].Tags.Add(tags[10]);
            assets[8].Tags.Add(tags[13]);
            assets[8].Tags.Add(tags[22]);
            assets[9].Tags.Add(tags[7]);
            assets[9].Tags.Add(tags[11]);
            assets[9].Tags.Add(tags[7]);
            assets[9].Tags.Add(tags[21]);
            assets[10].Tags.Add(tags[7]);
            assets[10].Tags.Add(tags[11]);
            assets[10].Tags.Add(tags[13]);
            assets[10].Tags.Add(tags[22]);
            assets[11].Tags.Add(tags[7]);
            assets[11].Tags.Add(tags[11]);
            assets[11].Tags.Add(tags[15]);
            assets[11].Tags.Add(tags[18]);
            assets[12].Tags.Add(tags[7]);
            assets[12].Tags.Add(tags[11]);
            assets[12].Tags.Add(tags[13]);
            assets[12].Tags.Add(tags[18]);
            assets[13].Tags.Add(tags[7]);
            assets[13].Tags.Add(tags[12]);
            assets[13].Tags.Add(tags[14]);
            assets[13].Tags.Add(tags[19]);
            assets[14].Tags.Add(tags[7]);
            assets[14].Tags.Add(tags[12]);
            assets[14].Tags.Add(tags[15]);
            assets[14].Tags.Add(tags[22]);

            //manufactueres[0].Assets.Add(assets[0]);
            //manufactueres[0].Assets.Add(assets[1]);
            //manufactueres[1].Assets.Add(assets[2]);
            //manufactueres[1].Assets.Add(assets[3]);
            //manufactueres[1].Assets.Add(assets[4]);
            //manufactueres[1].Assets.Add(assets[5]);
            //manufactueres[2].Assets.Add(assets[6]);
            //manufactueres[2].Assets.Add(assets[7]);
            //manufactueres[2].Assets.Add(assets[8]);
            //manufactueres[3].Assets.Add(assets[9]);
            //manufactueres[3].Assets.Add(assets[10]);
            //manufactueres[3].Assets.Add(assets[11]);
            //manufactueres[4].Assets.Add(assets[12]);
            //manufactueres[4].Assets.Add(assets[13]);
            //manufactueres[4].Assets.Add(assets[14]);

            //packagetypes[0].Assets.Add(assets[0]);
            //packagetypes[0].Assets.Add(assets[1]);
            //packagetypes[0].Assets.Add(assets[2]);
            //packagetypes[0].Assets.Add(assets[3]);
            //packagetypes[1].Assets.Add(assets[4]);
            //packagetypes[1].Assets.Add(assets[5]);
            //packagetypes[2].Assets.Add(assets[6]);
            //packagetypes[2].Assets.Add(assets[7]);
            //packagetypes[1].Assets.Add(assets[8]);
            //packagetypes[2].Assets.Add(assets[9]);
            //packagetypes[0].Assets.Add(assets[10]);
            //packagetypes[0].Assets.Add(assets[11]);
            //packagetypes[0].Assets.Add(assets[12]);
            //packagetypes[0].Assets.Add(assets[13]);
            //packagetypes[0].Assets.Add(assets[14]);
        }
    }
}