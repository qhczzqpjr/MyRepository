using System;
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
        Text = 0,
        Cmd = 1,
        File = 2,
    }
}
