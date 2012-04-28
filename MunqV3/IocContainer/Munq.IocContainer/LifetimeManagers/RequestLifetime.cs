// --------------------------------------------------------------------------------------------------
// © Copyright 2011 by Matthew Dennis.
// Released under the Microsoft Public License (Ms-PL) http://www.opensource.org/licenses/ms-pl.html
// --------------------------------------------------------------------------------------------------

using System.Web;
using System;
using System.Collections.Generic;

namespace Munq.LifetimeManagers
{
	/// <summary>
	/// A lifetime manager that scopes the lifetime of created instances to the duration of the
	/// current HttpRequest.
	/// </summary>
	/// <example>
	/// In Web applications, it is sometime desirable to ensure that all classes use the same instance
	/// of a dependency, but only for the duration of the request.  One example might be a Repository
	/// that implements the Unit of Work pattern, such as an Entity Framework base repository.
	/// <code>
	/// public class ArticleRepository : IArticleRepository
	/// {
	///    ...
	/// }
	/// 
	/// public class SpanishTranslator : IArticleTranslator
	/// {
	///     public SpanishTranslator(IArticleRepository repository)
	///     {
	///         ...
	///     }
	///     ...
	/// }
	///  
	/// public class ArticleController : IController 
	/// {
	///     IArticleRepository _repository;
	///     IArticleTranslator _translator;
	///     public ArticleController(IArticleRepository repository, IArticleTranslator translator)
	///     {
	///         _repository = repository;
	///         _translator = translator;
	///     }
	///     
	///     public ActionResult Save(ArticleModel article)
	///     {
	///         _repository.Save(article);
	///         _translator.Save(article); // uses the same IArticleRepository instance
	///         _repository.AcceptChanges();
	///     }
	///     ...
	/// }
	/// 
	///     // initialization code, probably in global.ascx
	///	protected void Application_Start()
	///	{
	///		DependencyResolver.SetResolver(new MunqDependencyResolver());
	///		var ioc = MunqDependencyResolver.Container;
	///		var requestLifetime = new RequestLifetime();
	///		ioc.Register&lt;IArticleRepository, ArticleRepository&gt;()
	///		   .WithLifetimeManager(requestLifetime);
	///		ioc.Register&lt;IArticleTranslator, ArticleTranslator&gt;()
	///		   .WithLifetimeManager(requestLifetime);
	///     ...
	/// </code>
	/// </example>
	public class RequestLifetime : ILifetimeManager
	{
		private const string MunqRequestLtItemsKey = "MunqRequestLtItemsKey";
        private static HttpContextBase testContext;
		private readonly object _lock = new object();

		public static void Disposer(object sender, EventArgs e)
		{
			var application = (HttpApplication)sender;
			var _context = application.Context;
			if (_context != null)
			{
				Dictionary<string, object> requestLifetimeInstances = (Dictionary<string, object>)_context.Items[MunqRequestLtItemsKey];
				if (requestLifetimeInstances != null)
				{
					foreach (var item in requestLifetimeInstances.Values)
					{
						if (item is IDisposable)
							(item as IDisposable).Dispose();
					}
				}
			}
		}

		/// <summary>
		/// Return the HttpContext if running in a web application, the test 
		/// context otherwise.
		/// </summary>
		private static HttpContextBase Context
		{
			get
			{
				HttpContextBase context = (HttpContext.Current != null)
								? new HttpContextWrapper(HttpContext.Current)
								: testContext;
				return context;
			}
		}

		private static Dictionary<string, object> RequestLifetimeInstances
		{
			get
			{
				Dictionary<string, object> requestLifetimeInstances = Context.Items[MunqRequestLtItemsKey] as Dictionary<string, object>;
				if (requestLifetimeInstances == null)
				{
					requestLifetimeInstances = new Dictionary<string, object>();
					Context.Items[MunqRequestLtItemsKey] = requestLifetimeInstances;
				}
				return requestLifetimeInstances;
			}
		}

		#region ILifetimeManage Members
		/// <summary>
		/// Gets the instance from the Request Items, if available, otherwise creates a new
		/// instance and stores in the Request Items.
		/// </summary>
		/// <param name="registration">The creator (registration) to create a new instance.</param>
		/// <returns>The instance.</returns>
		public object GetInstance(IRegistration registration)
		{
			object instance;
			if (!RequestLifetimeInstances.TryGetValue(registration.Key, out instance))
			{
				lock (_lock)
				{
					if (!RequestLifetimeInstances.TryGetValue(registration.Key, out instance))
					{
						instance                        = registration.CreateInstance();
						RequestLifetimeInstances[registration.Key] = instance;
					}
				}
			}

			return instance;
		}

		/// <summary>
		/// Invalidates the cached value.
		/// </summary>
		/// <param name="registration">The Registration which is having its value invalidated</param>
		public void InvalidateInstanceCache(IRegistration registration)
		{
			RequestLifetimeInstances.Remove(registration.Key);
		}

		#endregion

		/// <summary>
		/// Only used for testing.  Has no effect when in web application
		/// </summary>
		/// <param name="context"></param>
		public static void SetContext(HttpContextBase context)
		{
			testContext = context;
		}
	}
}
