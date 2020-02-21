using Flunt.Validations;
using PaymentContext.Domain.Enums;
using PaymentContext.Shared.ValueObject;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public Document(string number, EDocumentType type)
        {
            Number = number;    
            Type = type;

            AddNotifications(new Contract()
                .Requires()
                .IsTrue(ValidateDocument(), "Document.Number", "Número de documento inválido"
                ));
          }
        public string Number { get; private set; }
        public  EDocumentType Type { get; private set; }

        public bool ValidateDocument()
        {
            if (Type == EDocumentType.CNPJ && Number.Length == 14)
            return true;           
            
            if (Type == EDocumentType.CPF && Number.Length == 11)
            return true;           
            
            else
            return false;
        }
    }
}
