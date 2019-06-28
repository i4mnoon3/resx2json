using System;
using NUnit.Framework;

namespace Resx2JSON
{
	[TestFixture]
	public class Test1
	{
		[Test]
		public void TestMethod()
		{
			var c = Resource.Deserialize("en-US.resx");
			c.Save("en-US.json");
		}
		
		[Test]
		public void a()
		{
			Console.WriteLine(@"the
""life""
is
so
the
""short""
""shit""".Sanitize());
		}
	}
}
