using Microsoft.AspNetCore.Mvc;
using ReservaSalasLibrary.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace ReservaSalasWEB.Controllers
{
    public class ReservationController : Controller
    {
        private readonly HttpClient _httpClient;

        public ReservationController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                var response = await _httpClient.PostAsJsonAsync("http://localhost:5000/api/reservas", reserva);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Failed to create reservation.");
            }
            return View(reserva);
        }
    }
}