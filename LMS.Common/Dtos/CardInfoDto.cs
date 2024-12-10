namespace LMS.Common.Dtos
{
    public class CardInfoDto
    {
        public Guid Id { get; set; }
        public required string CardNumber { get; set; }
        public required string CardHolderName { get; set; }
        public required string CVVHash { get; set; }

        public UserDto? User { get; set; }
    }
}
