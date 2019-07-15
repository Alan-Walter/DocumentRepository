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
    }
}
