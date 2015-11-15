using System;

namespace XSearch.Business.Models
{
    public class ObjMapping
    {
        public int Id { get; set; }
        public Obj LId { get; set; }
        public Obj RId { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
