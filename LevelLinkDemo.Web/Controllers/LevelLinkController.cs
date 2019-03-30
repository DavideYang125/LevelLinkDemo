using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LevelLinDemo.DAL;
using LevelLinDemo.DAL.Edmx;
using LevelLinDemo.DAL.Repositories;
using LevelLinkDemo.Web.Models;

namespace LevelLinkDemo.Web.Controllers
{
    public class LevelLinkController : Controller
    {
        private static deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities();
        private static CityRepositories _cityRespository = new CityRepositories();
        private static ProvinceRepositories _provinceRespository = new ProvinceRepositories();
        private static RegionRepositories _regionRespository = new RegionRepositories();
        // GET: LevelLink
        public ActionResult Index()
        {
            var citys = _cityRespository.GetCitys();
            return View();
        }
        /// <summary>
        /// get all level link data
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllData()
        {
            var provinces = _provinceRespository.GetProvinces();
            var citys = _cityRespository.GetCitys();
            var regions = _regionRespository.GetRegions();
            List<LevelProvince> levelProvinces = new List<LevelProvince>();
            foreach (var province in provinces)
            {
                var levelProvince = new LevelProvince();
                levelProvince.province = province;
                var currentCitys = citys.Where(l => l.ProvinceId == province.Id).ToList();
                List<LevelCity> levelCitys = new List<LevelCity>();
                foreach (var currentCity in currentCitys)
                {
                    var currentRegions = regions.Where(l => l.CityId == currentCity.Id).ToList();
                    var levelCity = new LevelCity() { city = currentCity, regions = currentRegions };
                    levelCitys.Add(levelCity);
                }
                levelProvince.citys = levelCitys;
                levelProvinces.Add(levelProvince);
            }
            return Json(levelProvinces, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetProvinces()
        {
            var provinces = _provinceRespository.GetProvinces();
            return Json(provinces, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetCitys(int provinceId)
        {
            var citys = _cityRespository.GetCitysById(provinceId);
            return Json(citys, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetRegions(int cityId)
        {
            var regions = _regionRespository.GetRegionsByCityId(cityId);
            return Json(regions, JsonRequestBehavior.AllowGet);
        }
    }
}