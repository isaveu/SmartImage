using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using JetBrains.Annotations;
using SimpleCore.Utilities;
using SmartImage.Searching;
using SmartImage.Utilities;

// ReSharper disable IdentifierTypo

namespace SmartImage
{
	/// <summary>
	/// Search config
	/// 
	/// </summary>
	/// <remarks>
	/// Config is read from config file (<see cref="RuntimeInfo.ConfigLocation"/>) or from specified arguments
	/// </remarks>
	public sealed class SearchConfig
	{
		private const string CFG_IMGUR_APIKEY = "imgur_client_id";
		private const string CFG_SAUCENAO_APIKEY = "saucenao_key";
		private const string CFG_SEARCH_ENGINES = "search_engines";
		private const string CFG_PRIORITY_ENGINES = "priority_engines";


		private SearchConfig()
		{
			bool newCfg = false;

			// create cfg with default options if it doesn't exist
			if (!File.Exists(RuntimeInfo.ConfigLocation)) {
				var f = File.Create(RuntimeInfo.ConfigLocation);
				f.Close();
				newCfg = true;
			}

			var cfgFromFileMap = ExplorerSystem.ReadMap(RuntimeInfo.ConfigLocation);

			SearchEngines = ReadMapKeyValue(CFG_SEARCH_ENGINES, cfgFromFileMap, true, ENGINES_DEFAULT);
			PriorityEngines = ReadMapKeyValue(CFG_PRIORITY_ENGINES, cfgFromFileMap, true, PRIORITY_ENGINES_DEFAULT);
			ImgurAuth = ReadMapKeyValue(CFG_IMGUR_APIKEY, cfgFromFileMap, true, String.Empty);
			SauceNaoAuth = ReadMapKeyValue(CFG_SAUCENAO_APIKEY, cfgFromFileMap, true, String.Empty);

			if (newCfg) {
				WriteToFile();
			}
		}

		private const SearchEngines ENGINES_DEFAULT = SearchEngines.All;
		private const SearchEngines PRIORITY_ENGINES_DEFAULT = SearchEngines.SauceNao;


		/// <summary>
		///     User config and arguments
		/// </summary>
		public static SearchConfig Config { get; private set; } = new SearchConfig();

		public bool NoArguments { get; set; }

		public SearchEngines SearchEngines { get; set; }

		public SearchEngines PriorityEngines { get; set; }

		public string ImgurAuth { get; set; }


		public string SauceNaoAuth { get; set; }


		public bool UpdateConfig { get; set; }

		public string Image { get; set; }


		public IDictionary<string, string> ToMap()
		{
			var m = new Dictionary<string, string>
			{
				{CFG_SEARCH_ENGINES, SearchEngines.ToString()},
				{CFG_PRIORITY_ENGINES, PriorityEngines.ToString()},
				{CFG_IMGUR_APIKEY, ImgurAuth},
				{CFG_SAUCENAO_APIKEY, SauceNaoAuth}
			};

			return m;
		}

		public void Reset()
		{
			SearchEngines = SearchEngines.All;
			PriorityEngines = SearchEngines.SauceNao;
			ImgurAuth = null;
			SauceNaoAuth = null;

		}


		internal void WriteToFile()
		{

			CliOutput.WriteInfo("Updating config");
			ExplorerSystem.WriteMap(ToMap(), RuntimeInfo.ConfigLocation);
			CliOutput.WriteInfo("Wrote to {0}", RuntimeInfo.ConfigLocation);
		}

		private static void WriteMapKeyValue<T>(string name, T value, IDictionary<string, string> cfg)
		{
			string? valStr = value.ToString();

			if (!cfg.ContainsKey(name)) {
				cfg.Add(name, valStr);
			}
			else {
				cfg[name] = valStr;
			}

			//Update();
		}


		private static T ReadMapKeyValue<T>(string name,
			IDictionary<string, string> cfg,
			bool setDefaultIfNull = false,
			T defaultValue = default)
		{
			if (!cfg.ContainsKey(name)) cfg.Add(name, String.Empty);
			//Update();

			string rawValue = cfg[name];

			if (setDefaultIfNull && String.IsNullOrWhiteSpace(rawValue)) {
				WriteMapKeyValue(name, defaultValue.ToString(), cfg);
				rawValue = ReadMapKeyValue<string>(name, cfg);
			}

			var parse = Common.Read<T>(rawValue);
			return parse;
		}

		public override string ToString()
		{
			var sb = new StringBuilder();

			sb.AppendFormat("Search engines: {0}\n", SearchEngines);
			sb.AppendFormat("Priority engines: {0}\n", PriorityEngines);
			sb.AppendFormat("Imgur auth: {0}\n", ImgurAuth);
			sb.AppendFormat("SauceNao auth: {0}\n", SauceNaoAuth);
			sb.AppendFormat("Image: {0}\n", Image);

			return sb.ToString();
		}


		public static void Cleanup()
		{
			if (Config.UpdateConfig) {
				Config.WriteToFile();
			}
		}

		/// <summary>
		/// Parse config arguments and options
		/// </summary>
		/// <param name="args">Command line arguments</param>
		public static void ReadSearchConfigArguments(string[] args)
		{
			bool noArgs = args == null || args.Length == 0;

			if (noArgs) {
				Config.NoArguments = true;
				return;
			}

			var argQueue = new Queue<string>(args);
			using var argEnumerator = argQueue.GetEnumerator();

			while (argEnumerator.MoveNext()) {
				string argValue = argEnumerator.Current;

				// todo: structure

				switch (argValue) {
					case "--search-engines":
						argEnumerator.MoveNext();
						string sestr = argEnumerator.Current;
						Config.SearchEngines = Common.Read<SearchEngines>(sestr);
						break;
					case "--priority-engines":
						argEnumerator.MoveNext();
						string pestr = argEnumerator.Current;
						Config.PriorityEngines = Common.Read<SearchEngines>(pestr);
						break;
					case "--saucenao-auth":
						argEnumerator.MoveNext();
						string snastr = argEnumerator.Current;
						Config.SauceNaoAuth = snastr;
						break;
					case "--imgur-auth":
						argEnumerator.MoveNext();
						string imastr = argEnumerator.Current;
						Config.ImgurAuth = imastr;
						break;
					case "--update-cfg":
						Config.UpdateConfig = true;
						break;


					default:
						Config.Image = argValue;
						break;
				}
			}
		}
	}
}