using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Infrastucture;
using Web.ViewModels;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Web.Controllers
{
    public class PortfoioItemsController : Controller
    {
        private readonly IUnitOfWork<PortfoioItem> _portfoioItems;
        private readonly IHostingEnvironment _hosting;

        public PortfoioItemsController(
            IUnitOfWork<PortfoioItem> PortfoioItems,
            IHostingEnvironment hosting)
        {
            this._portfoioItems = PortfoioItems;
            this._hosting = hosting;
        }

        // GET: PortfoioItems
        public IActionResult Index()
        {
            return View(_portfoioItems.Entity.GetALL());
        }

        // GET: PortfoioItems/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfoioItem = _portfoioItems.Entity.GetById(id);
            if (portfoioItem == null)
            {
                return NotFound();
            }

            return View(portfoioItem);
        }

        // GET: PortfoioItems/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortfoioItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create(PortfolioViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.File != null)
                {
                    string upload = Path.Combine(_hosting.WebRootPath, @"img/portfolio");
                    string fullPath = Path.Combine(upload, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullPath, FileMode.Create));
                }
                PortfoioItem portfoio = new PortfoioItem
                {
                    ProjectName = model.ProjectName,
                    Descrption = model.Descrption,
                    ImageUrl = model.File.FileName
                };
                _portfoioItems.Entity.Insert(portfoio);
                _portfoioItems.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortfoioItems/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfoioItem = _portfoioItems.Entity.GetById(id);
            if (portfoioItem == null)
            {
                return NotFound();
            }
            return View(portfoioItem);
        }

        // POST: PortfoioItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, PortfoioItem model , IFormFile? load)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                try
                {
                    if (load != null)
                    {
                        string upload = Path.Combine(_hosting.WebRootPath, @"img/portfolio");
                        string fullPath = Path.Combine(upload, load.FileName);
                        load.CopyTo(new FileStream(fullPath, FileMode.Create));
                    }
                    PortfoioItem portfoio = new PortfoioItem
                    {
                        Id = model.Id,
                        ProjectName = model.ProjectName,
                        Descrption = model.Descrption,
                        ImageUrl = load!=null ? load.FileName:model.ImageUrl
                    };
                    _portfoioItems.Entity.Update(portfoio);
                    _portfoioItems.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfoioItemExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: PortfoioItems/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfoioItem = _portfoioItems.Entity.GetById(id);
            if (portfoioItem == null)
            {
                return NotFound();
            }

            return View(portfoioItem);
        }

        // POST: PortfoioItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _portfoioItems.Entity.Delete(id);
            _portfoioItems.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfoioItemExists(Guid id)
        {
            return _portfoioItems.Entity.GetALL().Any(e => e.Id == id);
        }
    }
}
