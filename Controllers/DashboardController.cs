using Hotels.Data;
using Hotels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Hotels.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbcontext _context;

        public DashboardController(ApplicationDbcontext context)
        {
            _context = context;
        }
        public IActionResult Delete(int Id)
        {
            var hotelDel = _context.hotel.SingleOrDefault(x => x.Id == Id);
            if (hotelDel != null)
            {
                _context.hotel.Remove(hotelDel);
                _context.SaveChanges();
                ViewBag.msg = "ok";

            }

            return RedirectToAction("Index");
        }

        public IActionResult CreateNewRooms(Rooms rooms)
        {
            _context.rooms.Add(rooms);
            _context.SaveChanges();

            return RedirectToAction("Rooms");

        }

        [HttpPost]
        public IActionResult Index(string city)
        {
            var hotel = _context.hotel.Where(x => x.City.Equals(city));
            return View(hotel);

        }
		public IActionResult AddRoomDetails(RoomDetails roomDetails)
		{
            if (ModelState.IsValid)
            {
                _context.RoomDetails.Add(roomDetails);
                _context.SaveChanges();
                return RedirectToAction("RoomDetails");

			}
            var RoomDetails = _context.RoomDetails.ToList();
            return View("RoomDetails", roomDetails);

		}
        public IActionResult Rooms()
        {
            var hotel = _context.hotel.ToList();
            ViewBag.hotel = hotel;

            ViewBag.currentuser = Request.Cookies["UserName"];
			var rooms = _context.rooms.ToList();
             return View(rooms);
        }

		[Authorize]
		public IActionResult Index()
        {
            var currentuser = HttpContext.User.Identity.Name;
            ViewBag.currentuser = currentuser;
            CookieOptions options = new CookieOptions();
            //options.Expires = DateTime.Now.AddMinutes(20);
            Response.Cookies.Append("UseName", currentuser, options);
            var hotel = _context.hotel.ToList();
			return View(hotel);
        }

		public IActionResult Edit(int id)

		{
			var hoteledit = _context.hotel.SingleOrDefault(x => x.Id == id);
			return View(hoteledit);
		}

        public IActionResult Update(Hotel hotel)
        {

            _context.hotel.Update( hotel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CreateNewHotel(Hotel hotel)
        {
            if (ModelState.IsValid) 
            {
                _context.hotel.Add(hotel);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index", hotel);
            
        }

    }
}
