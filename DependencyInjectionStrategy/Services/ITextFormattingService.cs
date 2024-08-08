namespace DependencyInjectionStrategy.Services
{
	/// <summary>
	/// Simple interface with two implementations LowerCaseTextFormattingService and UpperCaseTextFormattingService
	/// for demostration purposes
	/// </summary>
	public interface ITextFormattingService
	{
		public string FormatText(string text);
	}
}
