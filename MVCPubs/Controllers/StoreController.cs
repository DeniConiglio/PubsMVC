using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCPubs.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVCPubs.Controllers
{
    public class StoreController : Controller
    {
        private readonly pubsContext _context;

        public StoreController(pubsContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View("Index", _context.Stores.ToList());
        }

        public IActionResult Create()
        {
            Stores store = new Stores();
            return View("Create", store);
        }



        [HttpPost]
        public IActionResult Create(Stores store)
        {
            _context.Add(store);
            _context.SaveChanges();
            return RedirectToAction("Index")
            ;
        }



        public IActionResult Delete(string id)
        {
            var store = _context.Stores.SingleOrDefault(m => m.StorId == id);
            _context.Stores.Remove(store);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        //GET:store/Edit/id
        [HttpGet]
        public ActionResult Edit(string id)
        {

            Stores store = _context.Stores.Find(id);

            return View("Edit", store);

        } 


        //POST : store/Edit
        [HttpPost]
        public ActionResult Edit(Stores store)
        {

            if (ModelState.IsValid)
            {

                _context.Entry(store).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(store);

        }

        [HttpGet("/stores/{city}/{state}")]
        public IActionResult FiltrarPorCiudad(string city, string state)
        {
            List<Stores> stores = (from p in _context.Stores where p.City == city && p.State == state select p).ToList();
            return View("Index", stores);
        }

        [HttpGet("/stores/Details/{id}")]
        //GET:
        public IActionResult Details(string id)
        {
            Stores store = _context.Stores.Find(id);

            return View("Details", store);
        }
    }
}
