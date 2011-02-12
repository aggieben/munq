#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

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
                FailWithObjectToCompareDefaultMessage("should be equal to", objectToCompare);
            return this;
        }

        IFluentTest IFluentTest.IsNotEqualTo(object objectToCompare)
        {
            if (_IsEqual(objectToCompare))
                FailWithObjectToCompareDefaultMessage("should not be equal to", objectToCompare);
            return this;
        }

        IFluentTest IFluentTest.IsTheSameObjectAs(object objectToCompare)
        {
            if (!Object.ReferenceEquals(ObjectToTest, objectToCompare))
                FailWithObjectToCompareDefaultMessage("should be the same object as", objectToCompare);
            return this;
        }

        IFluentTest IFluentTest.IsNotTheSameObjectAs(object objectToCompare)
        {
            if (Object.ReferenceEquals(ObjectToTest, objectToCompare))
                FailWithObjectToCompareDefaultMessage("should not be the same object as", objectToCompare);
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
                FailWithDefaultMessage("should be true");
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
                FailWithDefaultMessage("should be false");
            return this;
        }

        private bool _IsAnInstanceOf(Type type)
        {
            return  (ObjectToTest == null && type == null) ||
                    (ObjectToTest != null && type != null &&
                    type.IsAssignableFrom(ObjectToTest.GetType()));
        }

        IFluentTest IFluentTest.IsAnInstanceOfType(Type type)
        {
            if (!_IsAnInstanceOf(type))
                FailWithTypeDefaultMessage("should be an instance of type", type);
            return this;
        }

        private bool _IsNotAnInstanceOf(Type type)
        {
            return  !(ObjectToTest == null && type == null) &&
                    (ObjectToTest != null && type != null &&
                    !type.IsAssignableFrom(ObjectToTest.GetType()));
        }
        
        IFluentTest IFluentTest.IsNotAnInstanceOfType(Type type)
        {
            if (!_IsNotAnInstanceOf(type))
                FailWithTypeDefaultMessage("should not be an instance of type", type);
            return this;
        }

        IFluentTest IFluentTest.IsNotNull()
        {
            if (ObjectToTest == null)
                FailWithDefaultMessage("should not be null");
            return this;
        }

        IFluentTestCollection IFluentTest.IsACollectionThat()
        {
            if (!_IsAnInstanceOf(typeof(ICollection)))
                FailWithDefaultMessage("is not a collection");

            return this;
        }

        IFluentTestString IFluentTest.IsAStringThat()
        {
            if (!_IsAnInstanceOf(typeof(string)))
                FailWithDefaultMessage("is not a string");

            return this;
        }
        #endregion
    }
}
