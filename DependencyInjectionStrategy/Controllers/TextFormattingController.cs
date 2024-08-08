using DependencyInjectionStrategy.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjectionStrategy.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class TextFormattingController : ControllerBase
	{
		private readonly ITextFormattingService _service;

		public TextFormattingController(ITextFormattingService service)
		{
			_service = service;
		}

		[HttpGet(Name = "GetFormatedText")]
		public IActionResult GetFormatedText(string text)
		{
			var formattedText = _service.FormatText(text);

			return Ok(formattedText);
		}
	}
}
