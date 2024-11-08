namespace LMS.Common.Models
{
    public class CreateCardInfoModel
    {
        public required string CardNumber { get; set; }
        public required string CardHolderNumber { get; set; }
        public required string CVVHash { get; set; }
    }
}
