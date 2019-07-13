﻿using System.Collections.Generic;

namespace Tiler
{
	public class TiledMap
	{
		/// <summary>
		/// The version of the map.
		/// </summary>
		public string Version { get; set; }

		/// <summary>
		/// The tiled version of the map.
		/// </summary>
		public string TiledVersion { get; set; }

		/// <summary>
		/// The width of a tile.
		/// </summary>
		public int TileWidth { get; set; }

		/// <summary>
		/// The height of a tile.
		/// </summary>
		public int TileHeight { get; set; }

		/// <summary>
		/// The width of the map in tiles.
		/// </summary>
		public int Width { get; set; }

		/// <summary>
		/// The height of the map in tiles.
		/// </summary>
		public int Height { get; set; }

		/// <summary>
		/// The background color of the map.
		/// </summary>
		public string BackgroundColor { get; set; }

		/// <summary>
		/// Stores the next available ID for new objects. This number is stored to prevent reuse of the same ID after objects have been removed.
		/// </summary>
		public int NextObjectId { get; set; }

		/// <summary>
		/// Stores the next available ID for new layers. This number is stored to prevent reuse of the same ID after layers have been removed.
		/// </summary>
		public int NextLayerId { get; set; }

		/// <summary>
		/// The map orientation.
		/// </summary>
		public TiledOrientation Orientation { get; set; }

		/// <summary>
		/// The order in which tiles on tile layers are rendered.
		/// </summary>
		public TiledRenderorder Renderorder { get; set; }

		/// <summary>
		/// For staggered and hexagonal maps, determines which axis is staggered.
		/// </summary>
		public TiledStaggerAxis StaggerAxis { get; set; }

		/// <summary>
		/// Only for hexagonal maps. Determines the width or height (depending on the staggered axis) of the tile's edge, in pixels.
		/// </summary>
		public int HexsideLength { get; set; }

		// TODO: LIST, LAYER; OBJECTGROUP, IMAGELAYER

		public List<ITiledLayer> Layer { get; set; }

		public List<ITiledLayer> ObjectGroups { get; set; }

		/// <summary>
		/// The properties of the map.
		/// </summary>
		public List<TiledProperty> Properties { get; set; }

		/// <summary>
		/// The tilesets that are used for this map.
		/// </summary>
		public List<TiledTileset> Tilesets { get; set; }

		public TiledMap()
		{
			Version = "1.0";
			Orientation = TiledOrientation.Orthogonal;
			Renderorder = TiledRenderorder.RightDown;
		}

		public override string ToString()
		{
			return "Version: " + this.Version + "\n";
		}
	}
}