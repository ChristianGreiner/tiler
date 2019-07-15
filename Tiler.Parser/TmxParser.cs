using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Tiler.Parser
{
	public class TmxParser : ITilerParser<XmlElement>
	{
		private readonly string tmxPath;
		private readonly string rawData;

		public TmxParser(string tmxPath)
		{
			this.tmxPath = tmxPath;
			this.rawData = File.ReadAllText(tmxPath);
		}

		public TiledMap ParseMap()
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
				map.TileHeight = GetValueFromAttribute<int>(xmlMap, "tileheight");

				// Next Ids
				map.NextLayerId = GetValueFromAttribute<int>(xmlMap, "nextlayerid");
				map.NextObjectId = GetValueFromAttribute<int>(xmlMap, "nextobjectid");

				map.BackgroundColor = GetValueFromAttribute<string>(xmlMap, "backgroundcolor");


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

				map.Layer = ParseTiledLayer(xmlMap);

				map.ObjectGroups = ParseObjectGroups(xmlMap);

				return map;
			}

			return null;
		}

		public List<ITiledLayer> ParseObjectGroups(XmlElement data)
		{
			var objectGroups = new List<ITiledLayer>();
			if(data != null)
			{
				var xmlObjectGroups = data.GetElementsByTagName("objectgroup");

				foreach(XmlElement xmlObjectGroup in xmlObjectGroups)
				{
					var group = new TiledObjectGroup();
					group.Objects = new List<TiledObject>();

					ParseDefaultLayerAttributes(xmlObjectGroup, group);

					// parse objects
					var objectElements = data.GetElementsByTagName("object");
					if (objectElements != null && objectElements.Count > 0)
					{
						foreach (XmlElement xmlObjectElement in objectElements)
						{
							var obj = ParseTiledObject(xmlObjectElement);
							if (obj != null)
								group.Objects.Add(obj);
						}
					}

					objectGroups.Add(group);
				}
			}
			return objectGroups;
		}

		public TiledObject ParseTiledObject(XmlElement data)
		{
			TiledObject tiledObj = null;

			if(data.HasChildNodes)
			{
				// check if object is a polygon
				var polygonElement = data.GetElementsByTagName("polygon");
				if (polygonElement != null && polygonElement.Count > 0)
				{
				}

				var ellipseElement = data.GetElementsByTagName("ellipse");
				if (ellipseElement != null && ellipseElement.Count > 0)
				{
					tiledObj = new TiledEllipse();
				}
			} else
			{
				tiledObj = new TiledObject();
			}

			if (tiledObj != null)
			{
				tiledObj.Id = GetValueFromAttribute<uint>(data, "id");
				tiledObj.Name = GetValueFromAttribute<string>(data, "name");
				tiledObj.Type = GetValueFromAttribute<string>(data, "type");
				tiledObj.Width = GetValueFromAttribute<float>(data, "width");
				tiledObj.Height = GetValueFromAttribute<float>(data, "height");
				tiledObj.X = GetValueFromAttribute<float>(data, "x");
				tiledObj.Y = GetValueFromAttribute<float>(data, "y");
			}

			return tiledObj;
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

					var xmlTileset = LoadData(tileset.Source);

					// Offset
					var xmlOffset = xmlTileset.GetElementsByTagName("tileoffset");
					if(xmlOffset != null)
					{
						var offset = new Point();
						offset.X = GetValueFromAttribute<int>((XmlElement)xmlOffset[0], "x");
						offset.Y = GetValueFromAttribute<int>((XmlElement)xmlOffset[0], "y");
						tileset.TileOffset = offset;
					}

					tileset.Properties = ParseProperties(xmlTileset);
					tileset.Version = GetValueFromAttribute<string>(xmlTileset, "version");
					tileset.TiledVersion = GetValueFromAttribute<string>(xmlTileset, "tiledversion");

					tileset.Name = GetValueFromAttribute<string>(xmlTileset, "name");

					tileset.TileWidth = GetValueFromAttribute<int>(xmlTileset, "tilewidth");
					tileset.TileHeight = GetValueFromAttribute<int>(xmlTileset, "tileheight");

					tileset.TileCount = GetValueFromAttribute<int>(xmlTileset, "tilecount");
					tileset.Columns = GetValueFromAttribute<int>(xmlTileset, "columns");

					tileset.BackgroundColor = GetValueFromAttribute<string>(xmlTileset, "backgroundcolor");

					// Parse Images
					var xmlImage = xmlTileset.GetElementsByTagName("image")[0];
					tileset.Image = new TiledImage();
					tileset.Image.Source = GetValueFromAttribute<string>(xmlImage, "source");
					tileset.Image.Width = GetValueFromAttribute<int>(xmlImage, "width");
					tileset.Image.Height = GetValueFromAttribute<int>(xmlImage, "height");
					tileset.Image.Data = File.ReadAllBytes(tileset.Image.Source);

					// Parse Tile
					tileset.Tiles = new List<TiledTile>();
					var xmlTiles = xmlTileset.GetElementsByTagName("tile");
					foreach(XmlElement xmlTile in xmlTiles)
					{
						var tileId = GetValueFromAttribute<int>(xmlTile, "id");
						var tileProperties = ParseProperties(xmlTile);
						tileset.Tiles.Add(new TiledTile(tileId, tileProperties));
					}

					tilesets.Add(tileset);
				}
			}

			return tilesets;
		}

		public List<ITiledLayer> ParseTiledLayer(XmlElement data)
		{
			if (data != null)
			{
				var layerList = new List<ITiledLayer>();
				var xmlLayerList = data.GetElementsByTagName("layer");
				foreach (XmlElement xmlLayer in xmlLayerList)
				{
					var layer = new TiledLayer();
					layer.Id = GetValueFromAttribute<int>(xmlLayer, "id");

					ParseDefaultLayerAttributes(xmlLayer, layer);

					// parse data
					var xmlLayerData = xmlLayer.GetElementsByTagName("data");
					if(xmlLayerData != null)
					{
						layer.Data = EncodeData((XmlElement)xmlLayerData[0], new Point(layer.Width, layer.Height));
					}


					layerList.Add(layer);
				}

				return layerList;
			}
			return null;
		}

		public TiledData EncodeData(XmlElement data, Point gridSize)
		{
			var encoding = GetValueFromAttribute<string>(data, "encoding");
			var compression = GetValueFromAttribute<string>(data, "compression");
			var tiledEncoding = TiledEncoding.Base64;
			var tiledCompression = TiledCompression.None;
			var idList = new List<uint>();

			switch (encoding)
			{
				// xml encoding
				case null:
					tiledEncoding = TiledEncoding.Xml;
					foreach (XmlElement child in data.ChildNodes)
					{
						if(child.Name.Equals("title"))
							idList.Add(GetValueFromAttribute<uint>(child, "gid"));
					}

					break;

				case "csv":
					tiledEncoding = TiledEncoding.Csv;
					idList.AddRange(data.InnerText.Split(',').Select(uint.Parse));
					break;

				case "base64":
					tiledEncoding = TiledEncoding.Base64;
					break;
			}

			return new TiledData()
			{
				Compression = tiledCompression,
				Encoding = tiledEncoding,
				Tiles = idList.ToArray()
			};
		}


		public List<TiledProperty> ParseProperties(XmlElement data)
		{
			if (data != null)
			{
				var properties = new List<TiledProperty>();
				foreach (XmlElement property in data.FirstChild.ChildNodes)
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

		public XmlElement LoadData(string path)
		{
			var xmlDoc = new XmlDocument();
			var xmlData = File.ReadAllText(path);
			xmlDoc.LoadXml(xmlData);
			return xmlDoc.DocumentElement;
		}

		private T GetValueFromAttribute<T>(XmlNode element, string attribute)
		{
			if (element.Attributes == null) return default(T);
			var result = element?.Attributes[attribute];
			if (result != null)
				return (T)Convert.ChangeType(result.Value, typeof(T));
			return default(T);
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