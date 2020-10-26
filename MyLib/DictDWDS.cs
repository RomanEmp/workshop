using AngleSharp;
using MyLib.Models;
using System;
using System.Text;

namespace MyLib
{
	public class DictDWDS : BaseTranslater
	{
		public DictDWDS(string baseURL) : base(baseURL)
		{

		}
		public override string Translate(string word)
		{
			var config = Configuration.Default.WithDefaultLoader();
			// Устанавливаем адрес страницы сайта
			var address = $"{BaseURL}{word}";
			// загружаем страницу и разбираем её
			var document = BrowsingContext.New(config).OpenAsync(address).Result;
			DataCollector result = new DataCollector();
			//--Заголовок
			var cellSelector = "div.dwdswb-lesart";
			var cells = document.QuerySelectorAll(cellSelector);
			var sb = new StringBuilder();
			var delimeter = new string('-', 60);
			foreach (var cell in cells)
			{
				var text = DwdsElementParser.GetText(cell);
				if (string.IsNullOrEmpty(text))
				{
					continue;
				}

				sb.AppendLine(text);
				sb.AppendLine(delimeter);
			}

			return sb.ToString();

		}
	}
}
