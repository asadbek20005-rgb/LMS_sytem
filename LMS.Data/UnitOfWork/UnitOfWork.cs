using LMS.Data.Context;
using LMS.Data.Repositories.Implementations;
using LMS.Data.Repositories.Interfaces;

namespace LMS.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        //private bool _disposed = false;
        public IUserRepository _UserRepository;
        //public IChatRepository _ChatRepository;
        //public IUserChatRepository _UserChatRepository;
        //public IMessageRepository _MessageRepository;


        public UnitOfWork(AppDbContext context, IUserRepository userRepository)
        {
            _context = context;
            _UserRepository = userRepository;
        }



        public IUserRepository UserRepository
        {
            get
            {
                if (_UserRepository == null)
                {
                    _UserRepository = new UserRepository(_context);
                }
                return _UserRepository;
            }
        }
    }

}
