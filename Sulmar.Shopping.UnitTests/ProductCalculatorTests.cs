using NUnit.Framework;
using Sulmar.Shopping.Domain;
using Sulmar.Shopping.Domain.Services;
using System;

namespace Sulmar.Shopping.UnitTests
{
    public class ProductCalculatorTests
    {
        private ProductCalculatorService productCalculatorService;

        [SetUp]
        public void Setup()
        {
            productCalculatorService = new ProductCalculatorService();
        }

        [Test]
        [Ignore("no bo tak")]
        public void Calculate_FromWarszawa_ResultsTwiceUnitPrice()
        {
            // Arrange
            Customer customer = new Customer { City = "Warszawa" };

            // Act
            var result = productCalculatorService.Calculate(customer, 100);

            // Assert
            Assert.AreEqual(200, result);

        }

        [Test]
        [Ignore("no bo tak")]
        public void Calculate_OtherCity_ResultsStandardUnitPrice()
        {
            // Arrange
            Customer customer = new Customer { City = "Bydgoszcz" };

            // Act
            var result = productCalculatorService.Calculate(customer, 100);

            // Assert
            Assert.AreEqual(100, result);
        }



        [Test]
        [Ignore("no bo tak")]
        public void Calculate_FromPoznan_ResultsZeroUnitPrice()
        {
            // Arrange
            Customer customer = new Customer { City = "Poznañ" };

            // Act
            var result = productCalculatorService.Calculate(customer, 100);

            // Assert
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Calculate_EmptyCustomer_ThrowArgumentNullException()
        {
            // Arrange
            Customer customer = null;

            // Act
            TestDelegate act = () => productCalculatorService.Calculate(customer, 0);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }

        [Test]
        public void Calculate_ZeroUnitPrice_ThrowsOutOfRangeException()
        {
            // Arrange
            Customer customer = new Customer();

            // Act
            TestDelegate act = () => productCalculatorService.Calculate(customer, 0);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(act);
        }



        [Test]
        [TestCase("Warszawa", 100, 200)]
        [TestCase("Poznañ", 100, 0)]
        [TestCase("Bydgoszcz", 100, 100)]
        public void Calculate_WhenCalled_ResultsUnitPrice(string city, decimal unitPrice, decimal expectedUnitPrice)
        {
            // Arrange
            Customer customer = new Customer { City = city };

            // Act
            var result = productCalculatorService.Calculate(customer, unitPrice);

            // Assert
            Assert.AreEqual(expectedUnitPrice, result);
        }
    }
}