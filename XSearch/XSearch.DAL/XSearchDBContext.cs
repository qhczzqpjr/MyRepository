using System;
using System.Data.Entity;
using XSearch.Data;
using XSearch.DAL;

namespace XSearch
{
    public class XSearchDbContext : DbContext
    {
        public XSearchDbContext()
            : base("XSearch")
        {
            //Database.SetInitializer<XSearchDBContext>(new DropCreateDatabaseIfModelChanges<XSearchDBContext>());
            Database.SetInitializer(new CreateDatabaseIfNotExists<XSearchDbContext>());
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

    public class SchoolDbInitializer : CreateDatabaseIfNotExists<XSearchDbContext>
    {
        protected override void Seed(XSearchDbContext context)
        {
            if (context == null) throw new ArgumentNullException("context");
            base.Seed(context);
        }
    }

}
