using System;
using System.Threading.Tasks;
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
        private IAppointmentRepository appointmentRepository = null!;
        private IAppointmentStatusesRepository statusesRepository = null!;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get
            {
                userRepository ??= new UserRepository(_context);
                return userRepository;
            }
        }

        public ISpecialistRepository SpecialistRepository
        {
            get
            {
                specialistRepository ??= new SpecialistRepository(_context);
                return specialistRepository;
            }
        }

        public IRolesRepository RolesRepository
        {
            get
            {
                rolesRepository ??= new RolesRepository(_context);
                return rolesRepository;
            }
        }

        public IFeedPostRepository FeedPostRepository
        {
            get
            {
                feedPostRepository ??= new FeedPostRepository(_context);
                return feedPostRepository;
            }
        }

        public ITagRepository TagRepository
        {
            get
            {
                tagRepository ??= new TagRepository(_context);
                return tagRepository;
            }
        }

        public IAvailabilitySlotRepository AvailabilitySlotRepository
        {
            get
            {
                availabilitySlotRepository ??= new AvailabilitySlotRepository(_context);
                return availabilitySlotRepository;
            }
        }

        public IAppointmentRepository AppointmentRepository
        {
            get
            {
                appointmentRepository ??= new AppointmentRepository(_context);
                return appointmentRepository;
            }
        }

        public IAppointmentStatusesRepository AppointmentStatusesRepository
        {
            get
            {
                statusesRepository ??= new AppointmentStatusesRepository(_context);
                return statusesRepository;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}