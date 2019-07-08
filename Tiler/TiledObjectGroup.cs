using System.Collections.Generic;

namespace Tiler
{
	public class TiledObjectGroup : ITiledLayer
	{
		/// <summary>
		/// Unique ID of the layer.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The name of the layer.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The y coordinate of the layer in tiles.
		/// </summary>
		public int X { get; set; }

		/// <summary>
		/// The x coordinate of the layer in tiles.
		/// </summary>
		public int Y { get; set; }

		/// <summary>
		/// The width of the layer in tiles.
		/// </summary>
		public int Width { get; set; }

		/// <summary>
		/// The height of the layer in tiles.
		/// </summary>
		public int Height { get; set; }

		/// <summary>
		/// The opacity of the layer as a value from 0 to 1.
		/// </summary>
		public float Opacity { get; set; }

		/// <summary>
		/// Whether the layer is shown (1) or hidden (0).
		/// </summary>
		public bool Visible { get; set; }

		/// <summary>
		/// Rendering offset x for this layer in pixels.
		/// </summary>
		public int OffsetX { get; set; }

		/// <summary>
		/// Rendering offset y for this layer in pixels.
		/// </summary>
		public int OffsetY { get; set; }

		/// <summary>
		/// Whether the objects are drawn according to the order of appearance ("index") or sorted by their y-coordinate ("topdown"). Defaults to "topdown".
		/// </summary>
		public TiledDraworder Draworder { get; set; }

		/// <summary>
		/// Stores all objects of the objectgroup.
		/// </summary>
		public List<TiledObject> Objects { get; set; }

		/// <summary>
		/// The properties of the objectgroup.
		/// </summary>
		public List<TiledProperty> Properties { get; set; }

		public TiledObjectGroup()
		{
			Opacity = 1f;
			Visible = true;
			Draworder = TiledDraworder.Index;
		}
	}
}