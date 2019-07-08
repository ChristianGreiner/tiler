using System;
using System.Linq;
using System.Xml;

namespace Tiler.Parser
{
	public class TmxParser
	{
		private readonly string rawData;

		public TmxParser(string rawData)
		{
			this.rawData = rawData;
		}

		public void Parse()
		{
			if (!string.IsNullOrEmpty(this.rawData))
			{
				var xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(this.rawData);
				var xmlMap = xmlDoc.FirstChild;

				TiledMap map = new TiledMap();
				map.Version = GetValueFromAttribute<string>(xmlMap, "version");

				Console.Write("Version: " + map.Version);
			}
		}

		private T GetValueFromAttribute<T>(XmlNode element, string attribute)
		{
			var result = element?.Attributes[attribute];
			if (result != null)
				return (T)Convert.ChangeType(result.Value, typeof(T));
			return default(T);
		}
	}
}