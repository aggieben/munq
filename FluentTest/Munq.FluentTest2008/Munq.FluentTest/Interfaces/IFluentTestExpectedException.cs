using System;

namespace Munq.FluentTest
{
    public interface IFluentTestExpectedException
    {
        IFluentTestThrownException IsThrownWhen(Action whatToDo);
    }
    
    public interface IFluentTestThrownException
    {
        IFluentTestString AndHasAMessageThat();
    }
}
