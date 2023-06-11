using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using PsicoAppAPI.Models;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly DataContext _context;

        public TagRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Tag?> GetTagById(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            return tag;
        }

        public async Task<Tag?> GetTagByName(string name)
        {
            var tag = await _context.Tags.FirstOrDefaultAsync(t => t.Name == name);
            return tag;
        }

        public async Task<IEnumerable<Tag>?> GetTags()
        {
            // The design of the applications uses a small data for tags
            // so it's not necessary to use pagination or other techniques
            var tags = await _context.Tags.ToListAsync();
            return tags;
        }
    }
}