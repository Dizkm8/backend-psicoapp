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

        public async Task<List<AvailabilitySlot>?> GetAvailabilitySlotsByUserId(string userId)
        {
            var availabilitySlots = await _context.AvailabilitySlots
                .Where(availabilitySlot => availabilitySlot.UserId == userId)
                .ToListAsync();
            return availabilitySlots;
        }
    }
}