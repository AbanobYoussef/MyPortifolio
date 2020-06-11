using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<PortfoioItem> _portfoioItems;

        public HomeController(IUnitOfWork<Owner> owner,
            IUnitOfWork<PortfoioItem> PortfoioItems)
        {
            this._owner = owner;
            this._portfoioItems = PortfoioItems;
        }
        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                Owner=_owner.Entity.GetALL().First(),
                PortfoioItems=_portfoioItems.Entity.GetALL().ToList()
            };
            return View(homeViewModel);
        }

        public IActionResult About()
        {
            return View();
        }
    }
}