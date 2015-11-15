using System;

namespace XSearch.Data
{
    public class ObjMapping
    {
        public int Id { get; set; }
        public virtual Obj LId { get; set; }
        public virtual Obj RId { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
