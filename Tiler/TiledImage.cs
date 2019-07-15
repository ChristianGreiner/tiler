namespace Tiler
{
	public class TiledImage
	{
		/// <summary>
		/// The source of the image.
		/// </summary>
		public string Source { get; set; }

		/// <summary>
		/// The width of the image.
		/// </summary>
		public int Width { get; set; }

		/// <summary>
		/// The height of the image.
		/// </summary>
		public int Height { get; set; }

		public byte[] Data { get; set; }

		public TiledImage(string source = "", int width = 0, int height = 0)
		{
			this.Source = source;
			this.Width = width;
			this.Height = height;
		}
	}
}