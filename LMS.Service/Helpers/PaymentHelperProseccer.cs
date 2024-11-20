using LMS.Common.Constants;

namespace LMS.Service.Helpers;

public class PaymentHelperProseccer
{
    public static Task<decimal> CalculateAmount(decimal amount)
    {
        decimal amountWithTax = amount + amount * Constants.TaxRate;
        return Task.FromResult(amountWithTax);
    }
}  