using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Linq;

namespace Zenseless.Resources.Tests;

[TestClass()]
public class ShortestMatchResourceDirectoryTests
{
	[TestMethod()]
	public void ShortestMatchResourceDirectoryTest()
	{
		EmbeddedResourceDirectory inner = new();
		ShortestMatchResourceDirectory dir = new(inner);
		Assert.AreSame(inner, dir.ResourceDirectory);
	}

	[TestMethod()]
	public void EnumerateResourcesTest()
	{
		ShortestMatchResourceDirectory dir = new(new EmbeddedResourceDirectory());
		var res = dir.EnumerateResources();
		Assert.AreEqual(1, res.Count());
	}

	[TestMethod()]
	public void ExistsTest()
	{
		ShortestMatchResourceDirectory dir = new(new EmbeddedResourceDirectory());
		Assert.IsTrue(dir.Exists("Test.txt"));
		Assert.IsFalse(dir.Exists("dataTest.txt"));
	}

	[TestMethod()]
	public void MatchesTest()
	{
		ShortestMatchResourceDirectory dir = new(new EmbeddedResourceDirectory());
		Assert.AreEqual(1, dir.Matches("test").Count());
		Assert.AreEqual(1, dir.Matches("Test.txt").Count());
		Assert.AreEqual(1, dir.Matches("content.test.txt").Count());
	}

	[TestMethod()]
	public void OpenTest()
	{
		ShortestMatchResourceDirectory dir = new(new EmbeddedResourceDirectory());
		var stream = dir.Open("Test.txt");
		using var t = new StreamReader(stream);
		var text = t.ReadToEnd();
		Assert.AreEqual("0123456789", text);
		Assert.ThrowsException<InvalidOperationException>(() => dir.Open("dataTest.txt"));
	}

	[TestMethod()]
	public void ResourceTest()
	{
		ShortestMatchResourceDirectory dir = new(new EmbeddedResourceDirectory());
		var res = dir.Resource("Test.txt");
		Assert.AreEqual(dir.Matches("Test.txt").Single(), res.Name);
		Assert.ThrowsException<InvalidOperationException>(() => dir.Resource("dataTest.txt"));
	}
}