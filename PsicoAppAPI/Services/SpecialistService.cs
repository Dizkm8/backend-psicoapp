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

        public async Task<bool> ExistsAvailability(string userId, DateTime startTime)
        {
            var availabilities = await _unitOfWork.AvailabilitySlotRepository.GetAvailabilitySlotsByUserId(userId);
            if (availabilities is null) return false;

            var availability = availabilities.FirstOrDefault(x => x.StartTime == startTime);
            return availability is not null;
        }

        public async Task<bool> DisableAvailability(string userId, DateTime availabilityDate)
        {
            var availabilities = await _unitOfWork.AvailabilitySlotRepository.GetAvailabilitySlotsByUserId(userId);
            var availability = availabilities?.FirstOrDefault(x => x.StartTime == availabilityDate && x.IsAvailable);
            if (availability is null) return false;
            
            availability.IsAvailableOverride = false;
            return true;
        }

        public async Task<List<AvailabilitySlot>?> GetAllAvailability(string? userId)
        {
            if (userId is null) return null;
            var availabilitySlots = await _unitOfWork.AvailabilitySlotRepository.GetAvailabilitySlotsByUserId(userId);
            return availabilitySlots;
        }

        public async Task<List<AvailabilitySlot>?> GetAvailabilityByDate(string? userId, DateOnly StartDate,
            DateOnly EndDate)
        {
            if (userId is null) return null;
            if (StartDate > EndDate) return null;
            var repository = _unitOfWork.AvailabilitySlotRepository;
            var availability = await repository.GetAvailabiliySlotByUserIdAndDateRange(userId, StartDate, EndDate);
            return availability;
        }
    }
}