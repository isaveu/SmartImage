#region

#endregion

using System;

namespace SmartImage.Searching.Model
{
	public interface ISearchEngine
	{
		public string Name { get; }

		public SearchEngines Engine { get; }

		public SearchResult GetResult(string url);

		public ConsoleColor Color { get; }

	}
}