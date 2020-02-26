using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Domain.Entities
{
    public class PayPalPayment : Payment 
    {
        public PayPalPayment(string transactionCode, 
                             DateTime paidDate, 
                             DateTime expireDate, 
                             decimal total, 
                             decimal totalPaid,
                             string payer,
                             Address address, 
                             Document document, 
                             Email email): base(
                                 paidDate, 
                                 expireDate, 
                                 total, 
                                 totalPaid, 
                                 address, 
                                 document, 
                                 email)
        {
            TransactionCode = transactionCode;
        }

        public string TransactionCode { get; private set; }        

    }
}
