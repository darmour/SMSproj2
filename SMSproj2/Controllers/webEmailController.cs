using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SMSproj2.Models;


namespace SMSproj2.Controllers
{
    public class webEmailController : Controller
    {
        // GET: webEmailController
        public ActionResult Index()
        {
            return View();
        }

       
        // GET: webEmailController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: webEmailController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(WebEmail msg)
        {
            try
            {
                ViewData["oRecipients"] = msg.toRecipients;
                ViewData["subject"] = msg.subject;
                ViewData["emailMsg"] = msg.emailMsg;
                msg.sendWebMail();
                ViewData["status"] = msg.status;
                return View("Index");
                //return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       
    }
}
