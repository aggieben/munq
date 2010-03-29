using System;
using System.Collections;

namespace Munq.FluentTest
{
    /// <summary>
    /// Methods for testing collections.
    /// </summary>
    public interface IFluentTestCollection : IFluentTestCommon
    {
        /// <summary>
        /// Specifies a message to use on failure.
        /// </summary>
        /// <param name="msg"The message to use.</param>
        IFluentTestCollection WithFailureMessage(string msg);

        /// <summary>
        /// The assertion fails if the collection is null.
        /// </summary>
        IFluentTestCollection IsNotNull();

        /// <summary>
        /// The assertion fails if the type of the collection is not the specified type.
        /// </summary>
        /// <param name="type">The Type to compare against.</param>
        IFluentTestCollection IsAnInstanceOfType(Type type);

        /// <summary>
        /// The assertion fails if the type of the collection is the specified type.
        /// </summary>
        /// <param name="type">The Type to compare against.</param>
        IFluentTestCollection IsNotAnInstanceOfType(Type type);

        /// <summary>
        /// The assertion fails if the collection is not the nosame collection as the specified collection.
        /// </summary>
        /// <param name="objToCompare">The object to compare.</param>
        IFluentTestCollection IsTheSameCollectionAs(ICollection objToCompare);

        /// <summary>
        /// The assertion fails if the collection is the same collection as the specified collection.
        /// </summary>
        /// <param name="objToCompare">The object to compare.</param>
        IFluentTestCollection IsNotTheSameCollectionAs(ICollection objToCompare);

        /// <summary>
        /// The assertion fails if any of the objects in the collection are not instance of,
        /// or derived from, the specified type.
        /// </summary>
        /// <param name="type">The Type to compare against.</param>
        IFluentTestCollection AllItemsAreInstancesOfType(Type type);

        /// <summary>
        /// The assertion fails if any of the objects in the collection are null.
        /// </summary>
        IFluentTestCollection AllItemsAreNotNull();

        /// <summary>
        /// The assertion fails if any of the any objects in the collection are duplicates.
        /// </summary>
        IFluentTestCollection AllItemsAreUnique();

        /// <summary>
        /// The assertion fails if the collections are not equal.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentTestCollection IsEqualTo(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the collections are equal.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentTestCollection IsNotEqualTo(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the collections are not eqivalent
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentTestCollection IsEquivalentTo(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the collections are eqivalent
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentTestCollection IsNotEquivalentTo(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if  the collection does not contain the specified object.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentTestCollection Contains(object ObjectToCompare);

        /// <summary>
        /// The assertion fails if  the collection does not contain the specified object.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentTestCollection DoesNotContain(object ObjectToCompare);

        /// <summary>
        /// The assertion fails if the type of the collection is  a subset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentTestCollection IsNotASubsetOf(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the type of the collection is not a subset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentTestCollection IsASubsetOf(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the type of the collection is a superset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentTestCollection IsNotASupersetOf(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the type of the collection is not a superset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentTestCollection IsASupersetOf(ICollection ObjectToCompare);
        
        /// <summary>
        /// Returns an interface to validate the number of items in the collection.
        /// </summary>
        /// <returns></returns>
        IFluentTestCollectionCount Count();

    }
}
