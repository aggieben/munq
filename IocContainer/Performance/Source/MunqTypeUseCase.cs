using System;
using Domain;
using Munq;
using Munq.LifetimeManagers;

namespace Performance
{
	[System.ComponentModel.Description("MunqGeneric")]
	public class MunqGenericUseCase : UseCase
	{
		static IIocContainer container;
		static ILifetimeManager singleton = new ContainerLifetime();
		static ILifetimeManager lifetime = null; // new AlwaysNewLifetime();

		static MunqGenericUseCase()
		{
			container = new Container();

			container.Register<IWebService, WebService>();
			container.Register<IAuthenticator, Authenticator>();
			container.Register<IStockQuote, StockQuote>();
			container.Register<IDatabase, Database>();
			container.Register<IErrorHandler, ErrorHandler>();
			container.Register<ILogger,Logger>().WithLifetimeManager(singleton);
		}

		public override void Run()
		{
			var webApp = container.Resolve<IWebService>();
			webApp.Execute();
		}
	}
}
