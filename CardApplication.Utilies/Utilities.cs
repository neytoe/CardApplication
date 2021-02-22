using CardApplication.Dto;
using System;
using System.Text.RegularExpressions;

namespace CardApplication.Utilies
{
    public static class Utilities
    {
        public static bool IsDateValid(DateTime date)
        {
            return date > DateTime.Now;
        }

        public static bool IsAmountValid(decimal amount)
        {
            return amount > 0;
        }

        public static bool IsCardNumberValid(string cardNumber)
        {
            int i, checkSum = 0;
            // Compute checksum of every other digit starting from right-most digit
            for (i = cardNumber.Length - 1; i >= 0; i -= 2)
                checkSum += (cardNumber[i] - '0');
            // Now take digits not included in first checksum, multiple by two,
            // and compute checksum of resulting digits
            for (i = cardNumber.Length - 2; i >= 0; i -= 2)
            {
                int val = ((cardNumber[i] - '0') * 2);
                while (val > 0)
                {
                    checkSum += (val % 10);
                    val /= 10;
                }
            }
            // Number is valid if sum of both checksums MOD 10 equals 0
            return ((checkSum % 10) == 0);
        }

        public static bool IsModelValid(PaymentDto model)
        {
            var amount = IsAmountValid(model.Amount);
            var creditCardNumber = IsCardNumberValid(model.CreditCardNumber);
            var expirationDate = IsDateValid(model.ExpirationDate);

            if (amount && creditCardNumber && expirationDate) return true;

            return false;
        }
    }
}
