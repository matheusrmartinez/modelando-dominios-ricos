using System.Collections.Generic;
using System.Linq;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;
using PaymentoContext.Domain.Entites;

namespace PaymentContext.Domain.Entities
{
    public class Student : Entity
    {
        private IList<Subscription> _subscriptions;

        public Student(Name name, Document document, Email email)
        {
            Name = name;
            Document = document;
            Email = email;
            _subscriptions = new List<Subscription>();

            if (string.IsNullOrEmpty(Name.FirstName))
            {
                AddNotification("Name.FirstName", "Nome inv�lido");
            }

            AddNotifications(name, document, email);
        }

        public Name Name { get; private set; }
        public Document Document { get; private set; }
        public Email Email { get; private set; }
        public Address Address { get; private set; } 
        public IReadOnlyCollection<Subscription> Subscriptions { get { return _subscriptions.ToArray(); } }

        public void AddSubscription(Subscription subscription)
        {
            var hasSubscriptionActive = false;
            foreach (var sub in Subscriptions)
            {
                if (sub.Active)
                {
                    hasSubscriptionActive = true;
                }
            }

            //AddNotifications(new Contract()
            //    .Requires()
            //    .IsFalse(hasSubscriptionActive, "Student.Subscriptions", "Voc� j� tem uma assinatura ativa"));


            // Alternativa
            if (hasSubscriptionActive)
                AddNotification("Student.Subscriptions", "Voc� j� tem uma assinatura ativa");
        }

    }
}