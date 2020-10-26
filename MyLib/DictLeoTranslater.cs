using AngleSharp;
using MyLib.Models;
using System.Text;

namespace MyLib
{
    /// <summary>
    /// Переводчик LeoDict
    /// </summary>
	public class DictLeoTranslater:BaseTranslater
	{
		public DictLeoTranslater(string baseURL):base (baseURL)
		{
                
		}       

		public override string Translate(string word)
		{
            var config = Configuration.Default.WithDefaultLoader();
            var address = $"{BaseURL}{word}";
            var document =  BrowsingContext.New(config).OpenAsync(address).Result;
            DataCollector result = new DataCollector();
            //--Заголовок
            var cellSelector = "tbody";
            var cells = document.QuerySelectorAll(cellSelector);
			var sb = new StringBuilder();
			var delimeter = new string('-', 60);

			foreach (var cell in cells)
			{
				var text = LeoElementParcer.GetText(cell);
				if (string.IsNullOrEmpty(text))
				{
					continue;
				}
				sb.AppendLine(text);
				sb.AppendLine(delimeter);
			}
			

			return sb.ToString();
			// return cell.TextContent;

		}
	}
}
