using LevelLinDemo.DAL.Edmx;
using System;
using System.Linq;

namespace LevelLinkDemo.Consoles
{
    class Program
    {
        private static deyouyun_levellink_devEntities _context = new deyouyun_levellink_devEntities();
        static void Main(string[] args)
        {
            var provinces = _context.provinces.ToList().Where(l => l.Unique.Length > 6).ToList();
            var citys = _context.citys.ToList().Where(l => l.Unique.Length > 6).ToList();
            var regions = _context.regions.ToList().Where(l => l.Unique.Length > 6).ToList();

            //UpdateUnique();
        }
        public static void UpdateUnique()
        {
            UpdateUniqueForProvinces();
            Console.WriteLine("province unique is updated");
            Console.ReadKey();
            UpdateUniqueForCitys();
            Console.WriteLine("citys unique is updated");
            Console.ReadKey();
            UpdateUniqueForRegion();
            Console.WriteLine("regions unique is updated");
        }
        /// <summary>
        /// update unique for province
        /// </summary>
        public static void UpdateUniqueForProvinces()
        {
            var provinces = _context.provinces.ToList();
            foreach (var province in provinces)
            {
                Console.WriteLine(province.Name);
                var provinceId = province.Id;
                var idStr = GetFullNumStr(provinceId);
                var unique = idStr + "0000";
                province.Unique = unique;
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// update unique for city
        /// </summary>
        public static void UpdateUniqueForCitys()
        {
            var provinces = _context.provinces.ToList();
            var citys = _context.citys.ToList();
            foreach (var province in provinces)
            {
                var provinceId = province.Id;
                var provinceIdStr = GetFullNumStr(provinceId);
                var currentCitys = citys.Where(c => c.ProvinceId == provinceId).OrderBy(c => c.Id).ToList();
                int index = 0;
                foreach (var currentCity in currentCitys)
                {
                    Console.WriteLine(currentCity.Name);
                    index++;
                    var cityIndexStr = GetFullNumStr(index);
                    var cityUnique = provinceIdStr + cityIndexStr + "00";
                    currentCity.Unique = cityUnique;
                }
            }
            _context.SaveChanges();
        }
        /// <summary>
        /// update unique for region
        /// </summary>
        public static void UpdateUniqueForRegion()
        {
            var provinces = _context.provinces.ToList();
            var citys = _context.citys.ToList();
            var regions = _context.regions.ToList();
            foreach (var province in provinces)
            {
                var provinceId = province.Id;
                var provinceIdStr = GetFullNumStr(provinceId);
                var currentCitys = citys.Where(l => l.ProvinceId == provinceId).OrderBy(l => l.Id).ToList();

                foreach (var currentCity in currentCitys)
                {
                    var cityUnique = currentCity.Unique;
                    var cityIdStr = cityUnique.Substring(2, 2);
                    var currentRegions = regions.Where(l => l.CityId == currentCity.Id).OrderBy(l => l.Id);
                    int index = 0;
                    foreach (var currentRegion in currentRegions)
                    {
                        Console.WriteLine(currentRegion.Name);
                        index++;
                        var regionIndexStr = GetFullNumStr(index);
                        var regionUnique = provinceIdStr + cityIdStr + regionIndexStr;
                        currentRegion.Unique = regionUnique;
                    }
                }
            }
            _context.SaveChanges();
        }

        public static string GetFullNumStr(int num)
        {
            if(num.ToString().Length>6)
            {
                Console.WriteLine(num + "---length is greater than 6");
                Console.ReadKey();
            }
            var str = num.ToString();
            if (str.Length == 1) str = "0" + str;
            return str;
        }
    }
}
