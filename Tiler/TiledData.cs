using Duality;

namespace Tiler
{
	public class TiledData
	{
		/// <summary>
		/// The encoding used to encode the tile layer data.
		/// </summary>
		public TiledEncoding Encoding { get; set; }

		/// <summary>
		/// The compression used to compress the tile layer data.
		/// </summary>
		public TiledCompression Compression { get; set; }

		/// <summary>
		/// All ids of tiles stored in a grid.
		/// </summary>
		public uint[][] Tiles { get; set; }

		public TiledData()
		{
			Encoding = TiledEncoding.Csv;
			Compression = TiledCompression.None;
		}
	}
}