using LMS.Common.Dtos;
using LMS.Common.Models;
using LMS.Data.Entities;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;

namespace LMS.Service.Api
{
    public class CardInfoService
    {
        private readonly ICardInfoRepository _cardInfoRepository;
        private readonly IUserRepository _userRepository;
        public CardInfoService(ICardInfoRepository cardInfoRepository, IUserRepository userRepository)
        {
            _cardInfoRepository = cardInfoRepository;
            _userRepository = userRepository;
        }

        public async Task<CardInfoDto> EnterCardInfo(Guid userId, CreateCardInfoModel createCardInfoModel)
        {
            var user = await _userRepository.GetUserById(userId);
            var newCardInfo = new CardInfo
            {
                CardHolderNumber = createCardInfoModel.CardHolderNumber,
                CardNumber = createCardInfoModel.CardNumber,
                CVVHash = createCardInfoModel.CVVHash,
            };

            await _cardInfoRepository.CreateCardInfo(newCardInfo);
            return newCardInfo.ParseToDto();
        }
    }
}