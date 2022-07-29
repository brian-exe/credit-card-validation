using CreditCardValidation.Abstractions.Repositories;
using CreditCardValidation.Abstractions.Services;
using CreditCardValidation.Domain;
using CreditCardValidation.Models;
using CreditCardValidation.Models.Constants;
using CreditCardValidation.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CreditCardValidation.Services
{
    public class CreditCardValidationService : ICreditCardValidationService
    {
        private readonly ICardTypeRepository _repository;

        /// <summary>
        /// Detect type
        ///     if not detected error unrecognized type
        ///     if detected
        ///         check number length
        ///             error
        ///         check cvv length
        ///             error
        ///         check owner data
        ///             error
        ///         check expiration date
        ///             error
        /// </summary>

        public CreditCardValidationService(ICardTypeRepository repository)
        {
            _repository = repository;
        }
        public GenericApplicationResponse<CreditCardValidationResponse> ValidateCreditCard(CreditCardValidationRequest model)
        {
            var response = new GenericApplicationResponse<CreditCardValidationResponse>();
            CardType detectedType = DetectTypeFor(model);

            if(detectedType == null)
            {
                response.Errors.Add(new ApplicationError() { Code = ErrorCodes.UnrecognizedCardType, Message = "Not recognized type for card", Type = ErrorTypes.ValidationError });
                response.Success = false;
                return response;
            }

            if (!AllDataProvided(model))
            {
                response.Errors.Add(new ApplicationError() { Code = ErrorCodes.MissingFields, Message = "There are missing fields", Type = ErrorTypes.ValidationError });
                response.Success = false;
                return response;
            }

            if (!IsValidNumber(model))
                response.Errors.Add(new ApplicationError() { Code = ErrorCodes.InvalidCardNumber, Message = "Invalid card number", Type = ErrorTypes.ValidationError });

            if (!IsValidNumberLength(detectedType, model))
                response.Errors.Add(new ApplicationError() { Code = ErrorCodes.InvalidCardNumberLength, Message = "Invalid length for card number", Type = ErrorTypes.ValidationError });

            if (!IsValidCVVLength(detectedType, model))
                response.Errors.Add(new ApplicationError() { Code = ErrorCodes.InvalidCardCVVLength, Message = "Invalid CVV length", Type = ErrorTypes.ValidationError });

            if (!IsValidOwnerData(model))
                response.Errors.Add(new ApplicationError() { Code = ErrorCodes.InvalidOwnerData, Message = "Invalid owner data", Type = ErrorTypes.ValidationError });

            if (!IsCardExpirationValid(model))
                response.Errors.Add(new ApplicationError() { Code = ErrorCodes.ExpiredCard, Message = "Expired card", Type = ErrorTypes.ValidationError });


            response.Success = !response.Errors.Any();

            response.Data = response.Success ? new CreditCardValidationResponse() { CreditCardType = detectedType.Name } : null;

            return response;
        }

        private bool AllDataProvided(CreditCardValidationRequest model)
            => model.CVV != 0 && model.Expiration != null && model.Number != 0 && model.Owner != null;

        private bool IsCardExpirationValid(CreditCardValidationRequest model)
        {
            var daysInMonth = DateTime.DaysInMonth(model.Expiration.Year, model.Expiration.Month);
            var expirationDate = new DateTime(model.Expiration.Year, model.Expiration.Month, daysInMonth);
            return DateTime.Now.Date <= expirationDate;
        }

        private bool IsValidOwnerData(CreditCardValidationRequest model)
            => model.Owner != null && model.Owner.Contains(" ") && !model.Owner.Any(char.IsDigit);

        private bool IsValidCVVLength(CardType detectedType, CreditCardValidationRequest model)
        {
            foreach(var r in detectedType.CVVLengthRules)
            {
                if (model.CVV.ToString().Length == r.Length)
                    return true;
            }
            return false;
        }

        private bool IsValidNumberLength(CardType detectedType, CreditCardValidationRequest model)
        {
            foreach (var r in detectedType.LengthRules)
            {
                if (model.Number.ToString().Length == r.Length)
                    return true;
            }
            return false;
        }

        private bool IsValidNumber(CreditCardValidationRequest model)
            => LuhnValidator.IsValidCardNumber(model.Number.ToString());

        private CardType DetectTypeFor(CreditCardValidationRequest model)
        {
            var validTypes = _repository.GetSupportedCardTypes();
            CardType detectedType = null;
            foreach(var t in validTypes)
            {
                if (t.PrefixRules.Any(p => model.Number.ToString().StartsWith(p.Prefix.ToString())))
                {
                    detectedType = t;
                    break; // cards will always match only one type. no need to keep looking for
                }
            }
            return detectedType;
        }
    }
}
