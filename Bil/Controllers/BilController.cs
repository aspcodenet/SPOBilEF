using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bil.Controllers
{
    public class BilController : Controller
    {
        // GET: Bil
        public ActionResult Index(string sort)
        {
            var model = new ViewModels.BilIndexViewModel();
            using (var db = new Models.BilModel2())
            {
                //var carsFromDB = db.Bilar.AsQueryable();
                //if (sort == "NamnAsc")
                //    carsFromDB = carsFromDB.OrderBy(r => r.Manufacturer);
                //else if (sort == "NamnDesc")
                //    carsFromDB = carsFromDB.OrderByDescending(r => r.Manufacturer);


                //if (sort == "YearAsc")
                //    carsFromDB = carsFromDB.OrderBy(r => r.Year);
                //else if (sort == "YearDesc")
                //    carsFromDB = carsFromDB.OrderByDescending(r => r.Year);

                //model.Cars.AddRange(carsFromDB.Select( r => new ViewModels.BilIndexViewModel.BilListViewModel
                //{
                //    Manufacturer = r.Manufacturer,
                //    Model = r.Model,
                //    Year = r.Year,
                //    Id = r.Id
                //}));




                model.Cars.AddRange(db.Bilar.Select(r => new ViewModels.BilIndexViewModel.BilListViewModel
                {
                    Manufacturer = r.Manufacturer,
                    Model = r.Model,
                    Year = r.Year,
                    Id = r.Id
                }));

                if (sort == "NamnAsc")
                    model.Cars = model.Cars.OrderBy(r => r.Manufacturer).ToList();
                else if (sort == "NamnDesc")
                    model.Cars = model.Cars.OrderByDescending(r => r.Manufacturer).ToList();


                if (sort == "YearAsc")
                    model.Cars = model.Cars.OrderBy(r => r.Year).ToList();
                else if (sort == "YearDesc")
                    model.Cars = model.Cars.OrderByDescending(r => r.Year).ToList();


                model.CurrentSort = sort;

                return View(model);
            }
        }


        public ActionResult Test()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new ViewModels.BilCreateViewModel();
           return View(model);
        }

        [HttpPost]
        public ActionResult Create(ViewModels.BilCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new Models.BilModel2())
            {
                var bil = new Models.Bil
                {
                    Color = model.Color,
                    Manufacturer = model.Manufacturer,
                    Model = model.Modell,
                    Year = model.Year
                };
                db.Bilar.Add(bil);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (var db = new Models.BilModel2())
            {
                var bil = db.Bilar.FirstOrDefault(p=>p.Id == id);
                var model = new ViewModels.BilEditViewModel
                {
                    Color = bil.Color,
                    Manufacturer = bil.Manufacturer,
                    Modell = bil.Model,
                    NumberOfWheels = bil.NumberOfWheels,
                    //Price = bil.Price,
                    Year = bil.Year,
                    Id = bil.Id
                };
                return View(model);
            }
        }




        [HttpGet]
        public ActionResult Search(string SearchManufacturer, string SearchYear)
        {
            using (var db = new Models.BilModel2())
            {
                var model = new ViewModels.BilIndexViewModel
                {
                    SearchManufacturer = SearchManufacturer,
                    SearchYear = SearchYear
                };
                model.Cars.AddRange(db.Bilar.ToList().Select(r => new ViewModels.BilIndexViewModel.BilListViewModel
                {
                    Manufacturer = r.Manufacturer,
                    Model = r.Model,
                    Year = r.Year,
                    Id = r.Id
                }).Where(c => Matches(c, SearchManufacturer, SearchYear)
                    ));

                return View("Index", model);
            }
        }

        bool Matches(ViewModels.BilIndexViewModel.BilListViewModel bil, string SearchManufacturer, string SearchYear)
        {
            if (!string.IsNullOrEmpty(SearchManufacturer))
            {
                SearchManufacturer = SearchManufacturer.ToLower();
                if (!bil.Manufacturer.ToLower().Contains(SearchManufacturer)) return false;
            }
            if (!string.IsNullOrEmpty(SearchYear))
            {
                if (!bil.Year.ToString().Contains(SearchYear)) return false;
            }
            return true;
        }

        [HttpPost]
        public ActionResult Edit(ViewModels.BilEditViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            using (var db = new Models.BilModel2())
            {
                var bil = db.Bilar.FirstOrDefault(r=>r.Id == model.Id);
                bil.Manufacturer = model.Manufacturer;
                bil.Model = model.Modell;
                //bil.Price = model.Price;
                bil.Year = model.Year;
                bil.NumberOfWheels = model.NumberOfWheels;
                bil.Color = model.Color;
                db.SaveChanges();
            }


            return RedirectToAction("Index");
        }


        public ActionResult Search(string q)
        {
            return View();
        }

        public ActionResult View(int id)
        {
            using (var db = new Models.BilModel2())
            {
                var bil = db.Bilar.FirstOrDefault(r=>r.Id == id);
                var model = new ViewModels.BilViewViewModel
                {
                    Color = bil.Color,
                    Manufacturer = bil.Manufacturer,
                    Model = bil.Model,
                    Price = bil.Price,
                    Year = bil.Year
                };

                return View(model);
            }
        }
    }
}