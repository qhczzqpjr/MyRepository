using System;

namespace XSearch.Business.Models
{
    public class Obj
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        //public ObjValueType ValueType { get; set; }
        public DateTime CreateDateTime { get; set; }
    }
}
