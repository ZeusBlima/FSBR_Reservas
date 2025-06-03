using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ReservaSalasWEB.Models;
using System.Net.Http;
using System.Net.Http.Json;
using ReservaSalasLibrary.Models;
using AspNetCoreGeneratedDocument;

namespace ReservaSalasWEB.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly HttpClient _httpClient;

    public HomeController(ILogger<HomeController> logger, HttpClient httpClient)
    {
        _logger = logger;
        _httpClient = httpClient;
    }

    public async Task<IActionResult> Index()
    {
        var reservations = await _httpClient.GetFromJsonAsync<IEnumerable<Reserva>>("http://localhost:5000/api/reservas");
        return View(reservations);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
