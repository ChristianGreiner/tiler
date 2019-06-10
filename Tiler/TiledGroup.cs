using Duality;

namespace Tiler
{
	public class TiledGroup
	{
		/// <summary>
		/// Unique ID of the layer.
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The name of the group layer.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Rendering offset of the group layer in pixels.
		/// </summary>
		public Point2 Offset { get; set; }

		/// <summary>
		/// The opacity of the layer as a value from 0 to 1. Defaults to 1.
		/// </summary>
		public float Opacity { get; set; }

		/// <summary>
		/// Whether the layer is shown (1) or hidden (0). Defaults to 1.
		/// </summary>
		public bool Visible { get; set; }
	}
}