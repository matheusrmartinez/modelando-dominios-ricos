using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Tests.Handlers
{
    [TestClass]
    public class SubscriptionHandlerTests
    {

        [TestMethod]
        public void ShouldReturnErrorWhenDocumentsExists()
        {
            var handler = new SubscriptionHandler(new FakeStudentRepository(), new FakeEmailService());
            var command = new CreateBoletoSubscriptionCommand();
            command.FirstName = "Matheus";
            command.LastName = "Martinez";
            command.PayerDocument = "99999999999999";
            command.Email = "matheus.rmartinez@gmail.com";
            command.BarCode = "12345678945612";
            command.BoletoNumber = "1545749878923213245649846513210000120";
            command.PaymentNumber = "120456";
            command.PaidDate = DateTime.Now;
            command.ExpireDate = DateTime.Now.AddMonths(1);
            command.Total = 20;
            command.TotalPaid = 20;
            command.Payer = "Eu";
            command.PayerDocumentType = EDocumentType.CPF;
            command.PayerEmail = "matheus.rmartinez@gmail.com";
            command.Street = "Rua João do Brasil";
            command.Number = "1150";
            command.Neighborhood = "Jardim Brasil";
            command.City = "Sorocaba";
            command.State = "São Paulo";
            command.Country = "Brasil";
            command.ZipCode = "18016450";
            handler.Handle(command);
            Assert.AreEqual(false, handler.Valid);
        }
    }
}
