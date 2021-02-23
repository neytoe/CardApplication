using CardApplication.Core.Interface;
using CardApplication.Model;
using CardApplication.Utilies;
using Moq;
using System;
using Xunit;

namespace CardApplication.Test
{
    public class PaymentControllerTest
    {
        private readonly Mock<ICheapPaymentGateway> _cheapPayment;
        private readonly Mock<IExpensivePaymentGateway> _expensivePayment;
        private readonly Mock<IPremiumPaymentGateway> _premiumPayment;

        public PaymentControllerTest()
        {
            _cheapPayment = new Mock<ICheapPaymentGateway>();
            _expensivePayment = new Mock<IExpensivePaymentGateway>();
            _premiumPayment = new Mock<IPremiumPaymentGateway>();
        }

        [Theory]
        [InlineData(20, "Neto Anyankah", "5334771104426541", "2/20/2021", "007")]
        [InlineData(10, "Neto Anyankah", "5500000000000004", "2/20/2021", "346")]
        [InlineData(15, "Neto Anyankah", "4111111111111111 ", "2/20/2021", "377")]
        public void CheapPaymentTest(decimal amount, string cardHolder, string creditCardNumber, string date, string code)
        {
            var payment = new Payment
            {
                Amount = amount,
                CardHolder = cardHolder,
                CreditCardNumber = creditCardNumber,
                ExpirationDate = DateTime.Parse(date),
                SecurityCode = code
            };

            _cheapPayment.Setup(x => x.ProcessPayment(payment)).ReturnsAsync(true);

            Assert.True(_cheapPayment.Object.ProcessPayment(payment).Result);
        }

        [Theory]
        [InlineData(22, "Neto Anyankah", "5334771104426541", "2/20/2021", "007")]
        [InlineData(230, "Neto Anyankah", "5500000000000004", "2/20/2021", "346")]
        [InlineData(415, "Neto Anyankah", "4111111111111111 ", "2/20/2021", "377")]
        public void ExpensivePaymentTest(decimal amount, string cardHolder, string creditCardNumber, string date, string code)
        {
            var payment = new Payment
            {
                Amount = amount,
                CardHolder = cardHolder,
                CreditCardNumber = creditCardNumber,
                ExpirationDate = DateTime.Parse(date),
                SecurityCode = code
            };

            _expensivePayment.Setup(x => x.ProcessPayment(payment)).ReturnsAsync(true);

            Assert.True(_expensivePayment.Object.ProcessPayment(payment).Result);
        }

        [Theory]
        [InlineData(1230, "Neto Anyankah", "5334771104426541", "2/20/2021", "007")]
        [InlineData(6700, "Neto Anyankah", "5500000000000004", "2/20/2021", "346")]
        [InlineData(10000, "Neto Anyankah", "4111111111111111 ", "2/20/2021", "377")]
        public void PremiumPaymentTest(decimal amount, string cardHolder, string creditCardNumber, string date, string code)
        {
            var payment = new Payment
            {
                Amount = amount,
                CardHolder = cardHolder,
                CreditCardNumber = creditCardNumber,
                ExpirationDate = DateTime.Parse(date),
                SecurityCode = code
            };

            _premiumPayment.Setup(x => x.ProcessPayment(payment)).ReturnsAsync(true);

            Assert.True(_premiumPayment.Object.ProcessPayment(payment).Result);
        }

        [Fact]
        public void CheckIfCreditCardIsValid()
        {
            string cardNumber = "5334771104426541";
            var result = Utilities.IsCardNumberValid(cardNumber);
            Assert.True(result);
        }

        [Fact]
        public void CheckIfCreditCardIsInvalid()
        {
            string cardNumber = "4111111111111223";
            var result = Utilities.IsCardNumberValid(cardNumber);
            Assert.False(result);
        }

        [Fact]
        public void CheckIfAmountIsValid()
        {
            decimal amount = 442651;
            var result = Utilities.IsAmountValid(amount);
            Assert.True(result);
        }

        [Fact]
        public void CheckIfAmountIsInvalid()
        {
            decimal amount = -6541;
            var result = Utilities.IsAmountValid(amount);
            Assert.False(result);
        }

        [Fact]
        public void CheckIfDateIsValid()
        {
            DateTime date = DateTime.Parse("2/23/2078");
            var result = Utilities.IsDateValid(date);
            Assert.True(result);
        }

        [Fact]
        public void CheckIfDateIsInvalid()
        {
            DateTime amount = DateTime.Parse("2/7/2021");
            var result = Utilities.IsDateValid(amount);
            Assert.False(result);
        }
    }
}