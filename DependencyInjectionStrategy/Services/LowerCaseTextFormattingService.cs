namespace DependencyInjectionStrategy.Services
{
	class LowerCaseTextFormattingService : ITextFormattingService
	{
		public string FormatText(string text)
		{
			return text.ToLower();
		}
	}
}
