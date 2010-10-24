using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StructureMap;
using Domain;

namespace Performance
{
	[System.ComponentModel.Description("StructureMap")]
	public class StructureMapUseCase : UseCase
	{
		Container container;

		public StructureMapUseCase()
		{
			container = new Container();
			container.Configure(
				x => x.For<IWebService>()
					  .Use<WebService>());

			container.Configure(
				x => x.For<IAuthenticator>()
					.Use<Authenticator>());

			container.Configure(
				x => x.For<IStockQuote>()
					.Use<StockQuote>());

			container.Configure(
				x => x.For<IDatabase>()
					.Use<Database>());

			container.Configure(
				x => x.For<IErrorHandler>()
					.Use<ErrorHandler>());

			container.Configure(
				x => x.For<ILogger>()
					.Use(c => new Logger()));
		}

		public override void Run()
		{
			var webApp = container.GetInstance<IWebService>();
			webApp.Execute();
		}
	}
}
