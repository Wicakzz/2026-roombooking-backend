using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoomBooking.Data;
using RoomBooking.Models;

namespace RoomBooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly DataContext _context;

        public RoomController(DataContext context)
        {
            _context = context;
        }

        // 1. GET: api/room
        // Mengambil semua daftar ruangan
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        // 2. GET: api/room/{id}
        // Mengambil detail satu ruangan berdasarkan ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound("Ruangan tidak ditemukan.");
            return room;
        }

        // 3. POST: api/room
        // Menambah ruangan baru
        [HttpPost]
        public async Task<ActionResult<Room>> CreateRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRoom), new { id = room.Id }, room);
        }

        // 4. PUT: api/room/{id}
        // Update data ruangan secara keseluruhan
        // Pastikan routenya jelas menerima ID
[HttpPut("{id}")]
public async Task<IActionResult> UpdateRoom(int id, [FromBody] Room room)
{
    // Cek apakah id di URL sama dengan id di Body
    if (id != room.Id) return BadRequest("ID tidak sesuai.");

    var existingRoom = await _context.Rooms.FindAsync(id);
    if (existingRoom == null) return NotFound("Ruangan tidak ditemukan.");

    // Update field yang diperbolehkan
    existingRoom.Capacity = room.Capacity;
    existingRoom.Status = room.Status;
    // Jika ingin nama & lokasi bisa diubah juga, buka komen ini:
    // existingRoom.Name = room.Name;
    // existingRoom.Location = room.Location;

    await _context.SaveChangesAsync();
    return NoContent(); // Status 204 (Sukses)
}
        // 5. DELETE: api/room/{id}
        // Menghapus ruangan
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound("Ruangan tidak ditemukan.");

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Helper untuk cek keberadaan room
        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}