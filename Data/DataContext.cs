using Microsoft.EntityFrameworkCore;
using RoomBooking.Models; // Pastikan ini sesuai dengan folder Models Anda

namespace RoomBooking.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Booking> Bookings { get; set; }
    }
}