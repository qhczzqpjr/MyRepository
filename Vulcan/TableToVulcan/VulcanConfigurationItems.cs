using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableToVulcan
{
    public class VulcanConfigurationItems
    {
        public string Key { get; set; }
        public string ConnectionName { get; set; }
        public string Schema { get; set; }
        public string Table { get; set; }
        public string FileName { get; set; }
        public VulcanConfigurationItems(VulcanObject obj) 
        {
            this.Key = obj.Key;
            this.ConnectionName = obj.ConnectionName;
            this.Schema = obj.SchemaName;
            this.Table = obj.Name;
            this.FileName = obj.FileName;
        }
        public bool IsItemConfigured()
        {
            return !string.IsNullOrEmpty(this.Schema) && !string.IsNullOrEmpty(this.Table) && !string.IsNullOrEmpty(this.ConnectionName) && !string.IsNullOrEmpty(this.FileName);
        }
    }
}
