namespace Munq.Test
{
    public interface IFoo
    {
        string Name { get; }
    }

    public class Foo1: IFoo
    {
        #region IFoo Members

        public string Name
        {
            get { return "Foo1"; }
        }

        #endregion
    }

    public class Foo2 : IFoo
    {
        #region IFoo Members

        public string Name
        {
            get { return "Foo2"; }
        }

        #endregion
    }

    public interface IBar
    {
    }

    public class Bar1 : IBar
    {
    }

    public class Bar2 : IBar
    {
    }

    public interface IFooBar
    {
        IFoo foo { get; }
        IBar bar { get; }
    }

    public class FooBar : IFooBar
    {
        public IFoo foo { get; private set; }
        public IBar bar { get; private set; }

        public FooBar(IFoo f, IBar b)
        {
            foo = f;
            bar = b;
        }
    }

    public interface INoConstructor
    {
    }

    public class NoConstructor : INoConstructor
    {
        private NoConstructor() { }
    }
}
