using Autofac;
using Autofac.Extensions.DependencyInjection;
using DependencyInjectionStrategy.Middlewares;

namespace DependencyInjectionStrategy
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// replace built-in DI with Autofac for a more flexible DI configuration https://autofac.org/
			builder.Host
				.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureContainer<ContainerBuilder>((context, builder) =>
				{
					builder.RegisterModule(new AutofacModule());
				}); ;

			// register controller & swagger
			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(x =>
			{
				// add custom swagger IOperationFilter to add headers in each request via swagger UI
				x.OperationFilter<AddSwaggerHeaders>();
			});

			var app = builder.Build();

			// add custom middleware which create a new DI scope per request and injects dependencies dynamically
			app.UseMiddleware<CustomDependencyInjectionMiddleware>();

			// add swagger & controllers
			app.UseSwagger();
			app.UseSwaggerUI();
			app.MapControllers();

			app.Run();
		}
	}
}
