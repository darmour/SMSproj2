using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SMSproj2.Models;

namespace SMSproj2.Controllers
{
    
    public class smsMessageController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(SMSMessage msg)
        {
            try
            {
                ViewData["phoneNumber"] = msg.phoneNumber;
                ViewData["smsMessageTest"] = msg.smsMessageText;
                msg.sendMessage();
                ViewData["status"] = msg.status;
                return View("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
