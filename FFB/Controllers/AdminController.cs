using FFB.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FFB.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ImportPlayerList(PlayerListModel list)
        {
            FFB.Domain.SiteLogic.AdminLogic.SavePlayerList(list);

            return View();
        }

    }
}
