using LMS.Data.Repositories.Interfaces;

namespace LMS.Data.UnitOfWork
{
    internal interface IUnitOfWork
    {
       
            IUserRepository UserRepository { get; }
        
    }
}
