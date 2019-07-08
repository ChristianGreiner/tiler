using System.Collections.Generic;

namespace Tiler
{
	public class TiledObject
	{
		/// <summary>
		/// Unique ID of the object. Each object that is placed on a map gets a unique id. Even if an object was deleted, no object gets the same ID
		/// </summary>
		public uint Id { get; set; }

		/// <summary>
		/// The name of the object.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The type of the object.
		/// </summary>
		public string Type { get; set; }

		/// <summary>
		/// The y coordinate of the object.
		/// </summary>
		public float X { get; set; }

		/// <summary>
		/// The x coordinate of the object.
		/// </summary>
		public float Y { get; set; }

		/// <summary>
		/// The width of the object.
		/// </summary>
		public float Width { get; set; }

		/// <summary>
		/// The height of the object.
		/// </summary>
		public float Height { get; set; }

		/// <summary>
		/// The rotation of the object in degrees clockwise.
		/// </summary>
		public float Rotation { get; set; }

		/// <summary>
		/// An reference to a tile (optional).
		/// </summary>
		public int Gid { get; set; }

		/// <summary>
		/// Whether the object is shown (1) or hidden (0).
		/// </summary>
		public bool Visible { get; set; }

		/// <summary>
		/// The properties of the object.
		/// </summary>
		public List<TiledProperty> Properties { get; set; }

		public TiledObject()
		{
			Visible = true;
		}
	}
}