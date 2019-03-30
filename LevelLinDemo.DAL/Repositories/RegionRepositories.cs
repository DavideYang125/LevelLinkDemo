using LevelLinDemo.DAL.Edmx;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LevelLinDemo.DAL.Repositories
{
    public class RegionRepositories
    {
        private deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities();

        public List<regions> GetRegions()
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var regions = _context.regions.ToList().OrderBy(l => l.Unique).ToList();
                return regions;
            }
        }
        public regions GetRegion(string regionUnique)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var region = _context.regions.ToList().FirstOrDefault(l => l.Unique == regionUnique);
                return region;
            }
        }
        public List<regions> GetRegionsByCityUnique(string cityUnique)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var city = _context.citys.FirstOrDefault(l => l.Unique == cityUnique);
                var regions = _context.regions.ToList().Where(l => l.CityId == city.Id).OrderBy(l => l.Unique).ToList();
                return regions;
            }
        }
        public List<regions> GetRegionsByCityId(int cityId)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var regions = _context.regions.ToList().Where(l => l.CityId == cityId).OrderBy(l => l.Unique).ToList();
                return regions;
            }
        }
        #region update
        public bool UpdateRegionName(string unique, string name)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var currentRegion = _context.regions.FirstOrDefault(l => l.Unique == unique);
                if (currentRegion is null) return false;
                currentRegion.Name = name;
                _context.SaveChanges();
                return true;
            }
        }
        public bool UpdateRegionName(regions region)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var currentRegion = _context.regions.FirstOrDefault(l => l.Unique == region.Unique);
                if (currentRegion is null) return false;
                currentRegion.Name = region.Name;
                _context.SaveChanges();
                return true;
            }
        }
        #endregion

        #region delete
        public bool DeleteRegion(string unique)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var currentRegion = _context.regions.FirstOrDefault(l => l.Unique == unique);
                if (currentRegion is null) return false;
                _context.regions.Remove(currentRegion);
                _context.SaveChanges();
                return true;
            }
        }
        #endregion

        #region add
        public bool AddRegion(string regionName, string cityUnique, string provinceUnique)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var city = _context.citys.FirstOrDefault(l => l.Unique == cityUnique);
                if (city is null) return false;
                var province = _context.provinces.FirstOrDefault(l => l.Unique == provinceUnique);
                if (province is null) return false;
                var newUnique = GetNewRegionUnique(city);
                var newRegion = new regions()
                {
                    CityId = city.Id,
                    Name = regionName,
                    Unique = newUnique
                };
                _context.regions.Add(newRegion);
                _context.SaveChanges();
                return true;
            }
        }
        public string GetNewRegionUnique(citys city)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var cityCodeSrt = city.Unique.Substring(0, 4);
                var newRegionCode = 1;
                var lastRegion = _context.regions.Where(l => l.CityId == city.Id).OrderByDescending(l => l.Id).FirstOrDefault();
                if (lastRegion != null)
                {
                    newRegionCode = Convert.ToInt32(lastRegion.Unique.Substring(4, 2)) + 1;
                }
                var newRegionCodeStr = newRegionCode.ToString();
                if (newRegionCodeStr.Length == 1) newRegionCodeStr = "0" + newRegionCodeStr;
                var newUnique = cityCodeSrt + newRegionCodeStr;
                return newUnique;
            }
        }
        #endregion
    }
}
