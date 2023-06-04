using Microsoft.EntityFrameworkCore;
using PsicoAppAPI.Data;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly DataContext _context;

        public RolesRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Tuple<int, string>> AdminRole()
        {
            var role = await _context.Roles.
                FirstOrDefaultAsync(r => r.Name.ToUpper() == "ADMIN");
            if (role is null) throw new Exception("Internal repository error searching for admin role");
            return new Tuple<int, string>(role.Id, role.Name);
        }

        public async Task<Tuple<int, string>> ClientRole()
        {
            var role = await _context.Roles.
                FirstOrDefaultAsync(r => r.Name.ToUpper() == "CLIENT");
            if (role is null) throw new Exception("Internal repository error searching for client role");
            return new Tuple<int, string>(role.Id, role.Name);
        }

        public async Task<Tuple<int, string>> SpecialistRole()
        {
            var role = await _context.Roles.
                FirstOrDefaultAsync(r => r.Name.ToUpper() == "SPECIALIST");
            if (role is null) throw new Exception("Internal repository error searching for specialist role");
            return new Tuple<int, string>(role.Id, role.Name);
        }
    }
}