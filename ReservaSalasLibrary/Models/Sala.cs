namespace ReservaSalasLibrary.Models
{
    public class Sala
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public int Capacidade { get; set; }
    }
}