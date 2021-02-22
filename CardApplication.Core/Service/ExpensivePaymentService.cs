using CardApplication.Core.Interface;
using CardApplication.Model;
using System;
using System.Threading.Tasks;

namespace CardApplication.Core.Service
{
    public class ExpensivePaymentService : CreditCardPaymentService, IExpensivePaymentGateway
    {
        public ExpensivePaymentService(ITransactionRepository transactionRepo, IGenericRepository<Payment> paymentRepo) 
            : base(transactionRepo, paymentRepo) {} 
    }
}
