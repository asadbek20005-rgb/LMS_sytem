namespace LMS.Data.Repositories.Interfaces
{
    public interface IUser_CourseRepository
    {
        Task AddUserCourse(Data.Entities.User_Course user_Course);
    }
}
