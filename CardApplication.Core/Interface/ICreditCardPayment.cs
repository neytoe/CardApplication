using CardApplication.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CardApplication.Core.Interface
{
    public interface ICreditCardPayment
    {
        Task<bool> ProcessPayment(Payment model);

        Task<bool> AddTransaction(string id);
    }
}
