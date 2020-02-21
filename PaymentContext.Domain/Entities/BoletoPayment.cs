﻿using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Domain.Entities
{
    public class BoletoPayment : Payment  
    {
        public BoletoPayment(string barCode, 
                             string boletoNumber, 
                             DateTime paidDate, 
                             DateTime expireDate, 
                             decimal total, 
                             decimal totalPaid, 
                             Address address, 
                             Document document, 
                             string owner, 
                             Email email):base(
                                 paidDate,  
                                 expireDate,  
                                 total,  
                                 totalPaid,  
                                 address,  
                                 document,  
                                 owner,  
                                 email)
        {
            BarCode = barCode;
            BoletoNumber = boletoNumber;
        }

        public string BarCode { get; private set; }
        public string BoletoNumber { get; private set; }
    }
}
