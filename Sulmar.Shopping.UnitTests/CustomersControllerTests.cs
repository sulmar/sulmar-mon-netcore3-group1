using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Sulmar.Shopping.API.Controllers;
using Sulmar.Shopping.Domain;
using Sulmar.Shopping.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sulmar.Shopping.UnitTests
{
    public class CustomersControllerTests
    {
        private CustomersControllerAsync customersController;
        private Mock<ICustomerRepositoryAsync> mockCustomerRepository;

        [SetUp]
        public void SetUp()
        {
            mockCustomerRepository = new Mock<ICustomerRepositoryAsync>();
            customersController = new CustomersControllerAsync(mockCustomerRepository.Object);
        }

        [Test]
        public async Task GetById_IdIsZero_ReturnsNotFound()
        {
            // Arrange
            mockCustomerRepository
                .Setup(m => m.GetAsync(0))
                .ReturnsAsync((Customer)null);
           
            // Act
            var result = await customersController.GetById(0);

            // Assert
            Assert.That(result, Is.TypeOf<NotFoundResult>());
        }

        [Test]
        public async Task GetById_ExistsId_ReturnsOk()
        {
            // Arrange
            mockCustomerRepository
                .Setup(m => m.GetAsync(1))
                .ReturnsAsync(new Customer());

            // Act
            var result = await customersController.GetById(1);

            // Assert
            Assert.That(result, Is.TypeOf<OkObjectResult>());
        }
    }
}
