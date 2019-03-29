using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LevelLinDemo.DAL;
using LevelLinDemo.DAL.Edmx;
using LevelLinDemo.DAL.Repositories;

namespace LevelLinkDemo.Web.Controllers
{
    public class LevelLinkController : Controller
    {
        private static deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities();
        private static CityRepositories _cityRespository = new CityRepositories();
        // GET: LevelLink
        public ActionResult Index()
        {
            var citys = _cityRespository.GetCitys();
            return View();
        }
        //public JsonResult AddProvince(string name)
        //{

        //}
    }
}