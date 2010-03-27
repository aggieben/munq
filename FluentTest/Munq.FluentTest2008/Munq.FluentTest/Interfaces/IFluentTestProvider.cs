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
