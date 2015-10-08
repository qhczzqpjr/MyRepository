using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.ComponentModel.DataAnnotations;
namespace XSearch.Data
{
    public class Obj
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public ObjValueType ValueType { get; set; }
        public DateTime CreateDateTime { get; set; }
    }

    public enum ObjValueType
    {
        text = 0,
        cmd = 1,
        file = 2,
    }
}
