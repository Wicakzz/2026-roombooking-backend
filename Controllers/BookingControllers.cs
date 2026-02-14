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

       // 3a. Update Data Lengkap (Dari Form Edit)
[HttpPut("{id}")]
public async Task<IActionResult> UpdateBooking(int id, Booking booking)
{
    if (id != booking.Id) return BadRequest("ID mismatch");

    var existing = await _context.Bookings.FindAsync(id);
    if (existing == null) return NotFound();

    existing.RoomName = booking.RoomName;
    existing.UserName = booking.UserName;
    existing.StartTime = booking.StartTime;
    existing.EndTime = booking.EndTime;
    existing.Status = booking.Status;

    await _context.SaveChangesAsync();
    return NoContent();
}

// 3b. Update Status Saja (Dari Tombol Acc/Reject)
[HttpPut("{id}/status")]
public async Task<IActionResult> UpdateStatus(int id, [FromBody] string status)
{
    var booking = await _context.Bookings.FindAsync(id);
    if (booking == null) return NotFound();

    booking.Status = status;
    await _context.SaveChangesAsync();
    return NoContent();
}

        // 4. Hapus Peminjaman
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}