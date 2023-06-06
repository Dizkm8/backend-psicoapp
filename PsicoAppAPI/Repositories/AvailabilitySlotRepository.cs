using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories
{
    public class AvailabilitySlotRepository : IAvailabilitySlotRepository
    {
        private readonly DataContext _context;

        public AvailabilitySlotRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> AddAvailabilitiesToUser(IEnumerable<AvailabilitySlot> availabilities, string userId)
        {
            await _context.AvailabilitySlots.AddRangeAsync(availabilities);
            var result = await _context.SaveChangesAsync() > 0;
            return result;
        }

        public async Task<List<AvailabilitySlot>?> GetAvailabilitySlotsByUserId(string userId)
        {
            var availabilitySlots = await _context.AvailabilitySlots
                .Where(availabilitySlot => availabilitySlot.UserId == userId)
                .ToListAsync();
            return availabilitySlots;
        }

        public async Task<List<AvailabilitySlot>?> GetAvailabiliySlotByUserIdAndDateRange(string userId, DateOnly startDate, DateOnly endDate)
        {
            var availability = await _context.AvailabilitySlots
                .Where(x => x.UserId == userId)
                .ToListAsync();

            return availability
                .Where(x => DateOnly.FromDateTime(x.StartTime) >= startDate && DateOnly.FromDateTime(x.StartTime) <= endDate)
                .ToList();
        }
    }
}