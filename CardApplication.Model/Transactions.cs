namespace CardApplication.Model
{
    public class Transactions
    {
        public string Id { get; set; }
        public PaymentStatus Status { get; set; }
        public string PaymentId { get; set; }
        public Payment Payment { get; set; }
    }
}

