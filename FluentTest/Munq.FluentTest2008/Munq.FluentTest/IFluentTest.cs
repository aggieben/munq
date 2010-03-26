using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace Munq.FluentTest
{
    public interface IFluentTestProvider
    {
        void Fail();
        void Fail(string msg);
        
        void InConclusive();
        void InConclusive(string msg);
        
    }

    /// <summary>
    /// Methods that are terminating checks regardless of the type of the object under test.
    /// </summary>
    /// <remarks>The methods all return null, instead of this, as no other method  
    /// can be meaningfully applied to an object after the execution of these methods.
    /// </remarks>
    public interface IFluentTestCommon
    {
        /// <summary>
        /// Fails the assertion without checking any conditions.
        /// </summary>
        void Fail();

        /// <summary>
        /// Fails the assertion without checking any conditions. Displays a message. 
        /// </summary>
        /// <param name="msg">The message to display.</param>
        void Fail(string msg);

        /// <summary>
        /// Indicates that the assertion cannot be verified. 
        /// </summary>
        void Inconclusive();

        /// <summary>
        /// Indicates that the assertion can not be verified. Displays a message. 
        /// </summary>
        /// <param name="msg">The message to display.</param>
        void Inconclusive(string msg);

        /// <summary>
        /// Verifies that the specified object is null (Nothing in Visual Basic). 
        /// The assertion fails if it is not null (Nothing in Visual Basic). 
        /// </summary>
        void IsNull();

        /// <summary>
        /// The assertion fails if it is not null (Nothing in Visual Basic). 
        /// </summary>
        /// <param name="msg">The message to display.</param>
        void IsNull(string msg);
    }

    /// <summary>
    /// Method to assert conditions about the object under test.
    /// </summary>
    public interface IFluentTest : IFluentTestCommon
    {
        /// <summary>
        /// The assertion fails if the object under test is null.
        /// </summary>
        IFluentTest IsNotNull();

        /// <summary>
        /// The assertion fails if the object under test is null.
        /// </summary>
        /// <param name="msg">The message to display.</param>
        IFluentTest IsNotNull(string msg);

        /// <summary>
        /// The assertion fails if the object under test is not an instance of, or derived from, the specified type.
        /// </summary>
        /// <param name="type">The Type to test for.</param>
        IFluentTest IsAnInstanceOfType(Type type);

        /// <summary>
        /// The assertion fails if the object under test is not an instance of, or derived from, the specified type.
        /// </summary>
        /// <param name="type">The Type to test for.</param>
        /// <param name="msg">The message to display.</param>
        IFluentTest IsAnInstanceOfType(Type type, string msg);

        /// <summary>
        /// The assertion fails if the object under test is an instance of, or derived from, the specified type.
        /// </summary>
        /// <param name="type">The Type to test for.</param>
        IFluentTest IsNotAnInstanceOfType(Type type);

        /// <summary>
        /// The assertion fails if the object under test is an instance of, or derived from, the specified type.
        /// </summary>
        /// <param name="type">The Type to test for.</param>
        /// <param name="msg">The message to display.</param>
        IFluentTest IsNotAnInstanceOfType(Type type, string msg);

        /// <summary>
        /// The assertion fails if the object under test is not the same object as the specified object.
        /// </summary>
        /// <param name="objToCompare">The object to campare against the object under test.</param>
        IFluentTest IsTheSameObjectAs(object objToCompare);

        /// <summary>
        /// The assertion fails if the object under test is not the same object as the specified object.
        /// </summary>
        /// <param name="objToCompare">The object to campare against the object under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentTest IsTheSameObjectAs(object objToCompare, string msg);

        /// <summary>
        /// The assertion fails if the object under test is the same object as the specified object.
        /// </summary>
        /// <param name="objToCompare">The object to campare against the object under test.</param>
        IFluentTest IsNotTheSameObjectAs(object objToCompare);

        /// <summary>
        /// The assertion fails if the object under test is the same object as the specified object.
        /// </summary>
        /// <param name="objToCompare">The object to campare against the object under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentTest IsNotTheSameObjectAs(object objToCompare, string msg);

        /// <summary>
        /// The assertion fails if the object under test is not equal to the specified value.
        /// </summary>
        /// <param name="objToCompare">The object to campare against the object under test.</param>
        IFluentTest IsEqualTo(object objToCompare);

        /// <summary>
        /// The assertion fails if the object under test is not equal to the specified value.
        /// </summary>
        /// <param name="objToCompare">The object to campare against the object under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentTest IsEqualTo(object objToCompare, string msg);

        /// <summary>
        /// The assertion fails if the object under test is equal to the specified value.
        /// </summary>
        /// <param name="objToCompare">The object to campare against the object under test.</param>
        IFluentTest IsNotEqualTo(object objToCompare);

        /// <summary>
        /// The assertion fails if the object under test is equal to the specified value.
        /// </summary>
        /// <param name="objToCompare">The object to campare against the object under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentTest IsNotEqualTo(object objToCompare, string msg);

        /// <summary>
        /// The assertion fails if the object under test is not true
        /// </summary>
        IFluentTest IsTrue();

        /// <summary>
        /// The assertion fails if the object under test is not true
        /// </summary>
        /// <param name="msg">The message to display.</param>
        IFluentTest IsTrue(string msg);

        /// <summary>
        /// The assertion fails if the object under test is not false.
        /// </summary>
        IFluentTest IsFalse();

        /// <summary>
        /// The assertion fails if the object under test is not false.
        /// </summary>
        /// <param name="msg">The message to display.</param>
        IFluentTest IsFalse(string msg);

        /// <summary>
        /// The assertion fails if the object under test is not a collection.
        /// </summary>
        IFluentCollectionTest IsACollection();

        /// <summary>
        /// The assertion fails if the object under test is not a collection.
        /// </summary>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest IsACollection(string msg);

        /// <summary>
        /// The assertion fails if the object under test is not a string.
        /// </summary>
        IFluentStringTest IsAString();

        /// <summary>
        /// The assertion fails if the object under test is not a string.
        /// </summary>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest IsAString(string msg);
    }

    /// <summary>
    /// Methods for testing collections.
    /// </summary>
    public interface IFluentCollectionTest : IFluentTestCommon
    {
        /// <summary>
        /// The assertion fails if the collection is null.
        /// </summary>
        IFluentCollectionTest IsNotNull();

        /// <summary>
        /// The assertion fails if the collection is null.
        /// </summary>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest IsNotNull(string msg);

        /// <summary>
        /// The assertion fails if the type of the collection is not the specified type.
        /// </summary>
        /// <param name="type">The Type to compare against.</param>
        IFluentCollectionTest IsAnInstanceOfType(Type type);

        /// <summary>
        /// The assertion fails if the type of the collection is not the specified type.
        /// </summary>
        /// <param name="type">The Type to compare against.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest IsAnInstanceOfType(Type type, string msg);

        /// <summary>
        /// The assertion fails if the type of the collection is the specified type.
        /// </summary>
        /// <param name="type">The Type to compare against.</param>
        IFluentCollectionTest IsNotAnInstanceOfType(Type type);

        /// <summary>
        /// The assertion fails if the type of the collection is the specified type.
        /// </summary>
        /// <param name="type">The Type to compare against.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest IsNotAnInstanceOfType(Type type, string msg);


        /// <summary>
        /// The assertion fails if the collection is not the nosame collection as the specified collection.
        /// </summary>
        /// <param name="objToCompare">The object to compare.</param>
        IFluentCollectionTest IsTheSameCollectionAs(ICollection objToCompare);

        /// <summary>
        /// The assertion fails if the collection is not the nosame collection as the specified collection.
        /// </summary>
        /// <param name="objToCompare">The object to compare.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest IsTheSameCollectionAs(ICollection objToCompare, string msg);

        /// <summary>
        /// The assertion fails if the collection is the same collection as the specified collection.
        /// </summary>
        /// <param name="objToCompare">The object to compare.</param>
        IFluentCollectionTest IsNotTheSameCollectionAs(ICollection objToCompare);

        /// <summary>
        /// The assertion fails if the collection is the same collection as the specified collection.
        /// </summary>
        /// <param name="objToCompare">The object to compare.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest IsNotTheSameCollectionAs(ICollection objToCompare, string msg);

        /// <summary>
        /// The assertion fails if any of the objects in the collection are not instance of, or derived from, the specified type.
        /// </summary>
        /// <param name="type">The Type to compare against.</param>
        IFluentCollectionTest AllItemsAreInstancesOfType(Type type);

        /// <summary>
        /// The assertion fails if any of the objects in the collection are not instance of, or derived from, the specified type.
        /// </summary>
        /// <param name="type">The Type to compare against.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest AllItemsAreInstancesOfType(Type type, string msg);

        /// <summary>
        /// The assertion fails if any of the objects in the collection are null.
        /// </summary>
        IFluentCollectionTest AllItemsAreNotNull();

        /// <summary>
        /// The assertion fails if any of the objects in the collection are null.
        /// </summary>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest AllItemsAreNotNull(string msg);

        /// <summary>
        /// The assertion fails if any of the any objects in the collection are duplicates.
        /// </summary>
        IFluentCollectionTest AllItemsAreUnique();

        /// <summary>
        /// The assertion fails if any of the any objects in the collection are duplicates.
        /// </summary>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest AllItemsAreUnique(string msg);

        /// <summary>
        /// The assertion fails if the collections are not equal.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsEqualTo(ICollection ObjectToCompare);
        /// <summary>
        /// The assertion fails if the collections are not equal.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest IsEqualTo(ICollection ObjectToCompare, string msg);

        /// <summary>
        /// The assertion fails if the collections are equal.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsNotEqualTo(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the collections are equal.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest IsNotEqualTo(ICollection ObjectToCompare, string msg);

        /// <summary>
        /// The assertion fails if the collections are not eqivalent
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsEquivalentTo(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the collections are not eqivalent
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest IsEquivalentTo(ICollection ObjectToCompare, string msg);

        /// <summary>
        /// The assertion fails if the collections are eqivalent
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsNotEquivalentTo(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the collections are eqivalent
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest IsNotEquivalentTo(ICollection ObjectToCompare, string msg);

        /// <summary>
        /// The assertion fails if  the collection does not contain the specified object.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest Contains(object ObjectToCompare);

        /// <summary>
        /// The assertion fails if any of the any objects in the collection are duplicates.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest Contains(object ObjectToCompare, string msg);

        /// <summary>
        /// The assertion fails if  the collection does not contain the specified object.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest DoesNotContain(object ObjectToCompare);

        /// <summary>
        /// The assertion fails if  the collection does not contain the specified object.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest DoesNotContain(object ObjectToCompare, string msg);

        /// <summary>
        /// The assertion fails if the type of the collection is  a subset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsNotASubsetOf(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the type of the collection is  a subset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest IsNotASubsetOf(ICollection ObjectToCompare, string msg);

        /// <summary>
        /// The assertion fails if the type of the collection is not a subset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsASubsetOf(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the type of the collection is not a subset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest IsASubsetOf(ICollection ObjectToCompare, string msg);

        /// <summary>
        /// The assertion fails if the type of the collection is a superset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsNotASupersetOf(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the type of the collection is a superset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest IsNotASupersetOf(ICollection ObjectToCompare, string msg);

        /// <summary>
        /// The assertion fails if the type of the collection is not a superset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        IFluentCollectionTest IsASupersetOf(ICollection ObjectToCompare);

        /// <summary>
        /// The assertion fails if the type of the collection is not a superset of the specified collection.
        /// </summary>
        /// <param name="ObjectToCompare">The object to compare.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest IsASupersetOf(ICollection ObjectToCompare, string msg);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is not equal to the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        IFluentCollectionTest CountIsEqualTo(int numberOfItems);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is not equal to the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest CountIsEqualTo(int numberOfItems, string msg);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is equal to  the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        IFluentCollectionTest CountIsNotEqualTo(int numberOfItems);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is equal to  the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest CountIsNotEqualTo(int numberOfItems, string msg);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is not greater than  the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        IFluentCollectionTest CountIsGreaterThan(int numberOfItems);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is not greater than  the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest CountIsGreaterThan(int numberOfItems, string msg);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is less  than  the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        IFluentCollectionTest CountIsGreaterThanOrEqualTo(int numberOfItems);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is less  than  the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest CountIsGreaterThanEqualTo(int numberOfItems, string msg);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is not less  than  the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        IFluentCollectionTest CountIsLessThan(int numberOfItems);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is not less  than  the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest CountIsLessThan(int numberOfItems, string msg);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is greater than  the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        IFluentCollectionTest CountIsLessThanOrEqualTo(int numberOfItems);

        /// <summary>
        /// The assertion fails if  the number of items in the  collection is greater than  the specified number.
        /// </summary>
        /// <param name="numberOfItems">The number to compare against.</param>
        /// <param name="msg">The message to display.</param>
        IFluentCollectionTest CountIsLessThanEqualTo(int numberOfItems, string msg);
    }

    /// <summary>
    /// Methods for testing string.
    /// </summary>
    public interface IFluentStringTest : IFluentTestCommon
    {
        /// <summary>
        /// The assertion fails if the string under test does not contain the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest Contains(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test does not contain the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest Contains(string stringToCompare, string msg);

        /// <summary>
        /// The assertion fails if the string under test contains the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest DoesNotContain(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test contains the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest DoesNotContain(string stringToCompare, string msg);

        /// <summary>
        /// The assertion fails if the string under test does not match the regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to apply to the string under test.</param>
        IFluentStringTest Matches(Regex regex);

        /// <summary>
        /// The assertion fails if the string under test does not match the regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to apply to the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest Matches(Regex regex, string msg);

        /// <summary>
        /// The assertion fails if the string under test matches the regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to apply to the string under test.</param>
        IFluentStringTest DoesNotMatch(Regex regex);

        /// <summary>
        /// The assertion fails if the string under test matches the regular expression.
        /// </summary>
        /// <param name="regex">The regular expression to apply to the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest DoesNotMatch(Regex regex, string msg);

        /// <summary>
        /// The assertion fails if the string under test does not start with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest StartsWith(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test does not start with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest StartsWith(string stringToCompare, string msg);

        /// <summary>
        /// The assertion fails if the string under test starts with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest DoesNotStartsWith(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test starts with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest DoesNotStartsWith(string stringToCompare, string msg);

        ///<summary>
        /// The assertion fails if the string under test does not end with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest EndsWith(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test does not end with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest EndsWith(string stringToCompare, string msg);

        ///<summary>
        /// The assertion fails if the string under test ends with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        IFluentStringTest DoesNotEndsWith(string stringToCompare);

        /// <summary>
        /// The assertion fails if the string under test ends with the specified string.
        /// </summary>
        /// <param name="stringToCompare">The string to test against the string under test.</param>
        /// <param name="msg">The message to display.</param>
        IFluentStringTest DoesNotEndsWith(string stringToCompare, string msg);
    }
}
