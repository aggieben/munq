#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

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

        IFluentTestCollection IFluentTestCollection.IsTheSameCollectionAs(ICollection collectionToCompare)
        {
            if (collectionToCompare == null)
                Verify.Fail();
                
            if (!Object.ReferenceEquals(CollectionToTest, collectionToCompare))
                Verify.Fail();
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotTheSameCollectionAs(ICollection collectionToCompare)
        {
            if (collectionToCompare == null)
                Verify.Fail();
                
            if (Object.ReferenceEquals(CollectionToTest, collectionToCompare))
                Verify.Fail();
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsAnInstanceOfType(Type type)
        {
            if (type == null)
                Verify.Fail();
                
            if (!_IsAnInstanceOf(type))
                Verify.Fail();
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotAnInstanceOfType(Type type)
        {
            if (type == null)
                Verify.Fail();
                
            if (!_IsNotAnInstanceOf(type))
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

        IFluentTestCollection IFluentTestCollection.IsEqualTo(ICollection collectionToCompare)
        {
            if (collectionToCompare == null)
                Verify.Fail();

            if (CollectionToTest.Count != collectionToCompare.Count)
                Verify.Fail();
                
            var iterator = collectionToCompare.GetEnumerator();
            
            foreach(var item in CollectionToTest)
            {
                if(!(iterator.MoveNext() && iterator.Current.Equals(item)))
                    Verify.Fail();
            }
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotEqualTo(ICollection collectionToCompare)
        {
            if (collectionToCompare == null)
                Verify.Fail();
                
            if (CollectionToTest.Count != collectionToCompare.Count)
                return this;
    
            var iterator = collectionToCompare.GetEnumerator();

            foreach (var item in CollectionToTest)
            {
                if (!(iterator.MoveNext() && iterator.Current.Equals(item)))
                    return this;
            }
            Verify.Fail();
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsEquivalentTo(ICollection collectionToCompare)
        {
            CollectionAssert.AreEquivalent(CollectionToTest, collectionToCompare);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotEquivalentTo(ICollection collectionToCompare)
        {
            CollectionAssert.AreNotEquivalent(CollectionToTest, collectionToCompare);
            return this;
        }

        private bool _Contains(object objectToCompare)
        {
            bool found = false;
            foreach (var item in CollectionToTest)
                if (Object.ReferenceEquals(item, objectToCompare))
                {
                    found = true;
                    break;
                }
            return found;
        }
        IFluentTestCollection IFluentTestCollection.Contains(object objectToCompare)
        {
            if (!_Contains(objectToCompare))
                Verify.Fail();
                
            return this;
        }

        IFluentTestCollection IFluentTestCollection.DoesNotContain(object objectToCompare)
        {
            if (_Contains(objectToCompare))
                Verify.Fail();

            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsASubsetOf(ICollection collectionToCompare)
        {
            CollectionAssert.IsSubsetOf(CollectionToTest, collectionToCompare);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotASubsetOf(ICollection collectionToCompare)
        {
            CollectionAssert.IsNotSubsetOf(CollectionToTest, collectionToCompare);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsASupersetOf(ICollection collectionToCompare)
        {
            CollectionAssert.IsSubsetOf(collectionToCompare, CollectionToTest);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotASupersetOf(ICollection collectionToCompare)
        {
            CollectionAssert.IsNotSubsetOf(collectionToCompare, CollectionToTest);
            return this;
        }
        
        IFluentTestCollectionCount IFluentTestCollection.Count()
        {
            return this;
        }
        #endregion
    }
}
