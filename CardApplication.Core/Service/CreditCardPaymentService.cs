using CardApplication.Core.Interface;
using CardApplication.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;


namespace CardApplication.Core.Service
{
    public class CreditCardPaymentService
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IGenericRepository<Payment> _paymentRepo;

        public CreditCardPaymentService(ITransactionRepository transactionRepo,
            IGenericRepository<Payment> paymentRepo)
        {
            _transactionRepo = transactionRepo;
            _paymentRepo = paymentRepo;
        }

        public async Task<bool> ProcessPayment(Payment model)
        {
            model.Id = Guid.NewGuid().ToString();
            await _paymentRepo.Add(model);
            return await AddTransaction(model.Id);
        }

        public async Task<bool> AddTransaction(string id)
        {
            var transaction = new Transactions
            {
                Id = Guid.NewGuid().ToString(),
                PaymentId = id
            };
            return await _transactionRepo.Add(transaction);
        }
    }
}
