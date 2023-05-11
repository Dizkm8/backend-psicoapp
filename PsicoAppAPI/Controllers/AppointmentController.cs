using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Models;
using PsicoAppAPI.Data;

namespace PsicoAppAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly DataContext _context;

    public AppointmentsController(DataContext context) => _context = context;
    /// <summary>
    /// Add a appointment in database context 
    /// </summary>
    /// <param name="appointment">appointments to add</param>
    /// <returns>Comment saved</returns> 
    [HttpPost]
    public IActionResult AddAppointment(Appointment appointment)
    {
        _context.Appointments.Add(appointment);
        _context.SaveChanges();
        return Ok(appointment);
    }
    
    /// <summary>
    /// Get a Appointment by id in database context
    /// </summary>
    /// <param name="id">Appointment id</param>
    /// <returns>Operation result</returns>
    [HttpGet("{id}")]
    public IActionResult GetAppointment(int id)
    {
        var appointment = _context.Appointments.FirstOrDefault(e => e.Id == id);
        if (appointment == default(Appointment))
        {
            return NotFound();
        }
        return Ok(appointment);
    }
    
    /// <summary>
    /// Get all Appointments of a user in database context
    /// </summary>
    /// <param name="clienteId">Client Rut</param>
    /// <returns>All appointments collected</returns>
    [HttpGet("{clientId}")]
    public IActionResult GetAppointmentsClient(String ClientId)
    {
        var appointments = _context.Appointments
            .Where(e => e.ClientId == ClientId )
            .ToList();
        return Ok(appointments);
    }
    
    /// <summary>
    /// Get all Appointments in database context
    /// </summary>
    /// <returns>All Appointments collected</returns>
    [HttpGet]
    public IActionResult GetAppointmentsAdmin()
    {
        var appointments = _context.Appointments.ToList();
        return Ok(appointments);
    }

}