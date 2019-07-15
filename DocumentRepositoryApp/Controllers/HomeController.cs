using DocumentRepositoryApp.Repository;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DocumentRepositoryApp.Controllers
{
    public class HomeController : Controller
    {
        readonly IDocumentRepository repository = new DocumentRepository();

        //
        // GET: /Home/
        public ActionResult Index()
        {
            var docs = repository.GetAll();
            return View(docs);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(string name, HttpPostedFileBase upload)
        {
            if (!string.IsNullOrEmpty(name) 
                && upload != null 
                && int.TryParse(Membership.GetUser().ProviderUserKey.ToString(), out int userId))
            {
                string userDir = Server.MapPath($"~/Files/{userId}/");
                if (!Directory.Exists(userDir))
                    Directory.CreateDirectory(userDir);

                string fileName;
                using (SHA1 sha = new SHA1CryptoServiceProvider())
                    fileName = BitConverter.ToString(sha.ComputeHash(Encoding.Unicode.GetBytes(name + DateTime.UtcNow.ToString())))
                        .Replace("-", string.Empty);
                    
                string filePath = userDir + fileName + Path.GetExtension(upload.FileName);
                upload.SaveAs(filePath);
                repository.Add(userId, name, filePath);

                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult Download(string fileName, string filePath)
        {
            if (string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(fileName))
                return View();
            string extension = Path.GetExtension(filePath).ToLower();
            string type = GetMimeType(extension);
            return File(filePath, type, fileName + extension);
        }

        private string GetMimeType(string ext)
        {
            string mimeType = "application/unknown";
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
    }
}
