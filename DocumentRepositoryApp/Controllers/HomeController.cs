using DocumentRepositoryApp.Models;
using DocumentRepositoryApp.Repository;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DocumentRepositoryApp.Controllers
{
    public class HomeController : Controller
    {
        DocumentRepository repository = new DocumentRepository();

        //
        // GET: /Home/
        public ActionResult Index()
        {
            var docs = repository.GetAll();
            return View(docs);
            //var user = Membership.GetUser();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                upload.SaveAs(Server.MapPath("~/Files/" + fileName));
            }
            return RedirectToAction("Index");
        }
    }
}
