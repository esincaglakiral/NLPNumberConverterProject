using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLPNumberConverter.BusinessLayer.Services;
using NLPNumberConverter.CoreLayer.Entities;

namespace NLPNumberConverter.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TextConverterController : ControllerBase
    {
        private readonly ITextConverterService _textConverterService;


        // Servisi constructor ile enjekte ediyoruz
        public TextConverterController(ITextConverterService textConverterService)
        {
            _textConverterService = textConverterService;
        }


        // POST metodu ile metni işleyip sayılara dönüştüreceğiz
        [HttpPost("convert")]
        public IActionResult ConvertText([FromBody] UserText userText)
        {
            if (userText == null || string.IsNullOrWhiteSpace(userText.InputText))
            {
                return BadRequest("Geçerli bir metin giriniz.");
            }

            // Servis ile metni işleme
            var result = _textConverterService.ConvertText(userText);

            // Sonucu döndürme
            return Ok(result);
        }
    }
}
