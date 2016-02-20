using System.Data.Entity;
using Data;

namespace Repository
{
    public class XSearchDbContent : DbContext
    {
        public XSearchDbContent() : base("XSearch")
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<XSearchDbContent>());//Only for testing  
        }

        public DbSet<Requirement> Requirements { get; set; }
    }
}