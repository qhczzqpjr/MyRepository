using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XSearch.Data;


namespace XSearch.DAL
{
    public class XSearchDBContext : DbContext
    {
        public XSearchDBContext()
            : base("XSearch")
        {
            //Database.SetInitializer<XSearchDBContext>(new DropCreateDatabaseIfModelChanges<XSearchDBContext>());
            Database.SetInitializer<XSearchDBContext>(new CreateDatabaseIfNotExists<XSearchDBContext>());
        }

        public DbSet<ObjMapping> ObjMappings { get; set; }
        public DbSet<Obj> Objs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new ObjMappingMap());
            modelBuilder.Configurations.Add(new ObjMap());
        }

    }

    public class SchoolDBInitializer : CreateDatabaseIfNotExists<XSearchDBContext>
    {
        protected override void Seed(XSearchDBContext context)
        {
            base.Seed(context);
        }
    }

}
