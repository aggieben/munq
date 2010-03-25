using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using System.Reflection;
using Munq.DI;
using Munq.DI.LifetimeManagers;

namespace Performance
{
	[System.ComponentModel.Description("Munq")]
	public class MunqUseCase : UseCase
	{
		Container container;
		ILifetimeManager lifetime = new ContainerLifetime();

		public MunqUseCase()
		{
			container = new Container().UsesDefaultLifetimeManagerOf(lifetime);

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

			container.RegisterInstance<ILogger>(new Logger());
		}

		public override void Run()
		{
			var webApp = container.Resolve<IWebService>();
			webApp.Execute();
		}
	}
}
