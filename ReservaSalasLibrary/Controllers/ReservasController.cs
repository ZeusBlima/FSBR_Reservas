using Microsoft.AspNetCore.Mvc;
using ReservaSalasLibrary.Data;
using ReservaSalasLibrary.Data.UnitOfWork;
using ReservaSalasLibrary.Models;

namespace ReservaSalasLibrary.Controllers
{
    [ApiController]
    [Route("api/Reservas")]
    public class ReservasController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservasController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reserva>>> GetReservas()
        {
            return Ok(await _unitOfWork.Reservas.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reserva>> GetReserva(int id)
        {
            var reserva = await _unitOfWork.Reservas.GetByIdAsync(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return Ok(reserva);
        }

        [HttpPost]
        public async Task<ActionResult<Reserva>> PostReserva(Reserva reserva)
        {
            if (reserva.Periodo.DataInicio >= reserva.Periodo.DataFim)
            {
                return BadRequest("Data de início deve ser anterior à data de fim.");
            }

            await _unitOfWork.Reservas.AddAsync(reserva);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetReserva), new { id = reserva.Id }, reserva);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReserva(int id, Reserva reserva)
        {
            if (id != reserva.Id)
            {
                return BadRequest();
            }

            if (reserva.Periodo.DataInicio >= reserva.Periodo.DataFim)
            {
                return BadRequest("Data de início deve ser anterior à data de fim.");
            }

            _unitOfWork.Reservas.Update(reserva);

            try
            {
                await _unitOfWork.CompleteAsync();
            }
            catch
            {
                if (await _unitOfWork.Reservas.GetByIdAsync(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var reserva = await _unitOfWork.Reservas.GetByIdAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }

            _unitOfWork.Reservas.Delete(reserva);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}