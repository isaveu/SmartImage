#region

using System;
using SmartImage.Searching.Model;

#endregion

namespace SmartImage.Searching.Engines.Simple
{
	public sealed class GoogleImages : SimpleSearchEngine
	{
		public GoogleImages() : base("http://images.google.com/searchbyimage?image_url=") { }

		public override string Name => "Google Images";

		public override SearchEngines Engine => SearchEngines.GoogleImages;

		public override ConsoleColor Color => ConsoleColor.DarkBlue;
	}
}