using System;
using System.Collections;

namespace Munq.FluentTest
{
    public partial class FluentTestObject : IFluentTest
    {
        #region IFluentTest members
        IFluentTest IFluentTest.WithFailureMessage(string msg)
        {
            ErrorMessage = msg;
            return this;
        }

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
                Verify.Fail();
            return this;
        }

        IFluentTest IFluentTest.IsNotEqualTo(object objectToCompare)
        {
            if (_IsEqual(objectToCompare))
                Verify.Fail();
            return this;
        }

        IFluentTest IFluentTest.IsTheSameObjectAs(object objectToCompare)
        {
            if (!Object.ReferenceEquals(ObjectToTest, objectToCompare))
                Verify.Fail();
            return this;
        }

        IFluentTest IFluentTest.IsNotTheSameObjectAs(object objectToCompare)
        {
            if (Object.ReferenceEquals(ObjectToTest, objectToCompare))
                Verify.Fail();
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
                Verify.Fail();
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
                Verify.Fail();
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
                Verify.Fail();
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
                Verify.Fail();
            return this;
        }

        IFluentTest IFluentTest.IsNotNull()
        {
            if (ObjectToTest == null)
                Verify.Fail();
            return this;
        }

        IFluentTestCollection IFluentTest.IsACollection()
        {
            if (!_IsAnInstanceOf(typeof(ICollection)))
                Verify.Fail();

            return this;
        }

        IFluentTestString IFluentTest.IsAString()
        {
            if (!_IsAnInstanceOf(typeof(string)))
                Verify.Fail();

            return this;
        }
        #endregion
    }
}
