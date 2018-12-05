using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzTravel.Models;
using OzTravel.Services;
using OzTravel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace OzTravel.Controllers
{
    public class OrderController : Controller
    {
        private UserManager<IdentityUser> _userManagerService;
        private IDataService<Order> _orderDataService;
        private IDataService<Package> _packageDataService;

        public OrderController(UserManager<IdentityUser> userManager,
                                 IDataService<Package> packageService,
                                 IDataService<Order> orderService)
        {
            _userManagerService = userManager;
            _packageDataService = packageService;
            _orderDataService = orderService;
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult Create(int id)
        {
            //int packageId = int.Parse(TempData["PackageId"].ToString());

            Package package = _packageDataService.GetSingle(p => p.PackageId == id);

            OrderCreateViewModel vm = new OrderCreateViewModel
            {
                PackageId = package.PackageId,
                Name = package.Name,
                Location = package.Location,
                Price = package.Price,
                Description = package.Description,
                Picture = package.Picture
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> Create(OrderCreateViewModel vm)
        {
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                Order order = new Order
                {
                    Date = DateTime.Now,
                    PackageName = vm.Name,
                    PackageId = vm.PackageId,
                    Quantity = vm.Quantity,
                    TotalPrice = vm.Quantity * vm.Price,
                    UserId = user.Id
                };
                _orderDataService.Create(order);
                return RedirectToAction("Details", "Order", new { id = order.OrderId });
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult Details(int id)
        {
            Order order = _orderDataService.GetSingle(o => o.OrderId == id);

            OrderDetailsViewModel vm = new OrderDetailsViewModel
            {
                OrderId = order.OrderId,
                PackageName = order.PackageName,
                Quantity = order.Quantity,
                TotalPrice = order.TotalPrice,
                Date = order.Date,
            };

            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> DetailsPast()
        {
            IdentityUser user = await _userManagerService.FindByNameAsync(User.Identity.Name);

            IEnumerable<Order> orderList = _orderDataService.Query(o => o.UserId == user.Id);

            OrderDetailsPastViewModel vm = new OrderDetailsPastViewModel
            {
                Orders = orderList,
                Count = orderList.Count()
            };

            return View(vm);
        }
    }
}