using System;
using System.Collections;

namespace Munq.FluentTest
{
    public class FluentTestObject : FluentTestCommon, Munq.FluentTest.IFluentTest
    {
        public FluentTestObject(object objectToTest)
            : base(objectToTest)
        {
        }

        #region IFluentTest members
        private bool _IsEqual(object objectToCompare)
        {
            if (ObjectToTest == null && objectToCompare == null)
                return true;

            return (ObjectToTest != null && objectToCompare != null
                    && ObjectToTest.Equals(objectToCompare));
        }

        IFluentTest IFluentTest.IsEqualTo(object objectToCompare)
        {
            if (!_IsEqual(objectToCompare))
                Verify.Provider.Fail();
            return this;
        }

        IFluentTest IFluentTest.IsEqualTo(object objectToCompare, string msg)
        {
            if (!_IsEqual(objectToCompare))
                Verify.Provider.Fail(msg);
            return this;
        }

        IFluentTest IFluentTest.IsNotEqualTo(object objectToCompare)
        {
            if (_IsEqual(objectToCompare))
                Verify.Provider.Fail();
            return this;
        }

        IFluentTest IFluentTest.IsNotEqualTo(object objectToCompare, string msg)
        {
            if (_IsEqual(objectToCompare))
                Verify.Provider.Fail(msg);
            return this;
        }

        IFluentTest IFluentTest.IsTheSameObjectAs(object objectToCompare)
        {
            if (!Object.ReferenceEquals(ObjectToTest, objectToCompare))
                Verify.Provider.Fail();
            return this;
        }

        IFluentTest IFluentTest.IsTheSameObjectAs(object objectToCompare, string msg)
        {
            if (!Object.ReferenceEquals(ObjectToTest, objectToCompare))
                Verify.Provider.Fail(msg);
            return this;
        }

        IFluentTest IFluentTest.IsNotTheSameObjectAs(object objectToCompare)
        {
            if (Object.ReferenceEquals(ObjectToTest, objectToCompare))
                Verify.Provider.Fail();
            return this;
        }

        IFluentTest IFluentTest.IsNotTheSameObjectAs(object objectToCompare, string msg)
        {
            if (!Object.ReferenceEquals(ObjectToTest, objectToCompare))
                Verify.Provider.Fail(msg);
            return this;
        }


        private bool _IsTrue()
        {
            try
            {
                return ObjectToTest != null && (bool)ObjectToTest;
            }
            catch
            {
                return false;
            }
        }
        
        IFluentTest IFluentTest.IsTrue()
        {
            if (!_IsTrue())
                Verify.Provider.Fail();
            return this;
        }

        IFluentTest IFluentTest.IsTrue(string msg)
        {
            if (!_IsTrue())
                Verify.Provider.Fail(msg);
            return this;
        }


        private bool _IsFalse()
        {
            try
            {
            return ObjectToTest != null && !(bool)ObjectToTest;
            }
            catch
            {
                return false;
            }
        }
        IFluentTest IFluentTest.IsFalse()
        {
            if (!_IsFalse())
                Verify.Provider.Fail();
            return this;
        }

        IFluentTest IFluentTest.IsFalse(string msg)
        {
            if (!_IsFalse())
                Verify.Provider.Fail(msg);
            return this;
        }

        private bool _IsAnInstanceOf(Type type)
        {
            return ObjectToTest != null &&
                    type.IsAssignableFrom(ObjectToTest.GetType());
        }
        
        IFluentTest IFluentTest.IsAnInstanceOfType(Type type)
        {
            if (!_IsAnInstanceOf(type))
                Verify.Provider.Fail();
            return this;
        }

        IFluentTest IFluentTest.IsAnInstanceOfType(Type type, string msg)
        {
            if (!_IsAnInstanceOf(type))
                Verify.Provider.Fail(msg);
            return this;
        }

        private bool _IsNotAnInstanceOf(Type type)
        {
            return ObjectToTest != null &&
                    !type.IsAssignableFrom(ObjectToTest.GetType());
        }
        IFluentTest IFluentTest.IsNotAnInstanceOfType(Type type)
        {
            if (!_IsNotAnInstanceOf(type))
                Verify.Provider.Fail();
            return this;
        }

        IFluentTest IFluentTest.IsNotAnInstanceOfType(Type type, string msg)
        {
            if (!_IsNotAnInstanceOf(type))
                Verify.Provider.Fail(msg);
            return this;
        }

        IFluentTest IFluentTest.IsNotNull()
        {
            if (ObjectToTest == null)
                Verify.Provider.Fail();
            return this;
        }

        IFluentTest IFluentTest.IsNotNull(string msg)
        {
            if (ObjectToTest == null)
                Verify.Provider.Fail(msg);
            return this;
        }

        IFluentCollectionTest IFluentTest.IsACollection()
        {
            if (!_IsAnInstanceOf(typeof(ICollection)))
                Verify.Provider.Fail();

            return new FluentTestCollection(ObjectToTest as ICollection);
        }

        IFluentCollectionTest IFluentTest.IsACollection(string msg)
        {
            if (!_IsAnInstanceOf(typeof(ICollection)))
                Verify.Provider.Fail(msg);

            return new FluentTestCollection(ObjectToTest as ICollection);
        }

        IFluentStringTest IFluentTest.IsAString()
        {
            if (!_IsAnInstanceOf(typeof(string)))
                Verify.Provider.Fail();

            return new FluentTestString(ObjectToTest as string);
        }

        IFluentStringTest IFluentTest.IsAString(string msg)
        {
            if (!_IsAnInstanceOf(typeof(string)))
                Verify.Provider.Fail();

            return new FluentTestString(ObjectToTest as string);
        }
        #endregion
    }
}
