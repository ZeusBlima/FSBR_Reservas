using ReservaSalasLibrary.Data.Repositories;

namespace ReservaSalasLibrary.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ReservaSalasContext _context;

        public UnitOfWork(ReservaSalasContext context)
        {
            _context = context;
            Salas = new SalaRepository(context);
            Reservas = new ReservaRepository(context);
        }

        public ISalaRepository Salas { get; private set; }
        public IReservaRepository Reservas { get; private set; }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}