namespace XSearch.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using XSearch.DAL;
    using XSearch.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<XSearchDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //ContextKey = "XSearch.DAL.XSearchDBContext";
        }

        protected override void Seed(XSearch.DAL.XSearchDBContext context)
        {
            context.Objs.AddOrUpdate(new Obj { Key = "ct", Value = "CREATE TABLE" });
        }
    }
}
