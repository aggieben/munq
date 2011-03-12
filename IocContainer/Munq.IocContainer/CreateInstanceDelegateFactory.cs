using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Munq
{
	/// <summary>
	/// This class provides a method to build a delegate to create a specified type.  It is used
	/// by Register&lt;TType, TImp&gt;() to build the Func&lt;IDependencyResolver, TImp&gt; need
	/// to create the instance to be returned.  Also used by the Resolve methods if the type requested
	/// is a class, not an interface, and is not currently Registered in the container.
	/// </summary>
    internal class CreateInstanceDelegateFactory
       
    {
		/// <summary>
		/// Build a delegate to return an instance of the specified type given an instance of IocContainer.
		/// Finds the public constructor with the most parameters.  The resulting method calls the container
		/// to resolve each parameter in the constructor.
		/// </summary>
		/// <param name="tImpl">The class to be resolved.</param>
		/// <returns>The delegate to create an instance of the class.</returns>
		public static System.Func<IDependencyResolver, object> Create(Type tImpl)
		{
			ParameterExpression container = Expression.Parameter(typeof(IDependencyResolver), "container");
			NewExpression exp             = BuildExpression(tImpl, container);
			return Expression.Lambda<System.Func<IDependencyResolver, object>>(
					exp,
					new ParameterExpression[] { container }
				).Compile();
		}

		private static NewExpression BuildExpression(Type type, ParameterExpression container)
		{
			ConstructorInfo constructor = GetConstructorInfo(type);
			ParameterInfo[] parameters  = constructor.GetParameters();

			// create the arguments for the constructor	
			List<Expression> arguments = new List<Expression>();

			foreach (var paramInfo in parameters)
			{
				var p = Expression.Call(container, "Resolve", new Type[] { paramInfo.ParameterType },
				  new Expression[] { });
				arguments.Add(p);
			}

			// create the new MyClass( ... ) call
			return Expression.New(constructor, arguments);
		}

		private static ConstructorInfo GetConstructorInfo(Type implType)
        {
            var constructors = implType.GetConstructors();
            var constructor  = constructors
							   .OrderBy(c => c.GetParameters().Length)
							   .LastOrDefault();
			if (constructor == null)
                throw new ArgumentException("The requested class does not have a public constructor.");

            return constructor;
        }

    }
}

