using System;

namespace Resx2JSON
{
	class Program
	{
		public static void Main(string[] args)
		{
			if (args.Length != 2) {
				Console.WriteLine("Usage: resx2json <FILE>");
				return;
			}
			
			var r = Resource.Deserialize(args[0]);
			r.Save(args[1]);
			Console.WriteLine(string.Format("Success! Resource file {0} converted to {1}.", args[0], args[1]));
		}
	}
}