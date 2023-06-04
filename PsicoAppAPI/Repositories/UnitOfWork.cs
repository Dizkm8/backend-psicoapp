using PsicoAppAPI.Data;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IUserRepository userRepository = null!;
        private ISpecialistRepository specialistRepository = null!;
        private IRolesRepository rolesRepository = null!;


        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get
            {
                userRepository ??= new UserRepository(_context);
                return userRepository ?? throw new NotImplementedException();
            }
        }

        public ISpecialistRepository SpecialistRepository
        {
            get
            {
                specialistRepository ??= new SpecialistRepository(_context);
                return specialistRepository ?? throw new NotImplementedException();
            }
        }

        public IRolesRepository RolesRepository
        {
            get
            {
                rolesRepository ??= new RolesRepository(_context);
                return rolesRepository ?? throw new NotImplementedException();
            }
        }
    }
}