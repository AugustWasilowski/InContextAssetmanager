using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using InContextAssets.Models;
using InContextAssets.DAL;
using InContextAssets.ViewModels;
using PagedList;

namespace InContextAssets.Controllers
{ 
    public class AssetController : Controller
    {
        private InContextAssetsContext db = new InContextAssetsContext();

        //
        // GET: /Asset/

        public ViewResult Index(string sortOrder,string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.ManufacturerSortParm = sortOrder == "Manufacturer" ? "Manufacturer desc" : "Manufacturer";
            ViewBag.PackageTypeSortParm = sortOrder == "PackageType" ? "PackageType desc" : "PackageType";

            if (Request.HttpMethod == "GET")
	        {
                searchString = currentFilter;
	        }
            else
            {
                page = 1;
            }
            ViewBag.CurrentFilter = searchString;

            var assets = from a in db.Assets                        
                         select a;
           
            if (!String.IsNullOrEmpty(searchString))
            {
                assets = assets.Where(a => a.Name.ToUpper().Contains(searchString.ToUpper())
                                    || a.Manufacturer.Name.ToUpper().Contains(searchString.ToUpper())
                                    || a.Tags.Any(t => t.Key.ToUpper().Contains(searchString.ToUpper()))
                                    || a.Tags.Any(t => t.Value.ToUpper().Contains(searchString.ToUpper())));                                    
            }
            
            switch (sortOrder)
            {
                case "Name desc":
                    assets = assets.OrderByDescending(a => a.Name);
                    break;
                case "Tag desc":
                    assets = assets.OrderByDescending(a => a.Tags.ElementAt(0));
                    break;
                case "Manufacturer":
                    assets = assets.OrderBy(a => a.Manufacturer.Name);
                    break;
                case "Manufacturer desc":
                    assets = assets.OrderByDescending(a => a.Manufacturer.Name);
                    break;
                case "PackageType":
                    assets = assets.OrderBy(a => a.PackageType.Type);
                    break;
                case "PackageType desc":
                    assets = assets.OrderByDescending(a => a.PackageType.Type);
                    break;
                default:
                    assets = assets.OrderBy(a => a.Name);
                    break;
            }

            int pageSize = 5;
            int pageNumber = (page ?? 1);
            return View(assets.ToPagedList(pageNumber, pageSize));
        }

        //
        // GET: /Asset/Details/5

        public ViewResult Details(int id)
        {
            Asset asset = db.Assets.Find(id);
            return View(asset);
        }

        //
        // GET: /Asset/Create

        public ActionResult Create(bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMesasge = "Unable to save changes.";
            }
            PopulateManufacturersDropDownList();
            PopulatePackageTypeDropDownList();
            PopulateAssignedTagData();
            return View();
        } 

        //
        // POST: /Asset/Create

        [HttpPost]
        public ActionResult Create(Asset asset)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Assets.Add(asset);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {

                ModelState.AddModelError("", "Unable to save changes.");
            }
            PopulateManufacturersDropDownList(asset.ManufacturerID);
            PopulatePackageTypeDropDownList(asset.PackageTypeID);
            if (asset.Tags != null)
            {
                PopulateAssignedTagData(asset);
            }
            else
            {
                PopulateAssignedTagData();
            }
            return View(asset);
        }
        
        //
        // GET: /Asset/Edit/5
 
        public ActionResult Edit(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMesasge = "Unable to save changes.";
            }
            Asset asset = db.Assets
                .Include(a => a.Tags)
                .Where(a => a.AssetID == id)
                .Single();
            PopulateManufacturersDropDownList(asset.ManufacturerID);
            PopulatePackageTypeDropDownList(asset.PackageTypeID);
            PopulateAssignedTagData(asset);
            return View(asset);
        }

        //
        // POST: /Asset/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection formCollection, string[]selectedTags)
        {
            var assetToUpdate = db.Assets
                .Include(a => a.Tags)
                .Where(a => a.AssetID == id)
                .Single();
            if (TryUpdateModel(assetToUpdate, "", null, new string[] { "Tags"}))
            {
                try
                {
                    UpdateAssetTags(selectedTags, assetToUpdate);

                    db.Entry(assetToUpdate).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes.");
                }
            }
            PopulateManufacturersDropDownList(assetToUpdate.ManufacturerID);
            PopulatePackageTypeDropDownList(assetToUpdate.PackageTypeID);
            PopulateAssignedTagData(assetToUpdate);
            return View(assetToUpdate);
        }

        //
        // GET: /Asset/Delete/5
 
        public ActionResult Delete(int id, bool? saveChangesError)
        {
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Unable to save changes.";
            }            
            return View(db.Assets.Find(id));
        }

        //
        // POST: /Asset/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Asset asset = db.Assets.Find(id);
                db.Assets.Remove(asset);
                db.SaveChanges();
            }
            catch (DataException)
            {
                return RedirectToAction("Delete", new System.Web.Routing.RouteValueDictionary {
                    { "id", id},
                    { "saveChangesError", true} });
            }
            return RedirectToAction("Index");
        }
        private void UpdateAssetTags(string[] selectedTags, Asset assetToUpdate)
        {
            if (selectedTags == null)
            {
                assetToUpdate.Tags = new List<Tag>();
                return;
            }

            var selectedTagsHS = new HashSet<string>(selectedTags);
            var assetTags = new HashSet<int>
                (assetToUpdate.Tags.Select(t => t.TagID));
            foreach (var tag in db.Tags)
            {
                if (selectedTagsHS.Contains(tag.TagID.ToString()))
                {
                    if (!assetTags.Contains(tag.TagID))
	                {
                        assetToUpdate.Tags.Add(tag);
	                }
                }
                else
                {
                    if (assetTags.Contains(tag.TagID))
                    {
                        assetToUpdate.Tags.Remove(tag);
                    }
                }
            }
        }
        private void PopulateManufacturersDropDownList(object selectedManufacturer = null)
        {
            var manufacturersQuery = from m in db.Manufacturers
                                     orderby m.Name
                                     select m;
            ViewBag.ManufacturerID = new SelectList(manufacturersQuery, "ManufacturerID", "Name", selectedManufacturer);
        }

        private void PopulatePackageTypeDropDownList(object selectedPackageType = null)
        {
            var packagetypesQuery = from p in db.PackageTypes
                                    orderby p.Type
                                    select p;
            ViewBag.PackageTypeID = new SelectList(packagetypesQuery, "PackageTypeID", "Type", selectedPackageType);
        }

        private void PopulateAssignedTagData()
        {
            var allTags = db.Tags;
            var viewModel = new List<AssetTagData>();
            foreach (var tag in allTags)
            {
                viewModel.Add(new AssetTagData
                {
                    TagID = tag.TagID,
                    Key = tag.Key,
                    Value = tag.Value
                });
            }
            ViewBag.Tags = viewModel;
        }
        private void PopulateAssignedTagData(Asset asset)
        {
            var allTags = db.Tags;   
            var assetTags = new HashSet<int>(asset.Tags.Select(t => t.TagID));  
            var viewModel = new List<AssetTagData>();

            foreach (var tag in allTags)
            {
                viewModel.Add(new AssetTagData
                {
                    TagID = tag.TagID,
                    Key = tag.Key,
                    Value = tag.Value,
                    Assigned = assetTags.Contains(tag.TagID)
                });                    
            }
            ViewBag.Tags = viewModel;
        }       

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}