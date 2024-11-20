using LMS.Data.Entities;

namespace LMS.Data.Repositories.Interfaces
{
    public interface ICardInfoRepository
    {
        Task CreateCardInfo(CardInfo cardInfo);
        //Task<List<CardInfo>> GetAllCardInfos();
        //Task<CardInfo> GetCardInfoById(Guid id);
        //Task UpdateCardInfo(CardInfo cardInfo);
    }
}