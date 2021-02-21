using CardApplication.Core.Interface;
using CardApplication.Model;
using System;
using System.Threading.Tasks;

namespace CardApplication.Core.Service
{
    public class PremiumPaymentService : IPremiumPaymentGateway
    {
        private readonly IGenericRepository<Transaction> _transactionRepo;
        private readonly IGenericRepository<Payment> _paymentRepo;

        public PremiumPaymentService(IGenericRepository<Transaction> transactionRepo,
            IGenericRepository<Payment> paymentRepo)
        {
            _transactionRepo = transactionRepo;
            _paymentRepo = paymentRepo;
        }

        public async Task<bool> ProcessPayment(Payment model)
        {
            model.Id = Guid.NewGuid().ToString();
            await _paymentRepo.Add(model);
            return await AddTransaction(PaymentStatus.processed, model.Id);
        }

        private async Task<bool> AddTransaction(PaymentStatus status, string id)
        {
            var transaction = new Transaction();
            transaction.Id = Guid.NewGuid().ToString();
            transaction.PaymentId = id;
            transaction.Status = status;
            return await _transactionRepo.Add(transaction);
        }
    }
}
