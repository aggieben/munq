using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Munq
{
    public class CreateInstanceDelegateFactory
       
    {
		public static System.Func<IDependencyResolver, object> Create(Type tImpl)
		{
			ParameterExpression container = Expression.Parameter(typeof(IDependencyResolver), "container");
			NewExpression exp = BuildExpression(tImpl, container);
			return Expression.Lambda<System.Func<IDependencyResolver, object>>(
					exp,
					new ParameterExpression[] { container }
				).Compile();
		}

		private static NewExpression BuildExpression(Type type, ParameterExpression container)
		{
			ConstructorInfo constructor = GetConstructorInfo(type);
			ParameterInfo[] parameters = constructor.GetParameters();

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
            var constructor = constructors
                                .OrderBy(c => c.GetParameters().Length)
                                .LastOrDefault();
            if (constructor == null)
                throw new ArgumentException("The requested class does not have a public constructor.");

            return constructor;
        }

    }
}

