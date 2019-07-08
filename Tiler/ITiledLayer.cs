using System.Collections.Generic;

namespace Tiler
{
	public interface ITiledLayer
	{
		/// <summary>
		/// Unique ID of the layer.
		/// </summary>
		int Id { get; set; }

		/// <summary>
		/// The name of the layer.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// The y coordinate of the layer in tiles.
		/// </summary>
		int X { get; set; }

		/// <summary>
		/// The x coordinate of the layer in tiles.
		/// </summary>
		int Y { get; set; }

		/// <summary>
		/// The width of the layer in tiles.
		/// </summary>
		int Width { get; set; }

		/// <summary>
		/// The height of the layer in tiles.
		/// </summary>
		int Height { get; set; }

		/// <summary>
		/// The opacity of the layer as a value from 0 to 1.
		/// </summary>
		float Opacity { get; set; }

		/// <summary>
		/// Whether the layer is shown (1) or hidden (0).
		/// </summary>
		bool Visible { get; set; }

		/// <summary>
		/// Rendering offset x for this layer in pixels.
		/// </summary>
		int OffsetX { get; set; }

		/// <summary>
		/// Rendering offset y for this layer in pixels.
		/// </summary>
		int OffsetY { get; set; }

		/// <summary>
		/// The properties of the layer.
		/// </summary>
		List<TiledProperty> Properties { get; set; }
	}
}