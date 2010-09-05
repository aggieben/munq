using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;

namespace Munq.Search.UnitTests.Index
{
	/// <summary>
	/// Summary description for IndexTest
	/// </summary>
	[TestClass]
	public class IndexTest
	{
		public IndexTest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

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
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		[TestMethod]
		public void GetDocsWithTerm()
		{
			uint docID1 = 1, docID2 = 2, docID3 = 3;
			string fieldName = "content";
			string term1 = "c#", term2 = "java";
			var index = new Munq.Search.Index.FieldIndex();

			index.AddDocFieldTermCount(fieldName, term1, docID1, 5);
			index.AddDocFieldTermCount(fieldName, term1, docID2, 3);
			index.AddDocFieldTermCount(fieldName, term2, docID3, 2);

			var docsWithTerm1 = index.GetDocsWithTermInField(term1, fieldName);
			Verify.That(docsWithTerm1).IsACollectionThat().Count().IsEqualTo(2);
			Verify.That(docsWithTerm1[docID1]).IsEqualTo(5U);
			Verify.That(docsWithTerm1[docID2]).IsEqualTo(3U);

			var docsWithTerm2 = index.GetDocsWithTermInField(term2, fieldName);
			Verify.That(docsWithTerm2).IsACollectionThat().Count().IsEqualTo(1);
			Verify.That(docsWithTerm2[docID3]).IsEqualTo(2U);
		}
	}
}
