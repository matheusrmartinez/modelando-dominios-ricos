using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaymentContext.Tests.ValueObjects
{
    [TestClass]
    public class DocumentTests
    {
        // Red, Green, Refactor
        [TestMethod]
        public void ShouldReturnErrorWhenCNPJIsInvalid()
        {
            var document = new Document("123456789", EDocumentType.CNPJ);
            Assert.IsTrue(document.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCNPJIsValid()
        {
            var document = new Document("12345678987946", EDocumentType.CNPJ);
            Assert.IsTrue(document.Valid);
        }

        [TestMethod]
        public void ShouldReturnErrorWhenCPFIsInvalid()
        {
            var document = new Document("123456789456", EDocumentType.CPF);
            Assert.IsTrue(document.Invalid);
        }

        [TestMethod]
        public void ShouldReturnSuccessWhenCPFIsValid()
        {
            var document = new Document("12345678913", EDocumentType.CPF);
            Assert.IsTrue(document.Valid);
        }

        // Método para testar diversos CPF's
        //[TestMethod]
        //[DataTestMethod]
        //[DataRow("76663732951")]
        //[DataRow("80226663434")]
        //[DataRow("03677180147")]
        //[DataRow("96640277743")]
        //[DataRow("61342375823")]
        //public void ShouldReturnSuccessWhenCPFAreValid(string CPF)
        //{
        //    var document = new Document(CPF, EDocumentType.CPF);
        //    Assert.IsTrue(document.Valid);
        //}
    }
}
