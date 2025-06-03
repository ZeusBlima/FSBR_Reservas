using Microsoft.EntityFrameworkCore;
using ReservaSalasLibrary.Models;

namespace ReservaSalasLibrary.Data.Repositories
{
    public class SalaRepository : Repository<Sala>, ISalaRepository
    {
        public SalaRepository(ReservaSalasContext context) : base(context)
        {
        }

        // Adicione métodos específicos para Sala, se necessário
    }
}