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
		public void StringSourceConstructorTest()
		{
			string[] values = new string[]{"One", "Two", "Three"}; // TODO: Initialize to an appropriate value
			StringSource target = new StringSource(values);

			var tokens = target.GetTokenIterator().ToList();

			Verify.That(tokens).IsACollectionThat().Count().IsEqualTo(3);

			int i = 0;
			foreach (var token in tokens)
				Verify.That(token.Value).IsAStringThat().IsEqualTo(values[i++]);

		}

		/// <summary>
		///A test for StringSource Constructor
		///</summary>
		[TestMethod()]
		public void StringSourceConstructorTest1()
		{
			string value = "Test"; // TODO: Initialize to an appropriate value
			StringSource target = new StringSource(value);

			var tokens = target.GetTokenIterator().ToList();

			Verify.That(tokens).IsACollectionThat().Count().IsEqualTo(1);
			Verify.That(tokens.First().Value).IsAStringThat().IsEqualTo(value);
		}

		/// <summary>
		///A test for GetTokenIterator
		///</summary>
		[TestMethod()]
		public void GetTokenIteratorTest()
		{
			IEnumerable<string> values = null; // TODO: Initialize to an appropriate value
			StringSource target = new StringSource(values); // TODO: Initialize to an appropriate value
			IEnumerable<Token> expected = null; // TODO: Initialize to an appropriate value
			IEnumerable<Token> actual;
			actual = target.GetTokenIterator();
			Assert.AreEqual(expected, actual);
			Assert.Inconclusive("Verify the correctness of this test method.");
		}
	}
}
