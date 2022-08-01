using CreditCardValidation.Abstractions.Services;
using CreditCardValidation.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

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
