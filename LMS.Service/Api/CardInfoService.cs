using LMS.Common.Dtos;
using LMS.Common.Models;
using LMS.Data.Entities;
using LMS.Data.Exceptions.CardInfo;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;
//BCrypt.Net.BCrypt.HashPassword(password),

namespace LMS.Service.Api
{
    public class CardInfoService(ICardInfoRepository cardInfoRepository, IUserRepository userRepository)
    {
        private readonly ICardInfoRepository _cardInfoRepository = cardInfoRepository;
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<CardInfoDto> EnterCardInfo(Guid userId, CreateCardInfoModel createCardInfoModel)
        {
            try
            {
                _ = await _userRepository.GetUserById(userId);
                var isValidateCard = IsValidateCard(createCardInfoModel.CardNumber, createCardInfoModel.CVV, createCardInfoModel.CardHolderNumber);
                if (isValidateCard)
                {
                    var newCardInfo = new CardInfo
                    {
                        CardHolderNumber = createCardInfoModel.CardHolderNumber,
                        CardNumber = createCardInfoModel.CardNumber,
                        CVVHash = createCardInfoModel.CVV,
                    };

                    await _cardInfoRepository.CreateCardInfo(newCardInfo);
                    return newCardInfo.ParseToDto();

                }
                else
                {
                    throw new CardInfoNotValidateException();
                }

            }
            catch (Exception ex)
            {
                throw new CardInfoNotValidateException();
            }
        }


        private static bool IsValidateCard(string cardNumber, string cvv, string cardHolderNumber)
        {
            if (cardNumber.Length == 16 && !string.IsNullOrEmpty(cvv) && !string.IsNullOrEmpty(cardHolderNumber))
            {
                return true;
            }
            return false;
        }
    }
}