using LevelLinDemo.DAL.Edmx;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LevelLinDemo.DAL.Repositories
{
    public class CityRepositories
    {

        public List<citys> GetCitys()
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var citys = _context.citys.ToList().OrderBy(l => l.Unique).ToList();
                return citys;
            }
        }
        public citys GetCitys(string cityUnique)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var city = _context.citys.FirstOrDefault(l => l.Unique == cityUnique);
                return city;
            }
        }
        public List<citys> GetCitysByProvinceUnique(string provinceUnique)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var province = _context.provinces.FirstOrDefault(l => l.Unique == provinceUnique);
                var citys = _context.citys.ToList().Where(l => l.ProvinceId == province.Id).OrderBy(l => l.Unique).ToList();
                return citys;
            }
        }
        public List<citys> GetCitysById(int id)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var citys = _context.citys.ToList().Where(l => l.ProvinceId == id).OrderBy(l => l.Unique).ToList();
                return citys;
            }
        }
        #region update
        public bool UpdateCityName(string unique, string name)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var currentCity = _context.citys.FirstOrDefault(l => l.Unique == unique);
                if (currentCity is null) return false;
                currentCity.Name = name;
                _context.SaveChanges();
                return true;
            }
        }
        public bool UpdateCityName(int id, string name)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var currentCity = _context.citys.FirstOrDefault(l => l.Id == id);
                if (currentCity is null) return false;
                currentCity.Name = name;
                _context.SaveChanges();
                return true;
            }
        }
        public bool UpdateCityName(citys city)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var currentCity = _context.citys.FirstOrDefault(l => l.Unique == city.Unique);
                if (currentCity is null) return false;
                currentCity.Name = city.Name;
                _context.SaveChanges();
                return true;
            }
        }
        
        #endregion

        #region delete
        public bool DeleteCity(string unique)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var currentCity = _context.citys.FirstOrDefault(l => l.Unique == unique);
                if (currentCity is null) return false;
                _context.citys.Remove(currentCity);
                _context.SaveChanges();
                return true;
            }
        }
        public bool DeleteCityById(int id)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var currentCity = _context.citys.FirstOrDefault(l => l.Id == id);
                if (currentCity is null) return false;
                _context.citys.Remove(currentCity);
                _context.SaveChanges();
                return true;
            }
        }
        #endregion

        #region add
        public bool AddCity(string provinceUnique, string cityName)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var province = _context.provinces.FirstOrDefault(l => l.Unique == provinceUnique);
                if (province is null) return false;

                var cityUnique = GetNewCityUniqueCode(province);
                var newCity = new citys()
                {
                    Unique = cityUnique,
                    Name = cityName,
                    ProvinceId = province.Id
                };
                _context.citys.Add(newCity);
                _context.SaveChanges();
                return true;
            }
        }
        public string GetNewCityUniqueCode(provinces province)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var provinceUnique = province.Unique;
                var provinceCodeStr = province.Unique.Substring(0, 2);
                var newCityCode = 1;
                var lastCity = _context.citys.Where(l => l.ProvinceId == province.Id).OrderByDescending(l => l.Id).FirstOrDefault();
                if (lastCity != null)
                {
                    var lastCityUnique = lastCity.Unique;
                    var cityCodeStr = lastCityUnique.Substring(2, 2);
                    var cityCode = Convert.ToInt32(cityCodeStr);
                    newCityCode = cityCode + 1;
                }
                var newUnique = newCityCode.ToString();
                if (newUnique.Length == 1) newUnique = "0" + newUnique;
                newUnique = provinceCodeStr + newUnique + "00";
                return newUnique;
            }
        }
        #endregion
    }
}
