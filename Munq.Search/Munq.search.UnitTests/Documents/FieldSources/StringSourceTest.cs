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
	///This is a test class for FieldSource<string>Test and is intended
	///to contain all FieldSource<string>Test Unit Tests
	///</summary>
	[TestClass()]
	public class StringFieldSourceTest
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
		///A test for FieldSource<string> Constructor
		///</summary>
		[TestMethod()]
		public void FieldSourceWithThreeStringsHasThreeTokens()
		{
			string[] values = new string[]{"One", "Two", "Three"}; // TODO: Initialize to an appropriate value
			FieldSource<string> target = new FieldSource<string>(values);

			var tokens = target.Tokens.ToList();

			Verify.That(tokens).IsACollectionThat().Count().IsEqualTo(3);

			int i = 0;
			foreach (var token in tokens)
			{
				Verify.That(token.Value).IsAStringThat().IsEqualTo(values[i]);
				Verify.That(token.Offset).IsEqualTo(0);
				i++;
			}
		}

		/// <summary>
		///A test for FieldSource<string> Constructor
		///</summary>
		[TestMethod()]
		public void FieldSourceWithSingleStringHasOneToken()
		{
			string value = "Test"; // TODO: Initialize to an appropriate value
			FieldSource<string> target = new FieldSource<string>(value);

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
		public void FieldSourceWithNoStringsHasNoTokens()
		{
			IEnumerable<string> values = null; // TODO: Initialize to an appropriate value
			FieldSource<string> target = new FieldSource<string>(values); // TODO: Initialize to an appropriate value

			var result = target.Tokens;
			Verify.That(result).IsNotNull()
							   .IsACollectionThat()
							   .IsAnInstanceOfType(typeof(IEnumerable<Token>))
							   .Count().IsEqualTo(0);
		}
	}
}
