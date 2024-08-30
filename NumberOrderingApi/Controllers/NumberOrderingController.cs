using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using NumberOrderingApi.Models;
using NumberOrderingApi.Services;

namespace NumberOrderingApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumberOrderingController : ControllerBase
    {
        private readonly INumberOrderingService _numberOrderingService;
        public NumberOrderingController(INumberOrderingService numberOrderingService)
        {
            _numberOrderingService = numberOrderingService;
        }

        [HttpPost]
        [Route("OrderNumbers")]
        public async Task<IActionResult> OrderNumbers([FromBody] AddNumberOrderingRequest request)
        {
            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(ModelState);
            }

            try
            {
                var validationResult = await _numberOrderingService.SortAndSaveNumbers(request.Numbers);

                if (validationResult != ValidationResult.Success)
                {
                    return UnprocessableEntity(validationResult.ErrorMessage);
                }
                
                return Ok("Numbers sorted and saved successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"A server error occurred while processing your request. {ex.Message}");
            }
        }

        [HttpGet]
        [Route("LoadContentOfLatestSavedFile")]
        public async Task<IActionResult> LoadContentOfLatestSavedFile()
        {
            try
            {
                var numbers = await _numberOrderingService.LoadContentOfLatestSavedFile();

                if (numbers == string.Empty)
                {
                    return NotFound("No records found.");
                }

                return Ok(numbers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"A server error occurred while processing your request. {ex.Message}");
            }
            
        }
    }
}
