﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LevelLinDemo.DAL.Edmx
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class deyouyun_levellink_devEntities : DbContext
    {
        public deyouyun_levellink_devEntities()
            : base("name=deyouyun_levellink_devEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<citys> citys { get; set; }
        public virtual DbSet<provinces> provinces { get; set; }
        public virtual DbSet<regions> regions { get; set; }
    }
}
