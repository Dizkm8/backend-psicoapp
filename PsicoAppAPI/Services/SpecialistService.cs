using PsicoAppAPI.DTOs;
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
            if(availabilities is null) return false;

            var availability = availabilities.FirstOrDefault(x => x.StartTime == startTime);
            return availability is not null;
        }
        // public async Task<IEnumerable<SpecialistDto>> GetAllSpecialists(IEnumerable<User> users)
        // {
        //     var tasks =
        //         users.Select(async x => await _unitOfWork.SpecialistRepository.GetSpecialistById(x.Id));
        //     var specialists = await Task.WhenAll(tasks);
        //     var test = specialists.Select(x => 
        //         await _unitOfWork.Specialities
        //         );
        //     return specialists;
        // }


        public async Task<List<AvailabilitySlot>?> GetAllAvailability(string? userId)
        {
            if(userId is null) return null;
            var availabilitySlots = await _unitOfWork.AvailabilitySlotRepository.GetAvailabilitySlotsByUserId(userId);
            return availabilitySlots;
        }

        public async Task<List<AvailabilitySlot>?> GetAvailabilityByDate(string? userId, DateOnly StartDate,
            DateOnly EndDate)
        {
            if(userId is null) return null;
            if(StartDate > EndDate) return null;
            var repository = _unitOfWork.AvailabilitySlotRepository;
            var availability = await repository.GetAvailabiliySlotByUserIdAndDateRange(userId, StartDate, EndDate);
            return availability;
        }
    }
}
