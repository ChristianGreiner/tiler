using System.Collections.Generic;

namespace Tiler.Parser
{
	public interface ITilerParser<T>
	{
		List<TiledTileset> ParseTilesets(T data);

		List<ITiledLayer> ParseLayers(T data);
	}
}