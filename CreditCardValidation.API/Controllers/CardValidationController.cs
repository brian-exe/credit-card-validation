using CreditCardValidation.Abstractions.Services;
using CreditCardValidation.API.Logger;
using CreditCardValidation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace CreditCardValidation.API.Controllers
{
    [Route("api/card-validation")]
    [ApiController]
    public class CardValidationController : ControllerBase
    {
        private readonly ICreditCardValidationService _cardValidationService;
        private readonly ILoggerProxy<CardValidationController> loggerProxy;
        public CardValidationController(ICreditCardValidationService cardValidationService, ILoggerProxyFactory loggerFactory)
        {
            _cardValidationService = cardValidationService;
            loggerProxy = loggerFactory.Create<CardValidationController>();
        }

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
