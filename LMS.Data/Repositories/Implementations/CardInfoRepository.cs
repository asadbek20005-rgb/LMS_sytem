using LMS.Data.Context;
using LMS.Data.Entities;
using LMS.Data.Exceptions;
using LMS.Data.Exceptions.CardInfo;
using LMS.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data.Repositories.Implementations
{
    public class CardInfoRepository(AppDbContext appDbContext) : ICardInfoRepository
    {
        private readonly AppDbContext _context = appDbContext;

        public async Task CreateCardInfo(CardInfo cardInfo)
        {
            await _context.CardInfos.AddAsync(cardInfo);
            await _context.SaveChangesAsync();
        }




        //public async Task<List<CardInfo>> GetAllCardInfos()
        //{
        //    return await _context.CardInfos.ToListAsync();
        //}

        //public async Task<CardInfo> GetCardInfoById(Guid id)
        //{
        //    var cardInfo = await _context.CardInfos.FindAsync(id);
        //    return cardInfo ?? throw new CardInfoNotFoundException();
        //}

        //public async Task UpdateCardInfo(CardInfo cardInfo)
        //{
        //    _context.CardInfos.Update(cardInfo);
        //    await _context.SaveChangesAsync();
        //}
    }
}
