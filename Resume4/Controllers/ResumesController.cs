using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Http;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using System.Net.Http.Headers;
using System.Text;
using Resume4.Models;
using Resume4.SearchServices;
using Tesseract;
using System.Drawing;

namespace Resume4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumesController : ControllerBase
    {

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            using (var engine = new TesseractEngine("./tessdata", "eng", EngineMode.Default))
            {
                using (var image = new Bitmap("./text.png"))
                {

                    using (var page = engine.Process(image))
                    {
                        return Ok(page.GetText());
                    }

                }
            }
        }

        //Get all file names
        [HttpGet("GetFileNames")]
        public ActionResult<List<string>> GetFileNames()
        {
            List<string> filenames = new List<string>();
            DirectoryInfo mydir = new DirectoryInfo(@"./wwwroot/Updates");
            FileInfo[] f = mydir.GetFiles();
            foreach (FileInfo file in f)
            {
                filenames.Add(file.Name);
            }
            return filenames;
        }
        // GET api/values/5
        [HttpGet("GetFileData/{fileName}")]
        public ActionResult<string> GetFileData(string fileName)
        {
            DirectoryInfo mydir = new DirectoryInfo(@"./wwwroot/Updates");
            string path = "./wwwroot/Updates/";

            string line = string.Empty;
            FileInfo[] f = mydir.GetFiles();
           
            ResumeView model = new ResumeView();

            foreach (FileInfo file in f)
            {
                if (file.Name == fileName)
                {
                    string filePath = path + file.Name;
                    model.Text = Helper.GetText(filePath);
                   // model.Text = Helper.GetHtml(model.Text);
                }

            }
            return Ok(new { fileName= fileName, text = model.Text });
        }

        // POST api/values
        private IHostingEnvironment _hostingEnvironment;
        private ActionResult<Stream> stream;

        public ResumesController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        [HttpPost("UploadFiles")]

        public IActionResult Post(IFormFile formFile)
        {
            try
            {
                var file = Request.Form.Files[0];
                string folderName = "Updates";
                string webRootPath = _hostingEnvironment.WebRootPath;
                string newPath = System.IO.Path.Combine(webRootPath, folderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                if (file.Length > 0)
                {
                    string fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fullPath = System.IO.Path.Combine(newPath, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
                return Ok();
            }
            catch (System.Exception ex)
            {
                return Ok("Upload Failed: " + ex.Message);
            }
        }

        /*[HttpPost("Search/{Key}")]
        public IActionResult Search(string key)
        {
           // DirectoryInfo mydir = new DirectoryInfo(@"C:/Users/BS049/source/repos/CV Parsing/Resume4/wwwroot/Updates");
            DirectoryInfo mydir = new DirectoryInfo(@"./wwwroot/Updates");
            string path = "./wwwroot/Updates/";
            ResumeView model = new ResumeView();
            int MaxMatch = 0;
            FileInfo[] f = mydir.GetFiles();
            List<int> LengthList = new List<int>();
            List<SearchResultView> LengthListS = new List<SearchResultView>();

            foreach (FileInfo file in f)
            {
                string filePath = path + file.Name;
                model.Text = Helper.GetText(filePath);  
                int Len = LCS.SearchStatus(model.Text, key);
                MaxMatch = Helper.max(Len , MaxMatch);
                LengthList.Add(Len);
            }
            int cnt = 0;
            List<SearchResultView> obj = new List<SearchResultView>();
            
            foreach (FileInfo file in f)
            {
                SearchResultView temp = new SearchResultView();
                if (LengthList[cnt] == MaxMatch)
                {
                    temp.Filename = file.Name;
                    string filePath = path + file.Name;
                    model.Text = Helper.GetText(filePath);
                    temp.htmlText= LCS.SearchFinalResult(model.Text, key);
                    obj.Add(temp);
                }
                cnt++;
            }
            return Ok(obj);
        }*/
        [HttpPost("SearchResult/{filename}/{Key}")]
        public IActionResult SearchResult(string filename, string key)
        {
            // DirectoryInfo mydir = new DirectoryInfo(@"C:/Users/BS049/source/repos/CV Parsing/Resume4/wwwroot/Updates");
            DirectoryInfo mydir = new DirectoryInfo(@"./wwwroot/Updates");
            string path = "./wwwroot/Updates/";
            ResumeView model = new ResumeView();
            int MaxMatch = 0;
            FileInfo[] f = mydir.GetFiles();
            List<int> LengthList = new List<int>();
            SearchResultView obj = new SearchResultView();
             
            string filePath = path + filename;
            obj.Filename = filename;
            model.Text = Helper.GetText(filePath);
            model.Text = Helper.GetHtml(model.Text);
            obj.Len = model.Text.Length;
            obj.htmlText = LCS.SearchFinalResult(model.Text, key);
            return Ok(obj);
        }
        /*[HttpPost]
          public void Post([FromBody] string value)
          {

          }*/

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{Filename}")]
        public void Delete(string Filename)
        {
            string fullPath = "./wwwroot/Updates/" + Filename;
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }

        }
    }
}

