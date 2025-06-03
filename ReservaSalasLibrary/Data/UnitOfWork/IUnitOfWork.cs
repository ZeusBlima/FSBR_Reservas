using ReservaSalasLibrary.Data.Repositories;

namespace ReservaSalasLibrary.Data.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ISalaRepository Salas { get; }
        IReservaRepository Reservas { get; }
        Task<int> CompleteAsync();
    }
}