using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomBooking.Data;
using RoomBooking.Models;

namespace RoomBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly DataContext _context;

        public BookingController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Booking (Melihat daftar peminjaman)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
        {
            return await _context.Bookings.ToListAsync();
        }

        // POST: api/Booking (Menambah data peminjaman)
        [HttpPost]
        public async Task<ActionResult<Booking>> CreateBooking(Booking booking)
        {
            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return Ok(booking);
        }
    }
}