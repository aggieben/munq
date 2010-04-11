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

        private void CollectionNullCheck(ICollection collectionToCompare)
        {
            if (collectionToCompare == null)
                FailWithDefaultMessage("can not be compared to [null]");
        }
        
        #region CollectionAssert members
        IFluentTestCollection IFluentTestCollection.WithFailureMessage(string msg)
        {
            ErrorMessage = msg;
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsTheSameCollectionAs(ICollection collectionToCompare)
        {
            CollectionNullCheck(collectionToCompare);  
                          
            if (!Object.ReferenceEquals(CollectionToTest, collectionToCompare))
                FailWithObjectToCompareDefaultMessage("should be the same collection as",
                    collectionToCompare);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotTheSameCollectionAs(ICollection collectionToCompare)
        {
            CollectionNullCheck(collectionToCompare);                
                
            if (Object.ReferenceEquals(CollectionToTest, collectionToCompare))
                FailWithObjectToCompareDefaultMessage("should not be the same collection as",
                    collectionToCompare);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsAnInstanceOfType(Type type)
        {
            if (type == null)
                FailWithDefaultMessage("can not be compared to a type of [null]");
                
            if (!_IsAnInstanceOf(type))
                FailWithTypeDefaultMessage("should be of type", type);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotAnInstanceOfType(Type type)
        {
            if (type == null)
                FailWithDefaultMessage("can not be compared to a type of [null]");
                
            if (!_IsNotAnInstanceOf(type))
                FailWithTypeDefaultMessage("should not be of type", type);
            return this;
        }
    
        IFluentTestCollection IFluentTestCollection.AllItemsAreInstancesOfType(Type type)
        {
            foreach (var item in CollectionToTest )
                if (!(item == null && type == null))
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
                        FailWithDefaultMessage("all items should be unique");
                
                itemList.Add(itemToTest);
            }
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsEqualTo(ICollection collectionToCompare)
        {
            CollectionNullCheck(collectionToCompare);                

            if (CollectionToTest.Count != collectionToCompare.Count)
                FailWithObjectToCompareDefaultMessage("should be the same length as", collectionToCompare);
                
            var iterator = collectionToCompare.GetEnumerator();
            
            foreach(var item in CollectionToTest)
            {
                if(!(iterator.MoveNext() && iterator.Current.Equals(item)))
                    FailWithObjectToCompareDefaultMessage("should be equal to", collectionToCompare);
            }
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotEqualTo(ICollection collectionToCompare)
        {
            CollectionNullCheck(collectionToCompare);                
                
            if (CollectionToTest.Count != collectionToCompare.Count)
                return this;
    
            var iterator = collectionToCompare.GetEnumerator();

            foreach (var item in CollectionToTest)
            {
                if (!(iterator.MoveNext() && iterator.Current.Equals(item)))
                    return this;
            }
            FailWithObjectToCompareDefaultMessage("should not be equal to", collectionToCompare);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsEquivalentTo(ICollection collectionToCompare)
        {
            CollectionNullCheck(collectionToCompare);
            CollectionAssert.AreEquivalent(CollectionToTest, collectionToCompare);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotEquivalentTo(ICollection collectionToCompare)
        {
            CollectionNullCheck(collectionToCompare);
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
            CollectionNullCheck(collectionToCompare);
            CollectionAssert.IsSubsetOf(CollectionToTest, collectionToCompare);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotASubsetOf(ICollection collectionToCompare)
        {
            CollectionNullCheck(collectionToCompare);
            CollectionAssert.IsNotSubsetOf(CollectionToTest, collectionToCompare);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsASupersetOf(ICollection collectionToCompare)
        {
            CollectionNullCheck(collectionToCompare);
            CollectionAssert.IsSubsetOf(collectionToCompare, CollectionToTest);
            return this;
        }

        IFluentTestCollection IFluentTestCollection.IsNotASupersetOf(ICollection collectionToCompare)
        {
            CollectionNullCheck(collectionToCompare);
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
