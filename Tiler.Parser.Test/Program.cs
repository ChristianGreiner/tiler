using System;
using System.ComponentModel;

namespace Tiler.Parser.Test
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var parser = new TmxParser("map01.tmx");
			var map = parser.ParseMap();

			//PrintR(map);

			Console.WriteLine("===== TILESETS ======");

			foreach(var tileset in map.Tilesets)
			{
				//PrintR(tileset);
			}

			Console.ReadKey();
		}

		public static void PrintR(Object obj)
		{
			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
			{
				string name = descriptor.Name;
				object value = descriptor.GetValue(obj);
				Console.WriteLine("{0}: {1}", name, value);
			}
		}
	}
}