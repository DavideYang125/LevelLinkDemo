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
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var provinces = _context.provinces.ToList().OrderBy(l => l.Unique).ToList();
                return provinces;
            }
        }
        public provinces GetProvince(string provinceUnique)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var province = _context.provinces.ToList().FirstOrDefault(l => l.Unique == provinceUnique);
                return province;
            }
        }
        #region update
        public bool UpdateProvinceeName(string unique, string name)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var currentProvince = _context.provinces.FirstOrDefault(l => l.Unique == unique);
                if (currentProvince is null) return false;
                currentProvince.Name = name;
                _context.SaveChanges();
                return true;
            }
        }
        public bool UpdateProvinceName(provinces province)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var currentProvince = _context.provinces.FirstOrDefault(l => l.Unique == province.Unique);
                if (currentProvince is null) return false;
                currentProvince.Name = province.Name;
                _context.SaveChanges();
                return true;
            }
        }
        #endregion

        #region delete
        public bool DeleteProvince(string unique)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
            {
                var currentProvince = _context.provinces.FirstOrDefault(l => l.Unique == unique);
                if (currentProvince is null) return false;
                _context.provinces.Remove(currentProvince);
                _context.SaveChanges();
                return true;
            }
        }
        #endregion

        #region add
        public bool AddProvince(string provinceName)
        {
            using (deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities())
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
                var province = _context.provinces.FirstOrDefault(l => l.Id == provinceId);
                province.Unique = provinceCodeStr + "0000";
                _context.SaveChanges();
                return true;
            }
        }
        #endregion
    }
}
