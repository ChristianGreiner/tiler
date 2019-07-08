using Duality.Editor.AssetManagement;

namespace Tiler.Duality.Editor.Importer
{
	public class TmxImporter : IAssetImporter
	{
		/// <summary>
		/// The id of the importer.
		/// </summary>
		public string Id
		{
			get { return "TmxImporter"; }
		}

		/// <summary>
		/// The name of the importer.
		/// </summary>
		public string Name
		{
			get { return "Tmx Importer"; }
		}

		/// <summary>
		/// The pr
		/// </summary>
		public int Priority
		{
			get { return 0; }
		}

		public void PrepareImport(IAssetImportEnvironment env)
		{
		}

		public void Import(IAssetImportEnvironment env)
		{
		}

		public void PrepareExport(IAssetExportEnvironment env)
		{
		}

		public void Export(IAssetExportEnvironment env)
		{
		}
	}
}