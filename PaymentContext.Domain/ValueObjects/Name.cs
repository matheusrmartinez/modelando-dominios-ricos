using Flunt.Validations;
using PaymentContext.Shared.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Domain.ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;

            AddNotifications(new Contract()
                    .Requires()
                    .HasMinLen(FirstName, 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres")
                    .HasMinLen(lastName, 3, "Name.LastName", "Sobrenome deve conter pelo menos 3 caracteres")
                    .HasMaxLen(FirstName, 40, "Name.FirstName", "Nome deve ter no máximo 40 caracteres"));

            if (string.IsNullOrEmpty(FirstName))
                AddNotification("Name.FirstName", "Nome inválido");

            if (string.IsNullOrEmpty(LastName))
                AddNotification("Name.LastName", "Sobrenome inválido");

        }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
    }
}
