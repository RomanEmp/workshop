using MyLib.Interfaces;

namespace MyLib.Models
{
	/// <summary>
	/// Базовый класс переводчика
	/// </summary>
	public abstract class BaseTranslater  : ITranslater
	{
		/// <summary>
		/// Путь до сервиса Переводчика
		/// </summary>
		public string BaseURL { get; private set; }
		/// <summary>
		/// Перевести
		/// </summary>
		/// <param name="word">Слово для перевода</param>
		/// <returns></returns>
		public abstract string Translate(string word);
		/// <summary>
		/// CTOR
		/// </summary>
		/// <param name="baseURL">Ссылка для сервиса переводчика</param>
		public BaseTranslater(string baseURL)
		{
			BaseURL = baseURL;
		}
	}
}
