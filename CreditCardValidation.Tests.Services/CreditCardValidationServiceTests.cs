using CreditCardValidation.Abstractions.Services;
using CreditCardValidation.Models;
using CreditCardValidation.Models.Constants;
using CreditCardValidation.Services;
using System;
using System.Linq;
using Xunit;

namespace CreditCardValidation.Tests.Services
{
    public class CreditCardValidationServiceTests
    {
        private ICreditCardValidationService _service;
        public CreditCardValidationServiceTests()
        {
            _service = new CreditCardValidationService();
        }
        /// <summary>
        /// campos faltantes ==> error
        /// campos completos pero numero invalido, no se encontr� tipo v�lido
        /// tipo reconocido pero invalido por luhn
        /// tipo reconocido pero largo inv�lido
        /// tipo reconocido pero cvv invalido
        /// tipo reconocido pero owner inv�lido
        /// tipo reconocido pero tarjeta expirada
        /// </summary>
        [Fact]
        public void ValidateCreditCard_WhenCalledWithNotAllFieldsProvided_ReturnsMissingFieldsError()
        {
            var model = new CreditCardValidationRequest()
            {
                Number = 0123433534534534
            };
            var result = _service.ValidateCreditCard(model);

            Assert.NotEmpty(result.Errors);
            Assert.Contains(result.Errors, b => b.Type == ErrorTypes.ValidationError && b.Code == ErrorCodes.MissingFields);
        }

        [Fact]
        public void ValidateCreditCard_WhenCalledNoIdentifiableNumber_ReturnsUnrecognizedTypeError()
        {
            var model = new CreditCardValidationRequest()
            {
                CVC = 4343,
                Number = 0123433534534534,
                Expiration = new CardExpirationModel() { Month = 02, Year = 2026},
                Owner = "owner"
            };
            var result = _service.ValidateCreditCard(model);

            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, b => b.Type == ErrorTypes.ValidationError && b.Code == ErrorCodes.UnrecognizedCardType);
        }

        [Fact]
        public void ValidateCreditCard_WhenCalledWithRecognizedTypeButInvalidNumber_ReturnsInvalidCardNumberError()
        {
            var model = new CreditCardValidationRequest()
            {
                CVC = 434,
                Number = 4111111111111111, //Visa (starts with 4)
                Expiration = new CardExpirationModel() { Month = 02, Year = 2026 },
                Owner = "Jon Doe"
            };
            var result = _service.ValidateCreditCard(model);

            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, b => b.Type == ErrorTypes.ValidationError && b.Code == ErrorCodes.InvalidCardNumber);
        }
        
        [Fact]
        public void ValidateCreditCard_WhenCalledWithRecognizedTypeButInvalidNumberLength_ReturnsInvalidCardNumberLengthError()
        {
            var model = new CreditCardValidationRequest()
            {
                CVC = 434,
                Number = 4111111111111111111, //Visa (starts with 4)
                Expiration = new CardExpirationModel() { Month = 02, Year = 2026 },
                Owner = "Jon doe"
            };
            var result = _service.ValidateCreditCard(model);

            Assert.NotEmpty(result.Errors);
            Assert.Contains(result.Errors, b => b.Type == ErrorTypes.ValidationError && b.Code == ErrorCodes.InvalidCardNumberLength);
        }

        [Fact]
        public void ValidateCreditCard_WhenCalledWithInvalidCVVLengthForType_ReturnsInvalidCVVLengthError()
        {
            var model = new CreditCardValidationRequest()
            {
                CVC = 43453,
                Number = 4765914316339760,
                Expiration = new CardExpirationModel() { Month = 02, Year = 2026 },
                Owner = "Jon doe"
            };
            var result = _service.ValidateCreditCard(model);

            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, b => b.Type == ErrorTypes.ValidationError && b.Code == ErrorCodes.InvalidCardCVVLength);
        }

        [Fact]
        public void ValidateCreditCard_WhenCalledWithInvalidDataForOwner_ReturnsInvalidOwnerDataError()
        {
            var model = new CreditCardValidationRequest()
            {
                CVC = 434,
                Number = 4765914316339760,
                Expiration = new CardExpirationModel() { Month = 02, Year = 2026 },
                Owner = "owner"
            };
            var result = _service.ValidateCreditCard(model);

            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, b => b.Type == ErrorTypes.ValidationError && b.Code == ErrorCodes.InvalidOwnerData);
        }

        [Fact]
        public void ValidateCreditCard_WhenCalledWithExpiredDate_ReturnsExpiredDateError()
        {
            var model = new CreditCardValidationRequest()
            {
                CVC = 434,
                Number = 4765914316339760,
                Expiration = new CardExpirationModel() { Month = 02, Year = 2020 },
                Owner = "Jon Doe"
            };
            var result = _service.ValidateCreditCard(model);

            Assert.NotEmpty(result.Errors);
            Assert.Single(result.Errors);
            Assert.Contains(result.Errors, b => b.Type == ErrorTypes.ValidationError && b.Code == ErrorCodes.ExpiredCard);
        }

        [Fact]
        public void ValidateCreditCard_WhenCalledWithAllInvalidFields_ReturnsAllErrors()
        {
            var model = new CreditCardValidationRequest()
            {
                CVC = 4345,
                Number = 4111111111111111,
                Expiration = new CardExpirationModel() { Month = 02, Year = 2020 },
                Owner = "Jon"
            };
            var result = _service.ValidateCreditCard(model);

            Assert.NotEmpty(result.Errors);
            Assert.Contains(result.Errors, b => b.Type == ErrorTypes.ValidationError && b.Code == ErrorCodes.InvalidCardCVVLength);
            Assert.Contains(result.Errors, b => b.Type == ErrorTypes.ValidationError && b.Code == ErrorCodes.InvalidCardNumber);
            Assert.Contains(result.Errors, b => b.Type == ErrorTypes.ValidationError && b.Code == ErrorCodes.ExpiredCard);
            Assert.Contains(result.Errors, b => b.Type == ErrorTypes.ValidationError && b.Code == ErrorCodes.InvalidOwnerData);
        }

        [Fact]
        public void ValidateCreditCard_WhenCalledWithValidFieldsForVisa_ReturnsVisaAsType()
        {
            var model = new CreditCardValidationRequest()
            {
                CVC = 434,
                Number = 4765914316339760,
                Expiration = new CardExpirationModel() { Month = 02, Year = 2026},
                Owner = "Jon Doe"
            };
            var result = _service.ValidateCreditCard(model);

            Assert.Empty(result.Errors);
            Assert.Equal("Visa", result.Data.CreditCardType);
        }

        [Fact]
        public void ValidateCreditCard_WhenCalledWithValidFieldsForMasterCard_ReturnsMastercardAsType()
        {
            var model = new CreditCardValidationRequest()
            {
                CVC = 111,
                Number = 5239205771213904,
                Expiration = new CardExpirationModel() { Month = 02, Year = 2026 },
                Owner = "Jon Doe"
            };
            var result = _service.ValidateCreditCard(model);

            Assert.Empty(result.Errors);
            Assert.Equal("Mastercard", result.Data.CreditCardType);
        }
    }
}
