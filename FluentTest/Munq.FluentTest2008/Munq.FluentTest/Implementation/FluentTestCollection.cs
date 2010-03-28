using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Munq.FluentTest
{
    public partial class FluentTestObject : IFluentCollectionTest
    {
        private ICollection CollectionToTest { get { return ObjectToTest as ICollection; } }

        #region CollectionAssert members
        IFluentCollectionTest IFluentCollectionTest.WithFailureMessage(string msg)
        {
            ErrorMessage = msg;
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsTheSameCollectionAs(ICollection objectToCompare)
        {
            Assert.AreSame(ObjectToTest, objectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotTheSameCollectionAs(ICollection objectToCompare)
        {
            Assert.AreNotSame(ObjectToTest, objectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsAnInstanceOfType(Type type)
        {
            Assert.IsInstanceOfType(ObjectToTest, type);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotAnInstanceOfType(Type type)
        {
            Assert.IsNotInstanceOfType(ObjectToTest, type);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotNull()
        {
            Assert.IsNotNull(ObjectToTest);
            return this;
        }
      
        IFluentCollectionTest IFluentCollectionTest.AllItemsAreInstancesOfType(Type type)
        {
            CollectionAssert.AllItemsAreInstancesOfType(CollectionToTest, type);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.AllItemsAreNotNull()
        {
            CollectionAssert.AllItemsAreNotNull(CollectionToTest);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.AllItemsAreUnique()
        {
            CollectionAssert.AllItemsAreUnique(CollectionToTest);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsEqualTo(ICollection ObjectToCompare)
        {
            CollectionAssert.AreEqual(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotEqualTo(ICollection ObjectToCompare)
        {
            CollectionAssert.AreNotEqual(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsEquivalentTo(ICollection ObjectToCompare)
        {
            CollectionAssert.AreEquivalent(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotEquivalentTo(ICollection ObjectToCompare)
        {
            CollectionAssert.AreNotEquivalent(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.Contains(object ObjectToCompare)
        {
            CollectionAssert.Contains(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.DoesNotContain(object ObjectToCompare)
        {
            CollectionAssert.DoesNotContain(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsASubsetOf(ICollection ObjectToCompare)
        {
            CollectionAssert.IsSubsetOf(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotASubsetOf(ICollection ObjectToCompare)
        {
            CollectionAssert.IsNotSubsetOf(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsASupersetOf(ICollection ObjectToCompare)
        {
            CollectionAssert.IsSubsetOf(ObjectToCompare, CollectionToTest);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotASupersetOf(ICollection ObjectToCompare)
        {
            CollectionAssert.IsNotSubsetOf(ObjectToCompare, CollectionToTest);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsEqualTo(int numberOfItems)
        {
            Assert.IsTrue(CollectionToTest.Count == numberOfItems);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsNotEqualTo(int numberOfItems)
        {
            Assert.IsTrue(CollectionToTest.Count != numberOfItems);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsGreaterThan(int numberOfItems)
        {
            Assert.IsTrue(CollectionToTest.Count > numberOfItems);
            return this;
            throw new NotImplementedException();
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsGreaterThanOrEqualTo(int numberOfItems)
        {
            Assert.IsTrue(CollectionToTest.Count >= numberOfItems);
            return this;
            throw new NotImplementedException();
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsLessThan(int numberOfItems)
        {
            Assert.IsTrue(CollectionToTest.Count < numberOfItems);
            return this;
            throw new NotImplementedException();
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsLessThanOrEqualTo(int numberOfItems)
        {
            Assert.IsTrue(CollectionToTest.Count <= numberOfItems);
            return this;
            throw new NotImplementedException();
        }
        #endregion
    }
}
