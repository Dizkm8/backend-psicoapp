using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;
using PsicoAppAPI.Services.Interfaces;

namespace PsicoAppAPI.Services
{
    public class SpecialistService : ISpecialistService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SpecialistService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<bool> AddAvailabilities(IEnumerable<AvailabilitySlot> availabilities, string userId)
        {
            var result = await _unitOfWork.AvailabilitySlotRepository.AddAvailabilitiesToUser(availabilities, userId);
            return result;
        }

        public async Task<List<AvailabilitySlot>?> GetAllAvailability(string? userId)
        {
            if (userId is null) return null;
            var availabilitySlots = await _unitOfWork.AvailabilitySlotRepository.GetAvailabilitySlotsByUserId(userId);
            return availabilitySlots;
        }

        public async Task<List<AvailabilitySlot>?> GetAvailabilityByDate(string? userId, DateOnly StartDate, DateOnly EndDate)
        {
            if (userId is null) return null;
            if (StartDate > EndDate) return null;
            var repository = _unitOfWork.AvailabilitySlotRepository;
            var availability = await repository.GetAvailabiliySlotByUserIdAndDateRange(userId, StartDate, EndDate);
            return availability;
        }
    }
}