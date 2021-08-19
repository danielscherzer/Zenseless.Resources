using System.IO;

namespace Zenseless.Resources
{
	/// <summary>
	/// Resource extension methods
	/// </summary>
	public static class ResourceExtensions
	{
		/// <summary>
		/// Reads the resource from the given resource directory and with the given name as text.
		/// </summary>
		/// <param name="resource">The resource directory.</param>
		/// <returns>A <c>string</c></returns>
		public static string OpenText(this IResource resource)
		{
			using var stream = resource.Open();
			using var t = new StreamReader(stream);
			return t.ReadToEnd();
		}
	}
}