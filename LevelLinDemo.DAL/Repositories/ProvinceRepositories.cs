using LevelLinDemo.DAL.Edmx;
using System.Collections.Generic;
using System.Linq;

namespace LevelLinDemo.DAL.Repositories
{
    public class ProvinceRepositories
    {
        private deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities();

        public List<provinces> GetProvinces()
        {
            var provinces = _context.provinces.ToList().OrderBy(l => l.Unique).ToList();
            return provinces;
        }
        public List<provinces> GetProvincesOfProvince(string provinceUnique)
        {
            var provinces = _context.provinces.ToList().Where(l => l.Unique == provinceUnique).OrderBy(l => l.Unique).ToList();
            return provinces;
        }
        #region update
        public bool UpdateProvinceeName(string unique, string name)
        {
            var currentProvince = _context.provinces.FirstOrDefault(l => l.Unique == unique);
            if (currentProvince is null) return false;
            currentProvince.Name = name;
            _context.SaveChanges();
            return true;
        }
        public bool UpdateProvinceName(provinces province)
        {
            var currentProvince = _context.provinces.FirstOrDefault(l => l.Unique == province.Unique);
            if (currentProvince is null) return false;
            currentProvince.Name = province.Name;
            _context.SaveChanges();
            return true;
        }
        #endregion

        #region delete
        public bool DeleteProvince(string unique)
        {
            var currentProvince = _context.provinces.FirstOrDefault(l => l.Unique == unique);
            if (currentProvince is null) return false;
            _context.provinces.Remove(currentProvince);
            _context.SaveChanges();
            return true;
        }
        #endregion

        #region add
        public bool AddProvince(string provinceName)
        {
            var newProvince = new provinces()
            {
                Name = provinceName,
            };
            _context.provinces.Add(newProvince);
            _context.SaveChanges();
            var provinceId = newProvince.Id;
            var provinceCodeStr = provinceId.ToString();
            if (provinceCodeStr.Length == 1) provinceCodeStr = "0" + provinceCodeStr;
            newProvince.Unique = provinceCodeStr + "0000";
            _context.SaveChanges();
            return true;
        }
        #endregion
    }
}
