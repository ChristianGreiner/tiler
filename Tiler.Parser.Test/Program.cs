using System;
using System.IO;
using System.Text;

namespace Tiler.Parser.Test
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var data = File.ReadAllText("map01.tmx", Encoding.UTF8);
			var parser = new TmxParser(data);
			parser.Parse();

			Console.ReadKey();
		}
	}
}