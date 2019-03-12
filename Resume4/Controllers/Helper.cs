using System.Text;


namespace Resume4.Controllers
{
    public class Helper
    {
        public static int max(int a, int b)
        {
            if (a > b) return a;
            return b;
        }
        public static string GetText(string filePath)
        {
            if (filePath.Contains(".pdf"))
            {
                return Parser.PdfToText(filePath);
            }
            else if ((filePath.Contains(".docx") || filePath.Contains(".doc")))
            {
                return Parser.DocToText(filePath);
            }
            else
            {
                return Parser.ImageToText(filePath);
            }
        }

        public static string GetHtml(string Text)
        {
            var sw = new StringBuilder();
            foreach (string s in Text.Split('\n'))
            {
                sw.AppendFormat("<p>{0}</p>", s);
            }
            return sw.ToString();
        }
    }
}
