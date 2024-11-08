using LMS.Data.Entities;

namespace LMS.Data.Repositories.Interfaces
{
    public interface IUser_Course_Report
    {
        Task CreateUser_Course_Report(User_Course_Report user_Course_Report);
        Task<List<User_Course_Report>> GetAllUser_Course_Repos();
        Task<User_Course_Report> GetUser_Course_ReportByUserId(Guid userId);
        Task<User_Course_Report> GetUser_Course_ReportByCourseId(Guid courseId);
        Task UpdateUser_Course_Report(User_Course_Report user_Course_Report);
        Task DeleteUser_Course_Report(User_Course_Report user_Course_Report);
    }
}
