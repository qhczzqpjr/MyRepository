using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace TableToVulcan
{

    public class VulcanObjectsSettings : ConfigurationSection
    {
        [ConfigurationProperty("VulcanObjects")]
        public VulcanObjects CustomConfiguration
        {
            get { return (VulcanObjects)base["VulcanObjects"] ?? new VulcanObjects(); }
        }
    }

    public class VulcanObjects : ConfigurationElementCollection
    {

        public VulcanObject this[int idx]
        {
            get { return (VulcanObject)BaseGet(idx); }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new VulcanObject();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((VulcanObject)element).Key;
        }
    }

    public class VulcanObject : ConfigurationElement
    {
        [ConfigurationProperty("Key", IsKey = true, IsRequired = true)]
        public string Key
        {
            get
            {
                string value = base["Key"] as String;
                
                return value ?? String.Empty;
            }
        }
        [ConfigurationProperty("Type")]
        public string Type
        {
            get
            {
                string value = base["Type"] as String;

                return value ?? String.Empty;
            }
        }

        [ConfigurationProperty("Schema")]
        public string SchemaName
        {
            get
            {
                string value = base["Schema"] as String;

                return value ?? String.Empty;
            }
        }
        [ConfigurationProperty("TableName")]
        public string Name
        {
            get
            {
                string value = base["TableName"] as String;

                return value ?? String.Empty;
            }
        }
        [ConfigurationProperty("ConnectionName")]
        public string ConnectionName
        {
            get
            {
                string value = base["ConnectionName"] as String;

                return value ?? String.Empty;
            }
        }
        [ConfigurationProperty("FileName")]
        public string FileName
        {
            get
            {
                string value = base["FileName"] as String;

                return value ?? String.Empty;
            }
        }

    }

}
