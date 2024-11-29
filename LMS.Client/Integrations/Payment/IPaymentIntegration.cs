using LMS.Common.Models;
using System.Net;

namespace LMS.Client.Integrations.Payment
{
    public interface IPaymentIntegration
    {
        Task<HttpStatusCode> Pay(Guid courseId, CreateUser_Course_Payment createUser_Course_Payment, CreateCardInfoModel createCardInfoModel);
    }
}
