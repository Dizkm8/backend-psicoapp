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

        public async Task<IEnumerable<SpecialistDto>> GetSpecialistsByUserList(IEnumerable<User> users)
        {
            // First we get all the specialists from the table matching Id from user with UserId from specialist
            var specialistsTask =
                users.ToList().Select(async x =>
                {
                    var specialist = await _unitOfWork.SpecialistRepository.GetSpecialistById(x.Id);
                    // In order to encapsulate all the data to work with I use anonymous structs
                    // to temporary save entities data (and reduce queries to db)
                    var tempObject = new
                    {
                        UserId = x.Id,
                        FullName = $"{x.Name} {x.FirstLastName} {x.SecondLastName}",
                        specialist?.SpecialityId,
                        specialityName = string.Empty
                    };
                    return tempObject;
                }).ToList();
            var possiblyNullSpecialists = await Task.WhenAll(specialistsTask);
            // Dismiss all null values
            var specialists = possiblyNullSpecialists.Where(
                x => x.SpecialityId is not null);
            // Now we have all specialist fullName and their userId, but we have specialityId instead of speciality name
            // so, for each specialist we search in the specialities table
            var specialitiesTask = specialists.Select(async x =>
                {
                    var specialityName =
                        await _unitOfWork.SpecialitiesRepository.GetSpecialityById(x.SpecialityId ?? -1);
                    return new SpecialistDto()
                    {
                        UserId = x.UserId,
                        FullName = x.FullName,
                        Speciality = specialityName?.Name ?? string.Empty
                    };
                }
            );
            var specialistsDto = await Task.WhenAll(specialitiesTask);
            return specialistsDto;
        }


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
