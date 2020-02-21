using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using System;

namespace PaymentContext.Domain.Entities
{
    public abstract class Payment : Notifiable
    {
        protected Payment(DateTime paidDate, DateTime expireDate, decimal total, decimal totalPaid, Address address, Document document, string owner, Email email)
        {
            Number = Guid.NewGuid().ToString().Replace("-","").Substring(0,10).ToUpper();
            PaidDate = paidDate;
            ExpireDate = expireDate;
            Total = total;
            TotalPaid = totalPaid;
            Address = address;
            Document = document;
            Owner = owner;
            Email = email;

            AddNotifications(new Contract()
                    .Requires()
                    .IsGreaterThan(0, Total, "Payment.DocTotal", "O total n�o pode ser zero")
                    .IsGreaterOrEqualsThan(0, TotalPaid, "Payment.TotalPaid", "O valor pago � menor do que o valor do pagamento")
                    );
        }

        public string Number { get; private set; }
        public DateTime PaidDate { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public decimal Total { get; private set; }
        public decimal TotalPaid { get; private set; }
        public Address Address { get; private set; }
        public Document Document { get; private set; }
        public string Owner { get; private set; }
        public Email Email { get; private set; }
    }
}