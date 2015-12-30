using FluentAssertions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightMock;
using Microsoft.AspNet.Mvc;
using RiotGames.Payments.Api.PaymentMethodApi.Controllers;
using RiotGames.Payments.Api.PaymentMethodApi.Models;
using RiotGames.Payments.Api.PaymentMethodApi.Repositories;
using RiotGames.Payments.Api.PaymentMethodApi.Services;
using RiotGames.Payments.Api.PaymentMethodApi.Tests.Mocks;
using RiotGames.Payments.Api.PaymentMethodApi.Tests.Stubs;
using Xunit;

namespace RiotGames.Payments.Api.PaymentMethodApi.Tests
{
    public class PaymentMethodsControllerTests
    {
        private readonly MockContext<IPaymentMethodService> _serviceMock;
        private readonly PaymentMethodsController _subject;

        public PaymentMethodsControllerTests()
        {
            _serviceMock = new MockContext<IPaymentMethodService>();
            var repositoryMock = new PaymentMethodServiceMock(_serviceMock);
            var loggerStub = new LoggerStub<PaymentMethodsController>();

            _subject = new PaymentMethodsController(repositoryMock, loggerStub);
        }

        [Fact]
        public async Task TestCreatePaymentMethod() 
        {   
            // arrange
            _subject.ModelState.AddModelError("RequiredPaymentMethodName", "dummy error");

            // act
            var actionResult = await _subject.Create(new PaymentMethod());

            // assert
            actionResult.Should().BeOfType<BadRequestResult>();
        }
    }
}
