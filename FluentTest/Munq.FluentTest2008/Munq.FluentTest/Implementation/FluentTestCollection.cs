using System;
using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Munq.FluentTest
{
    public partial class FluentTestObject : IFluentTestCommon, IFluentTest, IFluentStringTest, IFluentCollectionTest
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

        IFluentCollectionTest IFluentCollectionTest.IsTheSameCollectionAs(ICollection objectToCompare, string msg)
        {
            Assert.AreSame(CollectionToTest, objectToCompare, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotTheSameCollectionAs(ICollection objectToCompare)
        {
            Assert.AreNotSame(ObjectToTest, objectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotTheSameCollectionAs(ICollection objectToCompare, string msg)
        {
            Assert.AreNotSame(CollectionToTest, objectToCompare, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsAnInstanceOfType(Type type)
        {
            Assert.IsInstanceOfType(ObjectToTest, type);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsAnInstanceOfType(Type type, string msg)
        {
            Assert.IsInstanceOfType(CollectionToTest, type, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotAnInstanceOfType(Type type)
        {
            Assert.IsNotInstanceOfType(ObjectToTest, type);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotAnInstanceOfType(Type type, string msg)
        {
            Assert.IsNotInstanceOfType(CollectionToTest, type, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotNull()
        {
            Assert.IsNotNull(ObjectToTest);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotNull(string msg)
        {
            Assert.IsNotNull(CollectionToTest, msg);
            return this;
        }
        
        IFluentCollectionTest IFluentCollectionTest.AllItemsAreInstancesOfType(Type type)
        {
            CollectionAssert.AllItemsAreInstancesOfType(CollectionToTest, type);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.AllItemsAreInstancesOfType(Type type, string msg)
        {
            CollectionAssert.AllItemsAreInstancesOfType(CollectionToTest, type, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.AllItemsAreNotNull()
        {
            CollectionAssert.AllItemsAreNotNull(CollectionToTest);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.AllItemsAreNotNull(string msg)
        {
            CollectionAssert.AllItemsAreNotNull(CollectionToTest, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.AllItemsAreUnique()
        {
            CollectionAssert.AllItemsAreUnique(CollectionToTest);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.AllItemsAreUnique(string msg)
        {
            CollectionAssert.AllItemsAreUnique(CollectionToTest, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsEqualTo(ICollection ObjectToCompare)
        {
            CollectionAssert.AreEqual(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsEqualTo(ICollection ObjectToCompare, string msg)
        {
            CollectionAssert.AreEqual(CollectionToTest, ObjectToCompare, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotEqualTo(ICollection ObjectToCompare)
        {
            CollectionAssert.AreNotEqual(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotEqualTo(ICollection ObjectToCompare, string msg)
        {
            CollectionAssert.AreNotEqual(CollectionToTest, ObjectToCompare, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsEquivalentTo(ICollection ObjectToCompare)
        {
            CollectionAssert.AreEquivalent(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsEquivalentTo(ICollection ObjectToCompare, string msg)
        {
            CollectionAssert.AreEquivalent(CollectionToTest, ObjectToCompare, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotEquivalentTo(ICollection ObjectToCompare)
        {
            CollectionAssert.AreNotEquivalent(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotEquivalentTo(ICollection ObjectToCompare, string msg)
        {
            CollectionAssert.AreNotEquivalent(CollectionToTest, ObjectToCompare, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.Contains(object ObjectToCompare)
        {
            CollectionAssert.Contains(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.Contains(object ObjectToCompare, string msg)
        {
            CollectionAssert.Contains(CollectionToTest, ObjectToCompare, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.DoesNotContain(object ObjectToCompare)
        {
            CollectionAssert.DoesNotContain(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.DoesNotContain(object ObjectToCompare, string msg)
        {
            CollectionAssert.DoesNotContain(CollectionToTest, ObjectToCompare, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsASubsetOf(ICollection ObjectToCompare)
        {
            CollectionAssert.IsSubsetOf(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsASubsetOf(ICollection ObjectToCompare, string msg)
        {
            CollectionAssert.IsSubsetOf(CollectionToTest, ObjectToCompare, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotASubsetOf(ICollection ObjectToCompare)
        {
            CollectionAssert.IsNotSubsetOf(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotASubsetOf(ICollection ObjectToCompare, string msg)
        {
            CollectionAssert.IsNotSubsetOf(CollectionToTest, ObjectToCompare, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsASupersetOf(ICollection ObjectToCompare)
        {
            CollectionAssert.IsSubsetOf(ObjectToCompare, CollectionToTest);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsASupersetOf(ICollection ObjectToCompare, string msg)
        {
            CollectionAssert.IsSubsetOf(ObjectToCompare, CollectionToTest, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotASupersetOf(ICollection ObjectToCompare)
        {
            CollectionAssert.IsNotSubsetOf(ObjectToCompare, CollectionToTest);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.IsNotASupersetOf(ICollection ObjectToCompare, string msg)
        {
            CollectionAssert.IsNotSubsetOf(ObjectToCompare, CollectionToTest, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsEqualTo(int numberOfItems)
        {
            Assert.IsTrue(CollectionToTest.Count == numberOfItems);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsEqualTo(int numberOfItems, string msg)
        {
            Assert.IsTrue(CollectionToTest.Count == numberOfItems, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsNotEqualTo(int numberOfItems)
        {
            Assert.IsTrue(CollectionToTest.Count != numberOfItems);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsNotEqualTo(int numberOfItems, string msg)
        {
            Assert.IsTrue(CollectionToTest.Count != numberOfItems, msg);
            return this;
        }
  
        IFluentCollectionTest IFluentCollectionTest.CountIsGreaterThan(int numberOfItems)
        {
            Assert.IsTrue(CollectionToTest.Count > numberOfItems);
            return this;
            throw new NotImplementedException();
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsGreaterThan(int numberOfItems, string msg)
        {
            Assert.IsTrue(CollectionToTest.Count > numberOfItems, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsGreaterThanOrEqualTo(int numberOfItems)
        {
            Assert.IsTrue(CollectionToTest.Count >= numberOfItems);
            return this;
            throw new NotImplementedException();
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsGreaterThanEqualTo(int numberOfItems, string msg)
        {
            Assert.IsTrue(CollectionToTest.Count >= numberOfItems, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsLessThan(int numberOfItems)
        {
            Assert.IsTrue(CollectionToTest.Count < numberOfItems);
            return this;
            throw new NotImplementedException();
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsLessThan(int numberOfItems, string msg)
        {
            Assert.IsTrue(CollectionToTest.Count < numberOfItems, msg);
            return this;
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsLessThanOrEqualTo(int numberOfItems)
        {
            Assert.IsTrue(CollectionToTest.Count <= numberOfItems);
            return this;
            throw new NotImplementedException();
        }

        IFluentCollectionTest IFluentCollectionTest.CountIsLessThanEqualTo(int numberOfItems, string msg)
        {
            Assert.IsTrue(CollectionToTest.Count <= numberOfItems, msg);
            return this;
        }
        #endregion
    }
}
