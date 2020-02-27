using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Handlers;
using PaymentContext.Domain.Queries;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Tests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PaymentContext.Tests.Queries
{
    [TestClass]
    public class StudentQueriesTests
    {
        private IList<Student> _students = new List<Student>();
        public void AddStudents()
        {
            _students.Add(new Student(new Name("João", "da Silva"), new Document("123456787412", EDocumentType.CPF), new Email("joaodasilva@gmail.com")));
            _students.Add(new Student(new Name("Maria", "da Silva"), new Document("12345123456", EDocumentType.CPF), new Email("mariadasilva@gmail.com")));
            _students.Add(new Student(new Name("Teobaldo", "da Silva"), new Document("00000000000", EDocumentType.CPF), new Email("teobaldodasilva@gmail.com")));
        }

        [TestMethod]
        public void ShouldReturnErrorNullWhenDocumentNotExists()
        {
            AddStudents();
            var exp = StudentQueries.GetStudentByDocument("12345678911");
            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreEqual(null, student);
        }

        [TestMethod]
        public void ShouldReturnStudentWhenDocumentExists()
        {
            AddStudents();
            var exp = StudentQueries.GetStudentByDocument("12345123456");
            var student = _students.AsQueryable().Where(exp).FirstOrDefault();

            Assert.AreEqual(student, student);
        }
    }
}
