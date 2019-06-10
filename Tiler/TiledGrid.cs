using Duality;

namespace Tiler
{
	public class TiledGrid
	{
		/// <summary>
		/// Orientation of the grid for the tiles in this tileset.
		/// </summary>
		public TiledOrientation Orientation { get; set; }

		/// <summary>
		/// The size of a grid cell
		/// </summary>
		public Point2 Size { get; set; }
	}
}