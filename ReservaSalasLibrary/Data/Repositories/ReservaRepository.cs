using Microsoft.EntityFrameworkCore;
using ReservaSalasLibrary.Data.Mappings;
using ReservaSalasLibrary.Models;

namespace ReservaSalasLibrary.Data.Repositories
{
    public class ReservaRepository : Repository<Reserva>, IReservaRepository
    {
        public ReservaRepository(ReservaSalasContext context) : base(context)
        {
        }

        public override async Task<Reserva> GetByIdAsync(int id)
        {
            var mapping = await _context.Reservas
                .FirstOrDefaultAsync(r => r.Id == id);

            if (mapping == null)
            {
                throw new KeyNotFoundException($"Reserva with ID {id} not found.");
            }

            var sala = await _context.Salas.FindAsync(mapping.SalaId);
            if (sala == null)
            {
                throw new KeyNotFoundException($"Sala with ID {mapping.SalaId} not found.");
            }

            return ReservaMapping.ToReserva(mapping, sala);
        }

        public override async Task<IEnumerable<Reserva>> GetAllAsync()
        {
            var mappings = await _context.Reservas
                .Select(r => new ReservaMapping
                {
                    Id = r.Id,
                    SalaId = r.SalaId,
                    DataInicio = r.DataInicio,
                    DataFim = r.DataFim
                })
                .ToListAsync();

            var reservas = new List<Reserva>();
            foreach (var mapping in mappings)
            {
                var sala = await _context.Salas.FindAsync(mapping.SalaId);
                if (sala == null)
                {
                    throw new KeyNotFoundException($"Sala with ID {mapping.SalaId} not found.");
                }
                reservas.Add(ReservaMapping.ToReserva(mapping, sala));
            }

            return reservas;
        }

        public override async Task AddAsync(Reserva entity)
        {
            var mapping = ReservaMapping.FromReserva(entity);
            
            await _context.Reservas.AddAsync(mapping);
            await _context.SaveChangesAsync();
        }

        public override void Update(Reserva entity)
        {
            var mapping = ReservaMapping.FromReserva(entity);

            _context.Reservas.Update(mapping);
            _context.SaveChanges();
        }
    
    }
}