#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

using System;

namespace Munq.FluentTest
{
    public interface IFluentTestProvider
    {
        void Fail();
        void Fail(string msg);

        void InConclusive();
        void InConclusive(string msg);

        Type FailExceptionType { get; }
        Type InConclusiveExceptionType { get; }

    }
}
