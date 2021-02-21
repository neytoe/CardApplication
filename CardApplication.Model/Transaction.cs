namespace CardApplication.Model
{
    public class Transaction
    {
        public string Id { get; set; }
        public PaymentStatus Status { get; set; }
        public string PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}

