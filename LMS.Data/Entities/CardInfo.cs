namespace LMS.Data.Entities
{
    public class CardInfo
    {
        public Guid Id { get; set; }
        public required string CardNumber { get; set; }
        public required string CardHolderName { get; set; }
        public required string CVVHash { get; set; }
        public required Guid UserId { get; set; }
        public virtual User? User { get; set; }


    }
}
