using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Tiler.Parser
{
	public class TmxParser : ITilerParser<XmlElement>
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
				var xmlMap = xmlDoc.DocumentElement;

				TiledMap map = new TiledMap();

				// Version
				map.Version = GetValueFromAttribute<string>(xmlMap, "version");
				map.TiledVersion = GetValueFromAttribute<string>(xmlMap, "tiledversion");

				// Size
				map.Width = GetValueFromAttribute<int>(xmlMap, "width");
				map.Height = GetValueFromAttribute<int>(xmlMap, "height");

				// Tile Size
				map.TileWidth = GetValueFromAttribute<int>(xmlMap, "tilewidth");
				map.TileWidth = GetValueFromAttribute<int>(xmlMap, "tileheight");

				// Next Ids
				map.NextLayerId = GetValueFromAttribute<int>(xmlMap, "nextlayerid");
				map.NextObjectId = GetValueFromAttribute<int>(xmlMap, "nextobjectid");

				var renderorder = GetValueFromAttribute<string>(xmlMap, "renderorder");
				switch (renderorder)
				{
					case "right-down":
						map.Renderorder = TiledRenderorder.RightDown;
						break;

					case "right-up":
						map.Renderorder = TiledRenderorder.RightUp;
						break;

					case "left-down":
						map.Renderorder = TiledRenderorder.LeftDown;
						break;

					case "left-up":
						map.Renderorder = TiledRenderorder.LeftUp;
						break;

					default:
						map.Renderorder = TiledRenderorder.RightDown;
						break;
				}

				map.Tilesets = ParseTilesets(xmlMap);

				Console.WriteLine();
			}
		}

		private T GetValueFromAttribute<T>(XmlNode element, string attribute)
		{
			if (element.Attributes == null) return default(T);
			var result = element?.Attributes[attribute];
			if (result != null)
				return (T)Convert.ChangeType(result.Value, typeof(T));
			return default(T);
		}

		public List<TiledTileset> ParseTilesets(XmlElement data)
		{
			XmlNodeList xmlTilesets = data.GetElementsByTagName("tileset");

			List<TiledTileset> tilesets = new List<TiledTileset>();

			if (xmlTilesets?.Count > 0)
			{
				foreach (XmlElement element in xmlTilesets)
				{
					var tileset = new TiledTileset();

					tileset.FirstGid = GetValueFromAttribute<int>(element, "firstgid");
					tileset.Source = GetValueFromAttribute<string>(element, "source");

					tilesets.Add(tileset);
				}
			}

			return tilesets;
		}

		public List<ITiledLayer> ParseLayers(XmlElement data)
		{
			List<ITiledLayer> layerList = new List<ITiledLayer>();

			foreach (var child in data.Children)
			{
				// check if layer are stored in a group
				if (child.TagName.Equals("group"))
				{
					foreach (var groupElement in child.Children)
					{
						var groupLayer = ParseLayer(groupElement);
						if (groupLayer != null)
							layerList.Add(groupLayer);
					}
				}

				var layer = ParseLayer(child);
				if (layer != null)
					layerList.Add(layer);
			}

			return layerList;
		}

		/// <summary>
		/// Parse a single layer with tile data.
		/// </summary>
		/// <param name="data">The xml element.</param>
		/// <returns>The layer.</returns>
		public TiledLayer ParseTiledLayer(XmlElement data)
		{
			var layer = new TiledLayer();

			ParseDefaultLayerAttributes(data, layer);

			// layer data
			var dataElement = data.GetElementsByTagName("data").FirstOrDefault();
			if (dataElement != null)
				layer.Data = EncodeData(dataElement, layer.Size);

			return layer;
		}

		public List<TiledProperty> ParseProperties(XmlElement data)
		{
			if (data != null)
			{
				var properties = new List<TiledProperty>();
				foreach (XmlElement property in data.ChildNodes)
				{
					var xmlName = GetValueFromAttribute<string>(property, "name");
					var xmlTypeName = GetValueFromAttribute<string>(property, "type");
					var xmlValue = GetValueFromAttribute<string>(property, "value");

					Type propertyType = null;

					switch (xmlTypeName)
					{
						case "string":
							propertyType = typeof(string);
							break;

						case "":
							propertyType = typeof(string);
							break;

						case "int":
							propertyType = typeof(int);
							break;

						case "float":
							propertyType = typeof(float);
							break;

						case "bool":
							propertyType = typeof(bool);
							break;

						case "color":
							throw new NotImplementedException("Colors not implmented yet.");

						case "file":
							propertyType = typeof(string);
							break;

						default:
							propertyType = typeof(string);
							break;
					}

					var propertyValue = Convert.ChangeType(xmlValue, propertyType);
					properties.Add(new TiledProperty(xmlName, propertyType, propertyValue));
				}

				return properties;
			}

			return null;
		}

		private void ParseDefaultLayerAttributes(XmlElement xmlElement, ITiledLayer layer)
		{
			// name
			layer.Name = GetValueFromAttribute<string>(xmlElement, "name");

			// Size
			layer.Width = GetValueFromAttribute<int>(xmlElement, "width");
			layer.Height = GetValueFromAttribute<int>(xmlElement, "height");

			// opacity
			var opacity = GetValueFromAttribute<string>(xmlElement, "opacity");
			if (opacity != null)
				layer.Opacity = float.Parse(opacity);

			// offset
			layer.OffsetX = GetValueFromAttribute<int>(xmlElement, "offsetx");
			layer.OffsetY = GetValueFromAttribute<int>(xmlElement, "offsety");

			// properties
		}
	}
}