using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class Requirement
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Context { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            var objAsRequirement = obj as Requirement;
            return objAsRequirement != null && Equals(objAsRequirement);
        }
        protected bool Equals(Requirement other)
        {
            return string.Equals(Code, other.Code);
        }

        public override int GetHashCode()
        {
            return (Code != null ? Code.GetHashCode() : 0);
        }
    }
}
