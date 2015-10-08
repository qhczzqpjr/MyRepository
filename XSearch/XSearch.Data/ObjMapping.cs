using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
