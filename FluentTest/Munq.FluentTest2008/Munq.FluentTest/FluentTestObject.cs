using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Munq.FluentTest
{
    public class FluentTestObject : FluentTestCommon, Munq.FluentTest.IFluentTest
    {
        public FluentTestObject(object objectToTest)
            : base(objectToTest)
        {
        }

        #region IFluentTest members

        IFluentTest IFluentTest.IsEqualTo(object objectToCompare)
        {
            Assert.AreEqual(ObjectToTest, objectToCompare);
            return this;
        }

        IFluentTest IFluentTest.IsEqualTo(object objectToCompare, string msg)
        {
            Assert.AreEqual(ObjectToTest, objectToCompare, msg);
            return this;
        }

        IFluentTest IFluentTest.IsNotEqualTo(object objectToCompare)
        {
            Assert.AreNotEqual(ObjectToTest, objectToCompare);
            return this;
        }

        IFluentTest IFluentTest.IsNotEqualTo(object objectToCompare, string msg)
        {
            Assert.AreNotEqual(ObjectToTest, objectToCompare, msg);
            return this;
        }

        IFluentTest IFluentTest.IsTheSameObjectAs(object objectToCompare)
        {
            Assert.AreSame(ObjectToTest, objectToCompare);
            return this;
        }

        IFluentTest IFluentTest.IsTheSameObjectAs(object objectToCompare, string msg)
        {
            Assert.AreSame(ObjectToTest, objectToCompare, msg);
            return this;
        }

        IFluentTest IFluentTest.IsNotTheSameObjectAs(object objectToCompare)
        {
            Assert.AreNotSame(ObjectToTest, objectToCompare);
            return this;
        }

        IFluentTest IFluentTest.IsNotTheSameObjectAs(object objectToCompare, string msg)
        {
            Assert.AreNotSame(ObjectToTest, objectToCompare, msg);
            return this;
        }

        IFluentTest IFluentTest.IsTrue()
        {
            Assert.IsInstanceOfType(ObjectToTest, typeof(bool));
            Assert.IsTrue((bool)ObjectToTest);
            return this;
        }

        IFluentTest IFluentTest.IsTrue(string msg)
        {
            Assert.IsInstanceOfType(ObjectToTest, typeof(bool));
            Assert.IsTrue((bool)ObjectToTest, msg);
            return this;
        }

        IFluentTest IFluentTest.IsFalse()
        {
            Assert.IsInstanceOfType(ObjectToTest, typeof(bool));
            Assert.IsFalse((bool)ObjectToTest);
            return this;
        }

        IFluentTest IFluentTest.IsFalse(string msg)
        {
            Assert.IsInstanceOfType(ObjectToTest, typeof(bool));
            Assert.IsFalse((bool)ObjectToTest, msg);
            return this;
        }

        IFluentTest IFluentTest.IsAnInstanceOfType(Type type)
        {
            Assert.IsInstanceOfType(ObjectToTest, type);
            return this;
        }

        IFluentTest IFluentTest.IsAnInstanceOfType(Type type, string msg)
        {
            Assert.IsInstanceOfType(ObjectToTest, type, msg);
            return this;
        }

        IFluentTest IFluentTest.IsNotAnInstanceOfType(Type type)
        {
            Assert.IsNotInstanceOfType(ObjectToTest, type);
            return this;
        }

        IFluentTest IFluentTest.IsNotAnInstanceOfType(Type type, string msg)
        {
            Assert.IsNotInstanceOfType(ObjectToTest, type, msg);
            return this;
        }

        IFluentTest IFluentTest.IsNotNull()
        {
            Assert.IsNotNull(ObjectToTest);
            return this;
        }

        IFluentTest IFluentTest.IsNotNull(string msg)
        {
            Assert.IsNotNull(ObjectToTest, msg);
            return this;
        }

        IFluentCollectionTest IFluentTest.IsACollection()
        {
            Assert.IsInstanceOfType(ObjectToTest, typeof(ICollection));
            
            return new FluentTestCollection(ObjectToTest as ICollection);
        }

        IFluentCollectionTest IFluentTest.IsACollection(string msg)
        {
            Assert.IsInstanceOfType(ObjectToTest, typeof(ICollection), msg);
            return new FluentTestCollection(ObjectToTest as ICollection);
        }

        IFluentStringTest IFluentTest.IsAString()
        {
            Assert.IsInstanceOfType(ObjectToTest, typeof(string));
            return new FluentTestString(ObjectToTest as string);
        }

        IFluentStringTest IFluentTest.IsAString(string msg)
        {
            Assert.IsInstanceOfType(ObjectToTest, typeof(string), msg);
            return new FluentTestString(ObjectToTest as string);
        }
        #endregion
    }
}
