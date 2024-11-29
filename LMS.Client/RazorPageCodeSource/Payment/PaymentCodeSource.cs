using LMS.Client.Integrations.Payment;
using LMS.Common.Dtos;
using LMS.Common.Models;
using Microsoft.AspNetCore.Components;
using System.Net;

namespace LMS.Client.RazorPageCodeSource.Payment
{
    public class PaymentCodeSource : ComponentBase
    {
        [Inject] private IPaymentIntegration PaymentIntegration {  get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        protected CreateUser_Course_Payment paymentModel { get; set; } = new();
        protected CourseDto courseDto { get; set; } = new();
        protected CreateCardInfoModel model { get; set; } = new();
        [Parameter] public Guid CourseId { get; set; }

        protected async Task Pay()
        {
            var statusCode = await PaymentIntegration.Pay(CourseId, paymentModel, model);
            if (statusCode == HttpStatusCode.OK)
                NavigationManager.NavigateTo("/clientCourse");
        }


    }
}
