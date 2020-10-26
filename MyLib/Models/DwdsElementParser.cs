using AngleSharp.Dom;
using System;
using System.Linq;

namespace MyLib.Models
{
	public static class DwdsElementParser
	{
		public static string GetText(IElement element)
		{

			var text = "";
			//var number = element.QuerySelector(".dwdswb-lesart-n")?.TextContent;
			var header = element.QuerySelector("span.dwdswb-definition")?.TextContent;
			if( string.IsNullOrEmpty(header))
			{				
				return "";
			}
			var examples = element.QuerySelectorAll("span.dwdswb-belegtext")
				.Select(example => example.TextContent.Trim()).ToArray();
			text = $"SYNONYM: {header}{Environment.NewLine}";
			text += $"Beispiel:{Environment.NewLine}";
			text += $"{string.Join(Environment.NewLine, examples)}";
			return text;
		}
	}
}
