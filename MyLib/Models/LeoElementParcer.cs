using AngleSharp.Dom;
using System;
using System.Linq;
using System.Text;

namespace MyLib.Models
{
	public static class LeoElementParcer
	{
		public static string GetText(IElement element)
		{			
			var text = "";			
			var translate = element.QuerySelector("tr.is-clickable [lang = 'ru']")?.TextContent + " - "
				+ element.QuerySelector("tr.is-clickable [lang = 'de']")?.TextContent;
			if (string.IsNullOrEmpty(translate)) 
			{
				return "";
			}

			var sb = new StringBuilder();
			var delimeter = new string('-', 60);
			var examples = element.QuerySelectorAll("tr.is-clickable")
				.Select(example => example.TextContent.Trim()).ToArray();
			var examplesRu = element.QuerySelectorAll("tr.is-clickable [lang = 'ru']")
				.Select(example => example.TextContent.Trim()).ToArray();
			var examplesDe = element.QuerySelectorAll("tr.is-clickable [lang = 'de']")
				.Select(example => example.TextContent.Trim()).ToArray();
			
			foreach(var exampleRu in examplesRu)
			{
				foreach (var exampleDe in examplesDe)
				{
					return exampleRu + " - " + exampleDe;
				}
			}

			//text += $"ÜBERSETZUNG:{Environment.NewLine}";
			text += $"{string.Join(Environment.NewLine, examplesRu)}";			
			sb.AppendLine(text);
			//sb.AppendLine(delimeter);
			return sb.ToString();
		}
	}
}
