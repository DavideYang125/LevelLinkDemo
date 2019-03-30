using LevelLinDemo.DAL.Edmx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LevelLinkDemo.Web.Models
{
    public class LevelProvince
    {
        public provinces province { get; set; }
        public List<LevelCity> citys { get; set; }
    }
    public class LevelCity
    {
        public citys city { get; set; }
        public List<regions> regions { get; set; }
    }
}