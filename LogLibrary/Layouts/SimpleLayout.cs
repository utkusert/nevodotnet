using System;
namespace LogLibrary.Layouts
{
	public class SimpleLayout : ILayout
	{
        public string Format(string dateTime, string reportLevel, string message)
        {
            return $"{dateTime} - {reportLevel} - {message}";
        }

	}
}

