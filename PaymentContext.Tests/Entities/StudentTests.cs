using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using PaymentoContext.Domain.Entites;
using System;

namespace PaymentContext.Tests.Entities
{
    [TestClass]
    public class StudentTests
    {
        private readonly Subscription _subscription;
        private readonly Student _student;
        private readonly Name _name;
        private readonly Document _document;
        private readonly Address _address;
        private readonly Email _email;
        public StudentTests()
        {
            _document = new Document("40121050911", EDocumentType.CPF);
            _address = new Address("Rua João", "132", "Trujillo", "Sorocaba", "São Paulo", "Brasil", "18016200");
            _email = new Email("matheus.rmartinez@gmail.com");
            _name = new Name("Matheus", "Martinez");
            _student = new Student(_name, _document, _email);
            _subscription = new Subscription(null);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenHadActiveSubscription()
        {
            var payment = new PayPalPayment("0123479", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne Corp", _address, _document, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenSubscriptionHasNoPayment()
        {
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenAddSubscription()
        {
            var payment = new PayPalPayment("0123479", DateTime.Now, DateTime.Now.AddDays(5), 10, 10, "Wayne Corp", _address, _document, _email);
            _subscription.AddPayment(payment);
            _student.AddSubscription(_subscription);
            Assert.IsTrue(_student.Valid);

        }
    }
}
