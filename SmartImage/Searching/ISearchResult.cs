﻿using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
#nullable enable
namespace SmartImage.Searching
{
	public interface ISearchResult
	{
		/// <summary>
		/// Best match
		/// </summary>
		public string Url { get; }

		/// <summary>
		/// Image similarity
		/// </summary>
		public float? Similarity { get;  set; }

		/// <summary>
		/// Image width
		/// </summary>
		public int? Width { get;  set; }

		/// <summary>
		/// Image height
		/// </summary>
		public int? Height { get;  set; }


		public int? FullResolution => Width * Height;

		/// <summary>
		/// Image caption/name/title
		/// </summary>
		public string? Caption { get;  set; }
	}
}