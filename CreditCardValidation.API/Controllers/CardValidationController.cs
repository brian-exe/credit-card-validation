using CreditCardValidation.Abstractions.Services;
using CreditCardValidation.Models;
using CreditCardValidation.Models.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CreditCardValidation.API.Controllers
{
    [Route("api/card-validation")]
    [ApiController]
    public class CardValidationController : ControllerBase
    {
        private readonly ICreditCardValidationService _cardValidationService;

        public CardValidationController(ICreditCardValidationService cardValidationService)
            => _cardValidationService = cardValidationService;


        [HttpPost("credit")]
        public ActionResult<GenericApplicationResponse<CreditCardValidationResponse>> ValidateCreditCard(CreditCardValidationRequest req)
        {
            var response = _cardValidationService.ValidateCreditCard(req);

            if (!response.Success && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }
    }
}
