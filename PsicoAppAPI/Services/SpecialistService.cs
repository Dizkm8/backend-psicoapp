using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class SpecialistService : ISpecialistService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpecialistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ??
                throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<List<Tuple<DateTime, bool>>?> GetSpecialistAvailability(string? userId)
        {
            if (userId is null) return null;
            var availabilitySlots = await _unitOfWork.AvailabilitySlotRepository.GetAvailabilitySlotsByUserId(userId);
            if (availabilitySlots is null) return null;
            var availability = availabilitySlots.Select
                (x => new Tuple<DateTime, bool>(x.StartTime, x.IsAvailable)).ToList();
            return availability;

        }
    }
}