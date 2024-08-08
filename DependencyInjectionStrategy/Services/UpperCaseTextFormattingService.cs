namespace DependencyInjectionStrategy.Services
{
	class UpperCaseTextFormattingService : ITextFormattingService
	{
		public string FormatText(string text)
		{
			return text.ToUpper();
		}
	}
}
