using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Munq.FluentTest
{
    public partial class FluentTestObject : IFluentTestCollection
    {
        private ICollection CollectionToTest { get { return ObjectToTest as ICollection; } }

        #region CollectionAssert members
        IFluentTestCollection IFluentTestCollection.WithFailureMessage(string msg)
        {
            ErrorMessage = msg;
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsTheSameCollectionAs(ICollection objectToCompare)
        {
            if (Object.ReferenceEquals(CollectionToTest, objectToCompare))
                Verify.Fail();
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotTheSameCollectionAs(ICollection objectToCompare)
        {
            if (!Object.ReferenceEquals(CollectionToTest, objectToCompare))
                Verify.Fail();
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsAnInstanceOfType(Type type)
        {
            if (!_IsAnInstanceOf(type))
                Verify.Fail();
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotAnInstanceOfType(Type type)
        {
            if (_IsNotAnInstanceOf(type))
                Verify.Fail();
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotNull()
        {
            if ( CollectionToTest == null)
                Verify.Fail();
            return this;
        }
      
        IFluentTestCollection IFluentTestCollection.AllItemsAreInstancesOfType(Type type)
        {
            foreach (var item in CollectionToTest )
                Verify.That(item).IsAnInstanceOfType(type);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.AllItemsAreNotNull()
        {
            foreach(var item in CollectionToTest )
                Verify.That(item).IsNotNull();
            return this;
        }

        IFluentTestCollection IFluentTestCollection.AllItemsAreUnique()
        {
            var itemList = new List<object>();
            foreach (var itemToTest in CollectionToTest)
            {
                foreach(var itemTested in itemList)
                    if (itemToTest.Equals(itemTested))
                        Verify.Fail();
                
                itemList.Add(itemToTest);
            }
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsEqualTo(ICollection ObjectToCompare)
        {
            if (ObjectToCompare == null)
                Verify.Fail();

            if (CollectionToTest.Count != ObjectToCompare.Count)
                Verify.Fail();
                
            var iterator = ObjectToCompare.GetEnumerator();
            
            foreach(var item in CollectionToTest)
            {
                if(!(iterator.MoveNext() && iterator.Current.Equals(item)))
                    Verify.Fail();
            }
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotEqualTo(ICollection ObjectToCompare)
        {
            if (ObjectToCompare == null ||
                CollectionToTest.Count != ObjectToCompare.Count)
                return this;
    
            var iterator = ObjectToCompare.GetEnumerator();

            foreach (var item in CollectionToTest)
            {
                if (!(iterator.MoveNext() && iterator.Current.Equals(item)))
                    return this;
            }
            Verify.Fail();
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsEquivalentTo(ICollection ObjectToCompare)
        {
            CollectionAssert.AreEquivalent(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotEquivalentTo(ICollection ObjectToCompare)
        {
            CollectionAssert.AreNotEquivalent(CollectionToTest, ObjectToCompare);
            return this;
        }

        private bool _Contains(object ObjectToCompare)
        {
            bool found = false;
            foreach (var item in CollectionToTest)
                if (Object.ReferenceEquals(item, ObjectToCompare))
                {
                    found = true;
                    break;
                }
            return found;
        }
        IFluentTestCollection IFluentTestCollection.Contains(object ObjectToCompare)
        {
            if (!_Contains(ObjectToCompare))
                Verify.Fail();
                
            return this;
        }

        IFluentTestCollection IFluentTestCollection.DoesNotContain(object ObjectToCompare)
        {
            if (_Contains(ObjectToCompare))
                Verify.Fail();

            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsASubsetOf(ICollection ObjectToCompare)
        {
            CollectionAssert.IsSubsetOf(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotASubsetOf(ICollection ObjectToCompare)
        {
            CollectionAssert.IsNotSubsetOf(CollectionToTest, ObjectToCompare);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsASupersetOf(ICollection ObjectToCompare)
        {
            CollectionAssert.IsSubsetOf(ObjectToCompare, CollectionToTest);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotASupersetOf(ICollection ObjectToCompare)
        {
            CollectionAssert.IsNotSubsetOf(ObjectToCompare, CollectionToTest);
            return this;
        }
        
        IFluentTestCollectionCount IFluentTestCollection.Count()
        {
            return this;
        }
        #endregion
    }
}
