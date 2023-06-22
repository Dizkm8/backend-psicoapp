using System;
using System.Threading.Tasks;
using PsicoAppAPI.Data;
using PsicoAppAPI.Repositories.Interfaces;

namespace PsicoAppAPI.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private IUserRepository _userRepository = null!;
        private ISpecialistRepository _specialistRepository = null!;
        private IRolesRepository _rolesRepository = null!;
        private IFeedPostRepository _feedPostRepository = null!;
        private ITagRepository _tagRepository = null!;
        private IAvailabilitySlotRepository _availabilitySlotRepository = null!;
        private IAppointmentRepository _appointmentRepository = null!;
        private IAppointmentStatusesRepository _statusesRepository = null!;
        private IForumPostRepository _forumPostRepository = null!;
        private IGptRulesRepository _gptRulesRepository = null!;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get
            {
                _userRepository ??= new UserRepository(_context);
                return _userRepository;
            }
        }

        public ISpecialistRepository SpecialistRepository
        {
            get
            {
                _specialistRepository ??= new SpecialistRepository(_context);
                return _specialistRepository;
            }
        }

        public IRolesRepository RolesRepository
        {
            get
            {
                _rolesRepository ??= new RolesRepository(_context);
                return _rolesRepository;
            }
        }

        public IFeedPostRepository FeedPostRepository
        {
            get
            {
                _feedPostRepository ??= new FeedPostRepository(_context);
                return _feedPostRepository;
            }
        }

        public ITagRepository TagRepository
        {
            get
            {
                _tagRepository ??= new TagRepository(_context);
                return _tagRepository;
            }
        }

        public IAvailabilitySlotRepository AvailabilitySlotRepository
        {
            get
            {
                _availabilitySlotRepository ??= new AvailabilitySlotRepository(_context);
                return _availabilitySlotRepository;
            }
        }

        public IAppointmentRepository AppointmentRepository
        {
            get
            {
                _appointmentRepository ??= new AppointmentRepository(_context);
                return _appointmentRepository;
            }
        }

        public IAppointmentStatusesRepository AppointmentStatusesRepository
        {
            get
            {
                _statusesRepository ??= new AppointmentStatusesRepository(_context);
                return _statusesRepository;
            }
        }

        public IForumPostRepository ForumPostRepository
        {
            get
            {
                _forumPostRepository ??= new ForumPostRepository(_context);
                return _forumPostRepository;
            }
        }

        public IGptRulesRepository GptRulesRepository
        {
            get
            {
                _gptRulesRepository ??= new GptRulesRepository(_context);
                return _gptRulesRepository;
            }
        }
    }
}