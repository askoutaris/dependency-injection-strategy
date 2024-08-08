using Autofac;

namespace DependencyInjectionStrategy
{
	public class AutofacModule: Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			//builder
			//	.RegisterType<ApiMetrics>()
			//	.SingleInstance()
			//	.AsImplementedInterfaces();
		}
	}
}
