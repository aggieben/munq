using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using Munq.Search.Documents;
using Munq.Search.Documents.FieldSources;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Munq.FluentTest;

namespace Munq.Search.UnitTests.Documents
{
	/// <summary>
	/// Summary description for DocumentTest
	/// </summary>
	[TestClass]
	public class DocumentTest
	{
		public DocumentTest()
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
		public void AddingFieldIncreasesFieldCount()
		{
			var doc = new Document();
			var fi = new FieldOptions(StoreOptions.STORED, IndexOptions.Analyzed, TermVectorOptions.STORED);
			var field = new Field("Test",fi, new StringSource("Test Field Source"));
			doc.Add(field);
			Verify.That(doc.Count).IsEqualTo(1);
		}

		[TestMethod]
		public void FindByNameReturnsFirstAdded()
		{
			var doc = new Document();
			var fi = new FieldOptions(StoreOptions.STORED, IndexOptions.Analyzed, TermVectorOptions.STORED);
			var field1 = new Field("Test", fi, new StringSource("Test Field Source1"));
			var field2 = new Field("Test", fi, new StringSource("Test Field Source2"));
			doc.Add(field1);
			doc.Add(field2);

			Verify.That(doc.Count).Equals(2);
			Verify.That(doc.FindByName("Test")).IsTheSameObjectAs(field1);
		}

		[TestMethod]
		public void FindByNameReturnsNullIfNoMatchingField()
		{
			var doc = new Document();
			var result1 = doc.FindByName("No Found");
			var fi = new FieldOptions(StoreOptions.STORED, IndexOptions.Analyzed, TermVectorOptions.STORED);
			var field1 = new Field("Test", fi, new StringSource("Test Field Source"));
			doc.Add(field1);
			var result2 = doc.FindByName("No Found");

			Verify.That(result1).IsNull();
			Verify.That(result2).IsNull();
		}

		[TestMethod]
		public void FindAllByNameReturnsOnlyExpectedFields()
		{
			var doc = new Document();
			var fi = new FieldOptions(StoreOptions.STORED, IndexOptions.Analyzed, TermVectorOptions.STORED);
			var dummyField = new Field("Dummy", fi, new StringSource("Dummy Field"));

			var field1 = new Field("Test", fi, new StringSource("Test Field Source1"));
			var field2 = new Field("Test", fi, new StringSource("Test Field Source2"));

			doc.Add(dummyField);
			doc.Add(field1);
			doc.Add(field2);

			var result = doc.FindAllByName("Test");
			Verify.That(doc.Count).Equals(3);
			Verify.That(result).IsACollectionThat().Count().IsEqualTo(2);
		}

		[TestMethod]
		public void FindByNameReturnEmptyListIfNoMatchingField()
		{
			var doc = new Document();
			var result1 = doc.FindAllByName("Not Found");
			var fi = new FieldOptions(StoreOptions.STORED, IndexOptions.Analyzed, TermVectorOptions.STORED);
			var field1 = new Field("Test", fi, new StringSource("Test Field Source1"));
			doc.Add(field1);
			var result2 = doc.FindAllByName("Not Found");

			Verify.That(result1).IsACollectionThat().Count().IsEqualTo(0);
			Verify.That(result2).IsACollectionThat().Count().IsEqualTo(0);
		}


	}
}
