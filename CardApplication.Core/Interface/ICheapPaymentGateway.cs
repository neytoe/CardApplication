﻿using CardApplication.Model;
using System.Threading.Tasks;

namespace CardApplication.Core.Interface
{
    public interface ICheapPaymentGateway: ICreditCardPayment
    {
       // Task<bool> ProcessPayment(Payment model);
    }
}
