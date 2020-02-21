using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        protected Entity()
        {
            Notifications = new List<string>();
            Id = Guid.NewGuid();
        }

        private IList<string> Notifications;
        public Guid Id { get; private set; }
    }
}
