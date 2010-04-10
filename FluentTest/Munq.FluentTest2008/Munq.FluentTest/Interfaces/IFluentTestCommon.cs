#region Copyright Notice
// Copyright 2010 by Matthew Dennis
#endregion

namespace Munq.FluentTest
{
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
        /// Indicates that the assertion cannot be verified. 
        /// </summary>
        void Inconclusive();

        /// <summary>
        /// Verifies that the specified object is null (Nothing in Visual Basic). 
        /// The assertion fails if it is not null (Nothing in Visual Basic). 
        /// </summary>
        void IsNull();
    }
}
