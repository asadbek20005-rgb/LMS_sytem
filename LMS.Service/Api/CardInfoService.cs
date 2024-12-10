using LMS.Common.Dtos;
using LMS.Common.Models;
using LMS.Data.Entities;
using LMS.Data.Exceptions.CardInfo;
using LMS.Data.Repositories.Interfaces;
using LMS.Service.Extensions;
using Microsoft.AspNetCore.Identity;
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
                var user = await _userRepository.GetUserById(userId);
                var isValidateCard = IsValidateCard(createCardInfoModel.CardNumber, createCardInfoModel.CVV, createCardInfoModel.CardHolderName);
                if (isValidateCard)
                {
                    var hasher = new PasswordHasher<CardInfo>();
                    var newCardInfo = new CardInfo
                    {
                        CardHolderName = hasher.HashPassword(null, createCardInfoModel.CardHolderName), 
                        CardNumber = hasher.HashPassword(null, createCardInfoModel.CardNumber),           
                        CVVHash = hasher.HashPassword(null, createCardInfoModel.CVV),
                        UserId = user.Id,
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


        private static bool IsValidateCard(string cardNumber, string cvv, string cardHolderName)
        {
            if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length != 16)
            {
                return false;
            }

            if(string.IsNullOrEmpty(cvv) || cvv.Length != 3)
            {
                return false;
            }

            if (string.IsNullOrEmpty(cardHolderName)){
                return false;
            }
            return true;

        }
    }
}