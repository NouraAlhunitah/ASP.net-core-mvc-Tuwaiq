using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Hotels.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbcontext _context;
        private Hotel hotelupdate;

        public HomeController(ApplicationDbcontext context)
        {
            _context = context;
        }
        public IActionResult CreateNewRecord(Hotel hotels)
        {
            if (ModelState.IsValid)

            {
                _context.hotel.Add(hotels);
                _context.SaveChanges();
                return RedirectToAction("Index");

            }
            var hotel = _context.hotel.ToList();
            return View("Index",hotel);

		}
        public IActionResult Update(Hotel hotel)

        {
            if (ModelState.IsValid)
            {
                _context.hotel.Update(hotel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Edit");
        }



        public IActionResult Edit(int id)

        {
            var hotelupdate = _context.hotel.SingleOrDefault(x => x.Id == id);
            return View(hotelupdate);
        }
        public IActionResult Delete(int id)
        {

            var hoteldelete = _context.hotel.SingleOrDefault(x => x.Id == id);//search
            _context.hotel.Remove(hoteldelete); //delete
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Index (string City)

        {
            
            if (City != null){

                var hotels = _context.hotel.Where(x => x.City.Contains(City)).ToList();
                ViewBag.city = City;
                return View(hotels);
            } else {
                var hotels = _context.hotel.ToList();
                return View(hotels);
            }
           


        }


        public IActionResult Index()
        {
            var hotel = _context.hotel.ToList();
            return View(hotel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


   
    }

     


}
