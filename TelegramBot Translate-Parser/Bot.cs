using MyLib;
using MyLib.Models;
using System;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

class Bot
{
	/// <summary>
	/// Telegram Bot
	/// </summary>
	public static readonly TelegramBotClient BotD = new TelegramBotClient("1215869289:AAH7zCLYpZKfmZVXziJ9Gxhapaoq5IhNr8w");

	public static void Main(string[] args)
	{
		BotD.OnMessage += Bot_OnMessage;
		BotD.StartReceiving();
		Console.ReadLine();
	}
	/// <summary>
	/// События получения сообщения
	/// </summary>
	/// <param name="sender">Отправитель сообщений</param>
	/// <param name="e">Сообщение </param>
	public static void Bot_OnMessage(object sender, MessageEventArgs e)
	{
		var translaters = new BaseTranslater[] { new DictLeoTranslater("https://dict.leo.org/russisch-deutsch/"),
			new DictDWDS("https://www.dwds.de/wb/") };
		var word = e.Message?.Text;
		if (string.IsNullOrEmpty(word))
		{
			return;
		}
		var builder = new StringBuilder();
		foreach (var translater in translaters)
		{
			builder.AppendLine(translater.Translate(word));
		}
		var resultText = builder.ToString();
		BotD.SendTextMessageAsync(e.Message.Chat.Id, resultText);
	}
}



