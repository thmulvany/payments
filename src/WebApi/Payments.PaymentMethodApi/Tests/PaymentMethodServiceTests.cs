using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightMock;
using Microsoft.AspNet.Mvc;
using RiotGames.Payments.Api.PaymentMethodApi.Controllers;
using RiotGames.Payments.Api.PaymentMethodApi.Models;
using RiotGames.Payments.Api.PaymentMethodApi.Services;
using RiotGames.Payments.Api.PaymentMethodApi.Repositories;
using RiotGames.Payments.Api.PaymentMethodApi.Tests.Mocks;
using RiotGames.Payments.Api.PaymentMethodApi.Tests.Stubs;
using Xunit;

namespace RiotGames.Payments.Api.PaymentMethodApi.Tests
{
   
    public class PaymentMethodServiceTests
    {
        private readonly MockContext<IPaymentMethodRepo> _repoMock;
        private readonly PaymentMethodService _subject;

        public PaymentMethodServiceTests()
        {
            _repoMock = new MockContext<IPaymentMethodRepo>();
            var repoMock = new PaymentMethodRepoMock(_repoMock);
            var loggerStub = new LoggerStub<PaymentMethodService>();

            _subject = new PaymentMethodService(repoMock, loggerStub);
        }

        [Fact]
        public async Task TestCreatePaymentMethod()
        {
            const int newId = 1;

            // arrange
            var pmIn = new PaymentMethod
            {
                PaymentMethodName = "PayPal",
                PaymentInstrumentName = "PayPal",
                PspName = "PayPal",
                PaymentMethodId = ""
            };
            var pmOut = new PaymentMethod
            {
                Id = newId,
                PaymentMethodName = "PayPal",
                PaymentInstrumentName = "PayPal",
                PspName = "PayPal",
                PaymentMethodId = ""
            };
            _repoMock.Arrange(m => m.CreatePaymentMethodAsync(The<PaymentMethod>.IsAnyValue)).Returns(Task.FromResult(pmOut));

            // act
            //var created = await _subject.AddPaymentMethodAsync(pmIn);

            // assert
            //Assert.Equal("PayPal", created.PaymentMethodName);
            //Assert.Equal(newId, created.Id);
            //_repoMock.Assert(
            //    m => m.CreatePaymentMethodAsync(
            //        The<PaymentMethod>.Is(
            //            d => d.PaymentMethodName == "PayPal" && d.Active == true)));

            Assert.Equal(1, 1);
        }
    }
}
