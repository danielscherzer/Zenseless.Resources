using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Zenseless.Resources.Tests
{
	[TestClass()]
	public class EmbeddedResourceTests
	{
		[TestMethod()]
		public void EmbeddedResourceDirectoryTest()
		{
			Assert.ThrowsException<ArgumentException>(() => new EmbeddedResourceDirectory("Content"));
		}

		[TestMethod()]
		public void EnumerateResourcesTest()
		{
			EmbeddedResourceDirectory dir = new("Zenseless.Resources.Tests.Content");
			var res = dir.EnumerateResources();
			Assert.AreEqual(1, res.Count());
		}

		[TestMethod()]
		public void ExistsTest()
		{
			EmbeddedResourceDirectory dir = new("Zenseless.Resources.Tests.Content");
			Assert.IsTrue(dir.Exists("Test.txt"));
			Assert.IsFalse(dir.Exists("dataTest.txt"));
		}

		[TestMethod()]
		public void OpenTest()
		{
			EmbeddedResourceDirectory dir = new("Zenseless.Resources.Tests.Content");
			var stream = dir.Open("Test.txt");
			using var t = new StreamReader(stream);
			var text = t.ReadToEnd();
			Assert.AreEqual("0123456789", text);
			Assert.ThrowsException<ArgumentException>(() => dir.Open("dataTest.txt"));
		}

		[TestMethod()]
		public void ResourceTest()
		{
			EmbeddedResourceDirectory dir = new("Zenseless.Resources.Tests.Content");
			var res = dir.Resource("Test.txt");
			Assert.AreEqual("Test.txt", res.Name);
			Assert.ThrowsException<ArgumentException>(() => dir.Resource("dataTest.txt"));
		}

		[TestMethod()]
		public void AsStringTest()
		{
			EmbeddedResourceDirectory dir = new("Zenseless.Resources.Tests.Content");
			var res = dir.Resource("Test.txt");
			Assert.AreEqual("0123456789", res.AsString());
		}

		[TestMethod()]
		public void AsByteArrayTest()
		{
			EmbeddedResourceDirectory dir = new("Zenseless.Resources.Tests.Content");
			var res = dir.Resource("Test.txt");
			using var stream = res.Open();
			var buffer = new byte[stream.Length];
			stream.Read(buffer);
			CollectionAssert.AreEqual(buffer, res.AsByteArray());
		}
	}
}