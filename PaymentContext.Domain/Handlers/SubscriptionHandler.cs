using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;
using PaymentoContext.Domain.Entites;
using System;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable,
                                       IHandler<CreateBoletoSubscriptionCommand>,
                                       IHandler<CreatePayPalSubscriptionCommand>,
                                       IHandler<CreateCreditCardSubscriptionCommand>

    {

        private readonly IStudentRepository _studentRepository;
        private readonly IEmailService _emailServices;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _studentRepository = studentRepository;
            _emailServices = emailService;
        }
        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validations
            command.Validate();
            if (command.Invalid)
            {
                AddNotifications(command);
                return new CommandResult(false, "Não foi possível completar sua assinatura.");
            }

            // Verificar se Documento já está cadastrado
            if (_studentRepository.DocumentExists(command.PayerDocument))
                AddNotification("Document", "Este CPF já está cadastrado");

            // Verificar se Email já está cadastrado
            if (_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este Email já está cadastrado");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.PayerDocument, EDocumentType.CPF);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            var email = new Email(command.Email);


            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new BoletoPayment(command.BarCode,
                                            command.BoletoNumber,
                                            command.PaidDate,
                                            command.ExpireDate,
                                            command.Total,
                                            command.TotalPaid,
                                            address,
                                            document,
                                            email
                                            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar as Informações
            _studentRepository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailServices.SendEmail(student.Name.ToString(), student.Email.Address, "Bem-vindo ao balta.io", "Sua assinatura foi criada");

            // Retornar informações

            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand command)
        {
            // Verificar se Documento já está cadastrado
            if (_studentRepository.DocumentExists(command.PayerDocument))
                AddNotification("Document", "Este CPF já está cadastrado");

            // Verificar se Email já está cadastrado
            if (_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este Email já está cadastrado");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.PayerDocument, EDocumentType.CPF);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            var email = new Email(command.Email);


            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new PayPalPayment(command.TransactionCode,
                                            command.PaidDate,
                                            command.ExpireDate,
                                            command.Total,
                                            command.TotalPaid,
                                            command.FirstName,
                                            address,
                                            document,
                                            email
                                            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar as Informações
            _studentRepository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailServices.SendEmail(student.Name.ToString(), student.Email.Address, "Bem-vindo ao balta.io", "Sua assinatura foi criada");

            // Retornar informações

            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        public ICommandResult Handle(CreateCreditCardSubscriptionCommand command)
        {
                        // Verificar se Documento já está cadastrado
            if (_studentRepository.DocumentExists(command.PayerDocument))
                AddNotification("Document", "Este CPF já está cadastrado");

            // Verificar se Email já está cadastrado
            if (_studentRepository.EmailExists(command.Email))
                AddNotification("Email", "Este Email já está cadastrado");

            // Gerar os VOs
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.PayerDocument, EDocumentType.CPF);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);
            var email = new Email(command.Email);


            // Gerar as Entidades
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));
            var payment = new CreditCardPayment(
                                            command.CardHolderName,
                                            command.CardNumber,
                                            command.LastTransactionNumber,
                                            command.PaidDate,
                                            command.ExpireDate,
                                            command.Total,
                                            command.TotalPaid,
                                            address,
                                            document,
                                            email
                                            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar as Informações
            _studentRepository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailServices.SendEmail(student.Name.ToString(), student.Email.Address, "Bem-vindo ao balta.io", "Sua assinatura foi criada");

            // Retornar informações

            return new CommandResult(true, "Assinatura realizada com sucesso");
        }
    }
}
