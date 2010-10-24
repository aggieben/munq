using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Munq
{
    public class CreateInstanceDelegateFactory<TType, TImpl>
        where TType : class 
        where TImpl : class, TType
    {
        public static System.Func<IIocContainer, TType> Create()
        {
            ConstructorInfo constructor = GetConstructorInfo();
            ParameterInfo[] parameters = constructor.GetParameters();

            ParameterExpression container = Expression.Parameter(typeof(IIocContainer), "container");

            List<Expression> arguments = new List<Expression>();
            // create the arguments for the constructor
            foreach (var paramInfo in parameters)
            {

                var p = Expression.Call(container, "Resolve", new Type[] { paramInfo.ParameterType },
                  new Expression[] {  });
                arguments.Add(p);
            }

            NewExpression exp = Expression.New(constructor, arguments);

            return Expression.Lambda<System.Func<IIocContainer, TType>>(
                    exp,
                    new ParameterExpression[] { container }
                ).Compile();
        }

        private static ConstructorInfo GetConstructorInfo() 
        {
            var implType = typeof(TImpl);
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

