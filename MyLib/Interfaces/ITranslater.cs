

namespace MyLib.Interfaces
{
	/// <summary>
	/// Интерфейс переводчика
	/// </summary>
	public interface ITranslater
	{
		/// <summary>
		/// Перевести
		/// </summary>
		/// <param name="word">Слово для перевода</param>
		/// <returns></returns>
		string Translate(string word);
	}
}
