using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OzTravel.Models;

namespace OzTravel.ViewModels
{
    public class PackageCreateSelectViewModel
    {
        public IEnumerable<Location> Locations { get; set; }
    }
}
