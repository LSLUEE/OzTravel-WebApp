using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OzTravel.Models;

namespace OzTravel.ViewModels
{
    public class OrderDetailsPastViewModel
    {
        public IEnumerable<Order> Orders { get; set; }
        public int Count { get; set; }
    }
}
