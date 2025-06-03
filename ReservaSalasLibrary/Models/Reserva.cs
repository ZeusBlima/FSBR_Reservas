namespace ReservaSalasLibrary.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int SalaId { get; set; }
        public required Periodo Periodo { get; set; }

        public required Sala Sala { get; set; }
    }
}