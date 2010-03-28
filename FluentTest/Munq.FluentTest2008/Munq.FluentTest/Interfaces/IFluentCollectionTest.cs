using System;
using System.Collections;

namespace Munq.FluentTest
{
    /// <summary>
    /// Methods for testing collections.
    /// </summary>
    public interface IFluentCollectionTest : IFluentTestCommon
    {
        /// <summary>
        /// Specifies a message to use on failure.
        /// </summary>
        /// <param name="msg"The message to use.</param>
        IFluentCollectionTest WithFailureMessage(string msg);

        /// <summary>
        /// The assertion fails if the collection is null.
        /// </summary>
        IFluentCollectionTest IsNotNull();

        /// <summary>
        /// The assertion fails if the type of the collection is not the specified type.
        /// </summary>
        /// <param name="type">The Type to compare against.</param>
        IFluentCollectionTest IsAnInstanceOfType(Type type);

        /// <summary>
        /// The assertion fails if the type of the collection is the specified type.
        /// </summary>
        /// <param name="type">The Type to compare against.</param>
        IFluentCollectionTest IsNotAnInstanceOfType(Type type);

        /// <summary>
        /// The assertion fails if the collection is not the nosame collection as the specified collection.
        /// </summary>
        /// <param name="objToCompare">The object to compare.</param>
        IFluentCollectionTest IsTheSameCollectionAs(ICollection objToCompare);

        /// <summary>
        /// The assertion fails if the collection is the same collection as the specified collection.
        /// </summary>
        /// <param name="objToCompare">The object to compare.</param>
        IFluentCollectionTest IsNotTheSameCollectionAs(ICollection objToCompare);

        /// <summary>
        /// The assertion fails if any of the objects in the collection are not instance of, or derived from, the specified type.
        /// </summary>
        /// <param name="type">The Type to compare against.</param>
        IFluentCollectionTest AllItemsAreInstancesOfType(Type type);

        /// <summary>
        /// The assertion fails if any of the objects in the collection are null.
        /// </summary>
        IFluentCollectionTest AllItemsAreNotNull();

        /// <summary>
        /// The assertion fails if any of the any objects in the collection are duplicates.
        /// </summary>
        IFluentCollectionTest AllItemsAreUnique();

        /// <summary>
        /// The assertion fails if the collections are not equal.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsEqualTo(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the collections are equal.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsNotEqualTo(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the collections are not eqivalent
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsEquivalentTo(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the collections are eqivalent
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsNotEquivalentTo(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if  the collection does not contain the specified object.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest Contains(object ObjectToCompare);

        /// <summary>
        /// The assertion fails if  the collection does not contain the specified object.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest DoesNotContain(object ObjectToCompare);

        /// <summary>
        /// The assertion fails if the type of the collection is  a subset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsNotASubsetOf(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the type of the collection is not a subset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsASubsetOf(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the type of the collection is a superset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsNotASupersetOf(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the type of the collection is not a superset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsASupersetOf(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is not equal to the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        IFluentCollectionTest CountIsEqualTo(int numberOfItems);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is equal to  the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        IFluentCollectionTest CountIsNotEqualTo(int numberOfItems);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is not greater than  the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        IFluentCollectionTest CountIsGreaterThan(int numberOfItems);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is less  than  the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        IFluentCollectionTest CountIsGreaterThanOrEqualTo(int numberOfItems);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is not less  than  the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        IFluentCollectionTest CountIsLessThan(int numberOfItems);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is greater than  the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        IFluentCollectionTest CountIsLessThanOrEqualTo(int numberOfItems);
    }
}
