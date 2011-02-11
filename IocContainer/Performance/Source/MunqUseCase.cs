using System;
using Domain;
using Munq;

namespace Performance
{
	[System.ComponentModel.Description("Munq")]
	public class MunqUseCase : UseCase
	{
		static IIocContainer container;
		static ILifetimeManager singleton = new Munq.LifetimeManagers.ContainerLifetime();
		static ILifetimeManager lifetime = null; // new AlwaysNewLifetime();

		static MunqUseCase()
		{
			container = new Container();

			container.Register<IWebService>(
				c => new WebService(
					c.Resolve<IAuthenticator>(),
					c.Resolve<IStockQuote>()));

			container.Register<IAuthenticator>(
				c => new Authenticator(
					c.Resolve<ILogger>(),
					c.Resolve<IErrorHandler>(),
					c.Resolve<IDatabase>()));

			container.Register<IStockQuote>(
				c => new StockQuote(
					c.Resolve<ILogger>(),
					c.Resolve<IErrorHandler>(),
					c.Resolve<IDatabase>()));

			container.Register<IDatabase>(
				c => new Database(
					c.Resolve<ILogger>(),
					c.Resolve<IErrorHandler>()));

			container.Register<IErrorHandler>(
				c => new ErrorHandler(c.Resolve<ILogger>()));

			container.RegisterInstance<ILogger>(new Logger())
					.WithLifetimeManager(singleton);
		}

		public override void Run()
		{
			var webApp = container.Resolve<IWebService>();
			webApp.Execute();
		}
	}
}
