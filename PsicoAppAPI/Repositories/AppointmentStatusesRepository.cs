using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories;

public class AppointmentStatusesRepository : IAppointmentStatusesRepository
{
    private readonly DataContext _context;

    public AppointmentStatusesRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<AppointmentStatus?> GetAppointmentById(int id)
    {
        var status = await _context.AppointmentStatuses.FindAsync(id);
        return status;
    }

    public async Task<AppointmentStatus?> GetAppointmentByName(string name)
    {
        var status = await _context.AppointmentStatuses.FirstOrDefaultAsync(a => a.Name == name);
        return status;
    }

    public async Task<IEnumerable<AppointmentStatus>?> GetTags()
    {
        // The design of the applications uses a small data for statuses
        // so it's not necessary to use pagination or other techniques
        var status = await _context.AppointmentStatuses.ToListAsync();
        return status;
    }
}