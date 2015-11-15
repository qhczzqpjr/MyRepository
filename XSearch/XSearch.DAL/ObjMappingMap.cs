using System.Data.Entity.ModelConfiguration;
using XSearch.Data;

namespace XSearch
{
    public class ObjMappingMap : EntityTypeConfiguration<ObjMapping>
    {
        public ObjMappingMap()
        {
            HasKey(t => t.Id);
        }
    }
}
