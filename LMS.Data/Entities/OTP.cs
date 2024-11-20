using System.ComponentModel.DataAnnotations;

namespace LMS.Data.Entities
{
    public class OTP
    {
        [Key] public Guid Id { get; set; }
        public required string PhoneNumber { get; set; }
        public required int Code { get; set; }
        public required bool IsExpired { get; set; }
    }
}
