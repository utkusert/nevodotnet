using System;
namespace LogLibrary.Layouts
{
	public interface ILayout
	{
		string Format(string dateTime, string reportLevel, string message);
	}
}

