using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzTravel.Models;
using OzTravel.Services;
using OzTravel.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace OzTravel.Controllers
{
    public class LocationController : Controller
    {
        private IDataService<Location> _locationDataService;
        private IDataService<Package> _packageDataService;

        public LocationController(IDataService<Package> packageService,
                                 IDataService<Location> locationService)
        {
            _packageDataService = packageService;
            _locationDataService = locationService;
        }

        [HttpGet]
        public IActionResult Details(int id, string name)
        {
            TempData["locationId"] = id.ToString();

            //Package package = _packageDataService.GetSingle(p => p.PackageId == id);

            //TempData["locationName"] = name;

            Location location = _locationDataService.GetSingle(p => p.LocationId == id);
            IEnumerable<Package> packageList = _packageDataService.Query(p => p.LocationId == id && p.IsAvailable == true);

            LocationDetailsViewModel vm = new LocationDetailsViewModel
            {
                Name = location.Name,
                Description = location.Description,
                Picture = location.Picture,
                Packages = packageList
            };

            return View(vm);
        }
    }
}