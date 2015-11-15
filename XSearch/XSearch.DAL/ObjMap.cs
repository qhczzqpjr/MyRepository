using System.Data.Entity.ModelConfiguration;
using XSearch.Data;

namespace XSearch
{
    public class ObjMap : EntityTypeConfiguration<Obj>
    {
        public ObjMap()
        {
            HasKey(t => t.Id);
        }
    }
}
