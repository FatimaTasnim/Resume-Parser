using System;
using Mammoth;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Tesseract;
using System.Drawing;

namespace Resume4.Controllers
{
    public class Parser
    {
        //Convert any doc to text file
        public static string DocToText(string filePath)
        {
            var converter = new DocumentConverter();
            var result = converter.ConvertToHtml(filePath);
            var html = result.Value;
            return html;
        }

        //Convert any pdf to text file
        public static string PdfToText(string filePath)
        {
            var sb = new StringBuilder();
            try
            {
                using (PdfReader reader = new PdfReader(filePath))
                {
                    string prevPage = "";
                    for (int page = 1; page <= reader.NumberOfPages; page++)
                    {
                        ITextExtractionStrategy its = new SimpleTextExtractionStrategy();
                        var s = PdfTextExtractor.GetTextFromPage(reader, page, its);
                        if (prevPage != s) sb.Append(s);
                        prevPage = s;
                    }
                    reader.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return sb.ToString();
        }

        //Convert any image to text
        public static string ImageToText(string filePath)
        {
            string text;
            using (var engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default))
            {
                using (var image = new Bitmap(filePath))
                {

                    using (var page = engine.Process(image))
                    {
                        text= page.GetText();
                    }

                }
            }
            return text;
        }

    }
}
