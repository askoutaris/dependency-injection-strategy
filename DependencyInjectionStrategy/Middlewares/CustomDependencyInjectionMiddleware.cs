using Autofac;
using Autofac.Extensions.DependencyInjection;
using DependencyInjectionStrategy.Services;

namespace DependencyInjectionStrategy.Middlewares
{
	public class CustomDependencyInjectionMiddleware
	{
		private readonly RequestDelegate _next;

		public CustomDependencyInjectionMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context, ILifetimeScope parentScope)
		{
			// get header value
			var formattingStrategyHeader = context.Request.Headers[Constants.Headers.FormattingStrategy].FirstOrDefault();

			// try parse header value as TextFormattingStrategy enum or assign a default value
			// you could return 400 BadRequest too in case parsing fails
			if (!Enum.TryParse<TextFormattingStrategy>(formattingStrategyHeader, true, out TextFormattingStrategy textFormattingStrategy))
				textFormattingStrategy = TextFormattingStrategy.Lower;

			// parentScope is the DI scope created for each HTTP request.
			// we use BeginLifetimeScope to create a new scope based on parentScope and
			// register any other additional types we like
			using var scope = parentScope.BeginLifetimeScope(builder =>
			{
				// register corresponding interface implementation (stragety pattern) base on enum value
				// you could have a more complex logic here, involving more parameters,
				// to register various services based on a configuration file instead of this hard coded switch logic
				switch (textFormattingStrategy)
				{
					case TextFormattingStrategy.Lower:
						builder
							.RegisterType<LowerCaseTextFormattingService>().AsImplementedInterfaces().SingleInstance();
						break;
					case TextFormattingStrategy.Upper:
						builder.RegisterType<UpperCaseTextFormattingService>().AsImplementedInterfaces().SingleInstance();
						break;
					default:
						throw new Exception($"Unexpected TextFormattingStrategy {textFormattingStrategy}");
				}
			});

			// replace context.RequestServices with a new AutofacServiceProvider
			// providing our own newly created scope with our custom registrations to be used by next middlewares
			context.RequestServices = new AutofacServiceProvider(scope);

			await _next(context);
		}
	}
}
