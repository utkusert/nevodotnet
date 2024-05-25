using System.Text;
using System.Linq;

namespace LogLibrary.Models
{
    public class LogFile
    {
        private readonly StringBuilder content;

        public LogFile()
        {
            this.content = new StringBuilder();
        }

        public void Write(string message)
        {
            this.content.AppendLine(message);
        }

        public int Size => this.content.ToString().Where(char.IsLetter).Sum(c => (int)c);

        public string ReadLog()
        {
            return this.content.ToString();
        }
    }
}
