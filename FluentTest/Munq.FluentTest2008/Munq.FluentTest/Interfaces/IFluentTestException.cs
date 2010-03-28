using System;

namespace Munq.FluentTest
{
    public interface IFluentTestException
    {
        void IsThrownWhen(Action whatToDo);
    }
}
