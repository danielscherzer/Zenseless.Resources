using Zenseless.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System;
using System.IO;

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
		}

		[TestMethod()]
		public void ResourceTest()
		{
			EmbeddedResourceDirectory dir = new("Zenseless.Resources.Tests.Content");
			var res = dir.Resource("Test.txt");
			Assert.AreEqual("Test.txt", res.Name);
		}

		[TestMethod()]
		public void OpenTextTest()
		{
			EmbeddedResourceDirectory dir = new("Zenseless.Resources.Tests.Content");
			var res = dir.Resource("Test.txt");
			Assert.AreEqual("0123456789", res.OpenText());
		}
	}
}