using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
