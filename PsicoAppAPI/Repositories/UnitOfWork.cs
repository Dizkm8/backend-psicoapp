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
        private IFeedPostRepository feedPostRepository = null!;
        private ITagRepository tagRepository = null!;
        private IAvailabilitySlotRepository availabilitySlotRepository = null!;
        private ISpecialitiesRepository _specialitiesRepository = null!;

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

        public IFeedPostRepository FeedPostRepository
        {
            get
            {
                feedPostRepository ??= new FeedPostRepository(_context);
                return feedPostRepository ?? throw new NotImplementedException();
            }
        }

        public ITagRepository TagRepository
        {
            get
            {
                tagRepository ??= new TagRepository(_context);
                return tagRepository ?? throw new NotImplementedException();
            }
        }

        public IAvailabilitySlotRepository AvailabilitySlotRepository
        {
            get
            {
                availabilitySlotRepository ??= new AvailabilitySlotRepository(_context);
                return availabilitySlotRepository ?? throw new NotImplementedException();
            }
        }
        public ISpecialitiesRepository SpecialitiesRepository
        {
            get
            {
                _specialitiesRepository ??= new SpecialitiesRepository(_context);
                return _specialitiesRepository ?? throw new NotImplementedException();
            }
        }
    }
}
