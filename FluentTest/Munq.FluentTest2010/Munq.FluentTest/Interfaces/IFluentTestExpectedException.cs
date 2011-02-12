#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

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
