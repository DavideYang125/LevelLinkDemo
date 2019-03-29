using LevelLinDemo.DAL.Edmx;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LevelLinDemo.DAL.Repositories
{
    public class CityRepositories
    {
        private deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities();

        public List<citys> GetCitys()
        {
            var citys = _context.citys.ToList().OrderBy(l => l.Unique).ToList();
            return citys;
        }
        public List<citys> GetCitysOfProvince(string provinceUnique)
        {
            var citys = _context.citys.ToList().Where(l => l.Unique == provinceUnique).OrderBy(l => l.Unique).ToList();
            return citys;
        }
        #region update
        public bool UpdateCityName(string unique, string name)
        {
            var currentCity = _context.citys.FirstOrDefault(l => l.Unique == unique);
            if (currentCity is null) return false;
            currentCity.Name = name;
            _context.SaveChanges();
            return true;
        }
        public bool UpdateCityName(citys city)
        {
            var currentCity = _context.citys.FirstOrDefault(l => l.Unique == city.Unique);
            if (currentCity is null) return false;
            currentCity.Name = city.Name;
            _context.SaveChanges();
            return true;
        }
        #endregion

        #region delete
        public bool DeleteCity(string unique)
        {
            var currentCity = _context.citys.FirstOrDefault(l => l.Unique == unique);
            if (currentCity is null) return false;
            _context.citys.Remove(currentCity);
            _context.SaveChanges();
            return true;
        }
        #endregion

        #region add
        public bool AddCity(string provinceUnique, string cityName)
        {
            var province = _context.provinces.FirstOrDefault(l => l.Unique == provinceUnique);
            if (province is null) return false;
            var newId = 1;
            var lastCity = _context.citys.OrderByDescending(l => l.Id).FirstOrDefault();
            if (lastCity != null)
            {
                var lastId = lastCity.Id;
                newId = lastId + 1;
            }
            var cityUnique = GetNewCityUniqueCode(province);
            var newCity = new citys()
            {
                Id = newId,
                Unique = cityUnique,
                Name = cityName,
                ProvinceId = province.Id
            };
            _context.citys.Add(newCity);
            _context.SaveChanges();
            return true;
        }
        public string GetNewCityUniqueCode(provinces province)
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
        #endregion
    }
}
