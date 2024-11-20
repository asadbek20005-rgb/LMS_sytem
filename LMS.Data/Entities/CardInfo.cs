namespace LMS.Data.Entities
{
    public class CardInfo
    {
        public Guid Id { get; set; }
        public required string CardNumber { get; set; }
        public required string CardHolderNumber { get; set; }
        public required string CVVHash { get; set; }
        public User? User { get; set; }


    }
}
