using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using Domain;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.StaticFactory;

namespace Performance
{
	[Description("Unity")]
	public class UnityUseCase : UseCase
	{
		UnityContainer container;

		public UnityUseCase()
		{
			container = new UnityContainer();

			var builder = container
				.AddNewExtension<StaticFactoryExtension>()
				.Configure<IStaticFactoryConfiguration>();

			builder.RegisterFactory<IWebService>(
				c => new WebService(
					c.Resolve<IAuthenticator>(),
					c.Resolve<IStockQuote>()));

			builder.RegisterFactory<IAuthenticator>(
				c => new Authenticator(
					c.Resolve<ILogger>(),
					c.Resolve<IErrorHandler>(),
					c.Resolve<IDatabase>()));

			builder.RegisterFactory<IStockQuote>(
				c => new StockQuote(
					c.Resolve<ILogger>(),
					c.Resolve<IErrorHandler>(),
					c.Resolve<IDatabase>()));

			builder.RegisterFactory<IDatabase>(
				c => new Database(
					c.Resolve<ILogger>(),
					c.Resolve<IErrorHandler>()));

			builder.RegisterFactory<IErrorHandler>(
				c => new ErrorHandler(c.Resolve<ILogger>()));

			var logger = new Logger();

			try
			{
				// Why on earth is this throwing?!?!?!
				container.RegisterInstance<ILogger>(logger);
			}
			catch (SynchronizationLockException) { }

			Debug.Assert(Object.ReferenceEquals(logger, container.Resolve<ILogger>()));
		}

		public override void Run()
		{
			var webApp = container.Resolve<IWebService>();
			webApp.Execute();
		}
	}
}
