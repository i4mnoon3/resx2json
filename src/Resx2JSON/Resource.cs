using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Resx2JSON
{
	[XmlRoot("root")]
	public class Resource
	{
		[XmlElement("data")]
		public Data[] Data;
		
		public Resource()
		{
		}
		
		public void Save(string filename)
		{
			using (var w = new StreamWriter(filename)) {
				w.WriteLine(ToString());
			}
		}
		
		public override string ToString()
		{
			var s = "";
			int i = 0;
			foreach (var d in Data) {
				if (i++ > 0) {
					s += ", "; //+ Environment.NewLine;
				}
				s += d.ToString();
			}
			return "[ " + s + " ]";
		}
		
		public static Resource Deserialize(string filename)
		{
			return Deserialize(new StreamReader(filename));
		}
		
		public static Resource Deserialize(TextReader reader)
		{
			XmlSerializer serializer = new XmlSerializer(typeof(Resource));
			Resource type = (Resource)serializer.Deserialize(reader);
			reader.Close();
			return type;
		}
	}
	
	public static class Helper
	{
		public static string Sanitize(this string str)
		{
			var splittedStr = str.Split('\n');
			string separator = "\\n\" + " + Environment.NewLine + "\"";
			if (splittedStr.Length > 0) {
				var strings = new List<string>();
				foreach (var s in splittedStr) {
					strings.Add(X(s));
				}
				return String.Join(separator, strings);
			} else {
				return X(str);
			}
		}
		
		static string X(this string str)
		{
			str = str.Replace("\"", "\\\"");
			return str.Trim();
		}
	}
	
	public class Data
	{
		[XmlAttribute("name")]
		public string Name { get; set; }
		[XmlElement("value")]
		public string Value { get; set; }
		
		public override string ToString()
		{
			return string.Format("{{ \"name\": \"{0}\", \"value\": \"{1}\" }}", Name, Value.Sanitize());
		}
	}
}
