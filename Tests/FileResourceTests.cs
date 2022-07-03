using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Zenseless.Resources.Tests
{
	[TestClass()]
	public class FileResourceTests
	{
		[TestMethod()]
		public void FileResourceDirectoryArgumentExceptionTest()
		{
			Assert.ThrowsException<ArgumentException>(() => new FileResourceDirectory(""));
			Assert.ThrowsException<ArgumentException>(() => new FileResourceDirectory("asdfsdfxcvydsa"));
		}

		[TestMethod()]
		public void FileResourceDirectoryExistsTest()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var dir = Path.GetDirectoryName(assembly.Location);
			Assert.IsNotNull(dir);
			var assemblyName = Path.GetFileName(assembly.Location);
			Assert.IsNotNull(assemblyName);
			var resDir = new FileResourceDirectory(dir);
			Assert.IsTrue(resDir.Exists(assemblyName));
			Assert.IsFalse(resDir.Exists("blatemptest12378"));
			Assert.AreEqual(assemblyName, resDir.Resource(assemblyName).Name);
		}

		[TestMethod()]
		public void FileResourceDirectoryEnumerateTest()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var dir = Path.GetDirectoryName(assembly.Location);
			Assert.IsNotNull(dir);
			var assemblyName = Path.GetFileName(assembly.Location);
			Assert.IsNotNull(assemblyName);
			var resDir = new FileResourceDirectory(dir);
			var res = resDir.EnumerateResources().ToList();
			CollectionAssert.AllItemsAreNotNull(res);
			res.Contains(assemblyName);
		}

		[TestMethod()]
		public void FileResourceDirectoryOpenTest()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var dir = Path.GetDirectoryName(assembly.Location);
			Assert.IsNotNull(dir);
			var resDir = new FileResourceDirectory(dir);
			var jsonName = resDir.EnumerateResources().Where(res => res.EndsWith(".json")).First();
			var jsonText = resDir.Resource(jsonName).AsString();
			Assert.IsNotNull(jsonText);
			Assert.IsTrue(jsonText.Contains(":"));
		}
	}
}