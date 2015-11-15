namespace XSearch.Migrations
{
    using System.Data.Entity.Migrations;
    using Data;

    internal sealed class Configuration : DbMigrationsConfiguration<XSearchDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //ContextKey = "XSearch.DAL.XSearchDBContext";
        }

        protected override void Seed(XSearchDbContext context)
        {
            context.Objs.AddOrUpdate(new Obj { Key = "ct", Value = "CREATE TABLE" });
        }
    }
}
