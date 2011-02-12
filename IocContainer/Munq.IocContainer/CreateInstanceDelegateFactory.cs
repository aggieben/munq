using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Munq
{
    public class CreateInstanceDelegateFactory
       
    {
		public static System.Func<IDependencyResolver, TType> Create<TType, TImpl>()
			where TType : class
			where TImpl : class, TType
		{
			ConstructorInfo constructor = GetConstructorInfo(typeof(TImpl));
			ParameterInfo[] parameters = constructor.GetParameters();

			ParameterExpression container = Expression.Parameter(typeof(IDependencyResolver), "container");

			List<Expression> arguments = new List<Expression>();
			// create the arguments for the constructor
			foreach (var paramInfo in parameters)
			{

				var p = Expression.Call(container, "Resolve", new Type[] { paramInfo.ParameterType },
				  new Expression[] { });
				arguments.Add(p);
			}

			NewExpression exp = Expression.New(constructor, arguments);

			var finalExp = Expression.Lambda<System.Func<IDependencyResolver, TType>>(
											exp,
											new ParameterExpression[] { container }
										);
			return finalExp.Compile();
		}

		public static System.Func<IDependencyResolver, object> Create(Type tImpl)
		{
			ConstructorInfo constructor = GetConstructorInfo(tImpl);
			ParameterInfo[] parameters = constructor.GetParameters();

			ParameterExpression container = Expression.Parameter(typeof(IDependencyResolver), "container");

			List<Expression> arguments = new List<Expression>();
			// create the arguments for the constructor
			foreach (var paramInfo in parameters)
			{

				var p = Expression.Call(container, "Resolve", new Type[] { paramInfo.ParameterType },
				  new Expression[] { });
				arguments.Add(p);
			}

			NewExpression exp = Expression.New(constructor, arguments);

			return Expression.Lambda<System.Func<IDependencyResolver, object>>(
					exp,
					new ParameterExpression[] { container }
				).Compile();
		}

		private static ConstructorInfo GetConstructorInfo(Type implType)
        {
            var constructors = implType.GetConstructors();
            var constructor = constructors
                                .OrderBy(c => c.GetParameters().Length)
                                .LastOrDefault();
            if (constructor == null)
                throw new ArgumentException("TImpl does not have a public constructor.");

            return constructor;
        }

    }
}

