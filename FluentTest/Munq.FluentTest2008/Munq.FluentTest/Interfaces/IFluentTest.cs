using System;

namespace Munq.FluentTest
{
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
}
