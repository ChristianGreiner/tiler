namespace Tiler
{
	public class TiledGrid
	{
		/// <summary>
		/// Orientation of the grid for the tiles in this tileset.
		/// </summary>
		public TiledOrientation Orientation { get; set; }

		/// <summary>
		/// The width of a grid cell
		/// </summary>
		public int Width { get; set; }

		/// <summary>
		/// The height of a grid cell
		/// </summary>
		public int Height { get; set; }
	}
}