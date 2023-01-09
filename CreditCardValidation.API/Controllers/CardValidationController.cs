using CreditCardValidation.Abstractions.Services;
using CreditCardValidation.Infrastructure.Logger;
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
        private readonly ILoggerProxy<CardValidationController> _logger;

        public CardValidationController(ICreditCardValidationService cardValidationService, ILoggerProxyFactory proxyFactory)
        {
            _cardValidationService = cardValidationService;
            _logger = proxyFactory.Create<CardValidationController>();
        }

        [HttpPost("credit")]
        public ActionResult<GenericApplicationResponse<CreditCardValidationResponse>> ValidateCreditCard(CreditCardValidationRequest req)
        {
            _logger.LogError("Credit card logging test 4123778912341234");
            _logger.LogError("The Iban is {0}", "EE10 1234 1234 1234 1234 11");
            _logger.LogError("Request received. Details of the request: {0}", req);
            var response = _cardValidationService.ValidateCreditCard(req);

            if (!response.Success && response.Errors.Any())
                return BadRequest(response);

            return Ok(response);
        }
    }
}
