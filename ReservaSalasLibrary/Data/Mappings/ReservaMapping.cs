using ReservaSalasLibrary.Models;

namespace ReservaSalasLibrary.Data.Mappings
{
    public class ReservaMapping
    {
        public int Id { get; set; }
        public int SalaId { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        public static Reserva ToReserva(ReservaMapping mapping, Sala sala)
        {
            return new Reserva
            {
                Id = mapping.Id,
                SalaId = mapping.SalaId,
                Periodo = new Periodo
                {
                    DataInicio = mapping.DataInicio,
                    DataFim = mapping.DataFim
                },
                Sala = sala
            };
        }

        public static ReservaMapping FromReserva(Reserva reserva)
        {
            return new ReservaMapping
            {
                Id = reserva.Id,
                SalaId = reserva.SalaId,
                DataInicio = reserva.Periodo.DataInicio,
                DataFim = reserva.Periodo.DataFim
            };
        }
    }
}