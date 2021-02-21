using System;
using System.ComponentModel.DataAnnotations;

namespace CardApplication.Dto
{
    public class PaymentDto
    {
        [Required]
        public string CreditCardNumber { get; set; }

        [Required]
        public string CardHolder { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }
        
        [MaxLength(3, ErrorMessage = "Name cannot exceed 3 characters")]
        [MinLength(3, ErrorMessage = "Name cannot be less than 3 characters")]
        public string SecurityCode { get; set; }

        public decimal Amount { get; set; }
    }
}
