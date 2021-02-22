using CardApplication.Core.Interface;
using CardApplication.Model;
using System;
using System.Threading.Tasks;

namespace CardApplication.Core.Service
{
    public class PremiumPaymentService : CreditCardPaymentService, IPremiumPaymentGateway
    {
        public PremiumPaymentService(ITransactionRepository transactionRepo, IGenericRepository<Payment> paymentRepo) 
            : base(transactionRepo, paymentRepo) {} 
    }
}
