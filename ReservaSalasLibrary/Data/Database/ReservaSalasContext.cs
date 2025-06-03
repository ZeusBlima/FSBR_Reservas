using Microsoft.EntityFrameworkCore;
using ReservaSalasLibrary.Data.Mappings;
using ReservaSalasLibrary.Models;

namespace ReservaSalasLibrary.Data
{
    public class ReservaSalasContext : DbContext
    {
        public ReservaSalasContext(DbContextOptions<ReservaSalasContext> options) : base(options)
        {
        }

        public DbSet<Sala> Salas { get; set; }
        public DbSet<ReservaMapping> Reservas { get; set; }

    }
}