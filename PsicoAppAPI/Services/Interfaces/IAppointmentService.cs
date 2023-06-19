using PsicoAppAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PsicoAppAPI.Services.Interfaces
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAppointmentsByUser(int userId);
    }
}
