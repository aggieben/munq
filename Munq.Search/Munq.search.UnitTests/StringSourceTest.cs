using Munq.Search.Documents.FieldSources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;
using System;
using System.Collections.Generic;
using Munq.Search.Documents;
using System.Linq;

namespace Munq.Search.UnitTests
{
	
	
	/// <summary>
	///This is a test class for StringSourceTest and is intended
	///to contain all StringSourceTest Unit Tests
	///</summary>
	[TestClass()]
	public class StringSourceTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///A test for StringSource Constructor
		///</summary>
		[TestMethod()]
		public void StringSourceWithThreeStringsHasThreeTokens()
		{
			string[] values = new string[]{"One", "Two", "Three"}; // TODO: Initialize to an appropriate value
			StringSource target = new StringSource(values);

			var tokens = target.Tokens.ToList();

			Verify.That(tokens).IsACollectionThat().Count().IsEqualTo(3);

			int i = 0;
			foreach (var token in tokens)
			{
				Verify.That(token.Value).IsAStringThat().IsEqualTo(values[i]);
				Verify.That(token.Offset).IsEqualTo(i);
				i++;
			}

		}

		/// <summary>
		///A test for StringSource Constructor
		///</summary>
		[TestMethod()]
		public void StringSourceWithSingleStringHasOneToken()
		{
			string value = "Test"; // TODO: Initialize to an appropriate value
			StringSource target = new StringSource(value);

			var tokens = target.Tokens.ToList();

			Verify.That(tokens).IsACollectionThat().Count().IsEqualTo(1);

			Token token = tokens.First();
			Verify.That(token.Value).IsAStringThat().IsEqualTo(value);
			Verify.That(token.Offset).IsEqualTo(0);
		}

		/// <summary>
		///A test for GetTokenIterator
		///</summary>
		[TestMethod()]
		public void StringSourceWithNoStringsHasNoTokens()
		{
			IEnumerable<string> values = null; // TODO: Initialize to an appropriate value
			StringSource target = new StringSource(values); // TODO: Initialize to an appropriate value

			var result = target.Tokens;
			Verify.That(result).IsNotNull()
							   .IsACollectionThat()
							   .IsAnInstanceOfType(typeof(IEnumerable<Token>))
							   .Count().IsEqualTo(0);
		}
	}
}
