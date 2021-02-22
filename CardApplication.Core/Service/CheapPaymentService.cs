using CardApplication.Core.Interface;
using CardApplication.Model;
using System;
using System.Threading.Tasks;

namespace CardApplication.Core.Service
{
    public class CheapPaymentService : CreditCardPaymentService, ICheapPaymentGateway
    {
        public CheapPaymentService(ITransactionRepository transactionRepo, IGenericRepository<Payment> paymentRepo)
            : base(transactionRepo, paymentRepo) {}
    }
}
