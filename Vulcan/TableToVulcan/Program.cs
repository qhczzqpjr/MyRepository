using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Configuration;
using System.Text.RegularExpressions;

namespace TableToVulcan
{
    class Program
    {
        static void Main(string[] args)
        {

            string workspace = ConfigurationManager.AppSettings["Workspace"];
            Console.WriteLine("-------------------- Welcome to TableToVulcan ------------------");
            Dictionary<string, string> list = (ConfigurationManager.GetSection("WorkspaceSetting") as System.Collections.Hashtable)
               .Cast<System.Collections.DictionaryEntry>()
               .ToDictionary(n => n.Key.ToString(), n => n.Value.ToString());

            if (list["CopyTemplate"] == "1")
            {
                Console.WriteLine("-----------------------   FolderCopy Start --------------------- \n");
                try
                {
                    DirectoryCopy(list["TemplatePath"], workspace, true);
                    Console.WriteLine("-----------------------   FolderCopy Success --------------------- \n");
                }
                catch
                {
                    Console.WriteLine("-----------------------   FolderCopy Failed --------------------- \n");
                }
                Console.WriteLine("-----------------------   FolderCopy End --------------------- \n");
            }

            Console.WriteLine("-----------------------   Processing Start --------------------- \n");

            var VulcanObject = (ConfigurationManager.GetSection("VulcanObjectsSettings") as VulcanObjectsSettings).CustomConfiguration.Cast<VulcanObject>().ToList<VulcanObject>();


            foreach (VulcanObject item in VulcanObject)
            {
                VulcanConfigurationItems ConfigItem = new VulcanConfigurationItems(item);
                try
                {
                    Console.WriteLine("Start to Process Object: {0} ", item.Name);
                    Regex rgx = new Regex(@"([\w\s]*?).([\w\s]*?).([\w\s]*?)");
                    if (rgx.IsMatch(item.Key))
                    {
                        string[] splits = item.Key.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                        ConfigItem.ConnectionName = splits[0];
                        ConfigItem.Table = splits[2];

                        switch (splits[1].ToUpper())
                        {
                            case "DIM":
                                ConfigItem.FileName = workspace + @"\Dimensions\Dim" + ConfigItem.Table + ".xml";
                                ConfigItem.Schema = "Dimension";
                                break;
                            case "FACT":
                                ConfigItem.FileName = workspace + @"\FactTables\Fact" + ConfigItem.Table + ".xml";
                                ConfigItem.Schema = "Fact";
                                break;
                            default:
                                ConfigItem.FileName = workspace + @"\Tables\Staging" + ConfigItem.Table + ".xml";
                                ConfigItem.Schema = "Table";
                                break;
                        }
                        WriteXML(GetData(ConfigItem.Schema, ConfigItem.Schema, ConfigItem.Table, ConfigItem.ConnectionName), ConfigItem.FileName);
                    }
                    else
                    {
                        if (ConfigItem.IsItemConfigured())
                        {
                            WriteXML(GetData(ConfigItem.Schema, ConfigItem.Schema, ConfigItem.Table, ConfigItem.ConnectionName), ConfigItem.FileName);
                        }
                        else
                        {
                            Exception e = new Exception("Please provide enough config information!");
                            throw e;
                        }

                    }


                    Console.WriteLine("Successfully Processed Object: {0}", ConfigItem.Table);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to Process Object: {0} \n Details: {0}", ex.Message);
                    Console.WriteLine("End to Process Object: {0} \n", ConfigItem.Table);
                }

            }
            Console.WriteLine("-----------------------   Processing End --------------------- \n");
            Console.WriteLine("Enter any key to close the window");
            Console.ReadKey();

        }

        private static string GetData(string Type, string SchemaName, string Name, string ConnectionName)
        {
            string result = "";
            string connstr = ConfigurationManager.ConnectionStrings[ConnectionName].ToString();
            string columns = "<Columns> \n" + TableToString(GetQueryFromFile(GetColumnsQueryPath).Replace("@Schema", SchemaName).Replace("@Table", Name), connstr) + "</Columns>";
            string PrimaryKey, UniqueKey, Index, CustomExension;

            try
            {
                PrimaryKey = TableToString(GetQueryFromFile(GetPrimaryKeysQueryPath).Replace("@Schema", SchemaName).Replace("@Table", Name), connstr);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed when excute query in {0}. \n Details:{1}", GetPrimaryKeysQueryPath, e.Message);
                throw e;
            }
            try
            {
                UniqueKey = TableToString(GetQueryFromFile(GetUniqueKeysQueryPath).Replace("@Schema", SchemaName).Replace("@Table", Name), connstr);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed when excute query in {0}. \n Details:{1}", GetUniqueKeysQueryPath, e.Message);
                throw e;
            }
            string keys = "<Keys>\n" + PrimaryKey + UniqueKey + "\n</Keys>";
            if (PrimaryKey == "" && UniqueKey == "")
            {
                keys = "";
            }

            try
            {
                Index = TableToString(GetQueryFromFile(GetIndexesQueryPath).Replace("@Schema", SchemaName).Replace("@Table", Name), connstr);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed when excute query in {0}. \n Details:{1}", GetIndexesQueryPath, e.Message);
                throw e;
            }
            string indexes = "<Indexes>\n" + Index + "\n</Indexes>";
            if (Index == "")
            {
                indexes = "";
            }

            try
            {
                CustomExension = TableToString(GetQueryFromFile(GetCustomExtensionQueryPath).Replace("@Schema", SchemaName).Replace("@Table", Name), connstr);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed when excute query in {0}. \n Details:{1}", GetCustomExtensionQueryPath, e.Message);
                throw e;
            }
            string customExensions = "<CustomExtensions Name=\"Alter Keys\">\n <Tasks>\n <ExecuteSQL Name=\"Custom Update\" ConnectionName=\"" +
                ConnectionName + "\">\n <Query QueryType=\"Standard\">\n <Body>\n" +
                CustomExension + "\n</Body> </Query>\n </ExecuteSQL>\n </Tasks>\n </CustomExtensions>";
            if (CustomExension == "")
            {
                customExensions = "";
            }

            string vulcanHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?> \n <Vulcan xmlns=\"http://tempuri.org/vulcan2.xsd\"> \n <@Types> \n <@Type Name=\"@Table\" SchemaName=\"@Schema\" ConnectionName=\"@ConnectionName\" EmitVersionNumber=\"false\"> \n"
                .Replace("@Type", Type).Replace("@Schema", SchemaName).Replace("@Table", Name).Replace("@ConnectionName", ConnectionName);
            string vulcanFooter = "\n </@Type> \n </@Types> \n </Vulcan>".Replace("@Type", Type);
            result = vulcanHeader + columns + "\n" + keys + "\n" + indexes + "\n" + customExensions + vulcanFooter;
            return result;
        }

        static readonly string GetColumnsQueryPath = AppDomain.CurrentDomain.BaseDirectory + @"GetColumnsQuery.txt";
        static readonly string GetPrimaryKeysQueryPath = AppDomain.CurrentDomain.BaseDirectory + @"GetPrimaryKeysQuery.txt";
        static readonly string GetCustomExtensionQueryPath = AppDomain.CurrentDomain.BaseDirectory + @"GetCustomExtensionQuery.txt";
        static readonly string GetUniqueKeysQueryPath = AppDomain.CurrentDomain.BaseDirectory + @"GetUniqueKeysQuery.txt";
        static readonly string GetIndexesQueryPath = AppDomain.CurrentDomain.BaseDirectory + @"GetIndexesQuery.txt";

        private static void WriteXML(string Data, string xmlPath)
        {
            XElement doc = XElement.Parse(Data);
            doc.Save(xmlPath);
        }
        static string GetQueryFromFile(string FileName)
        {
            try
            {
                using (StreamReader sr = new StreamReader(FileName))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Open query configuration file.\n Details:{0}", e.Message);
                throw e;
            }

        }
        static string TableToString(string query, string connstr)
        {

            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                {
                    conn.Open();
                    SqlDataAdapter dap = new SqlDataAdapter(query, connstr);
                    dap.Fill(dt);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to connect to the Database for retriving the data!  Details:{0}", e.Message);
                //\n Please check your connection setting and server running status.\n
                throw e;
            }

            return String.Join("\n", dt.AsEnumerable().Select(p => p[0].ToString()));

        }

        /// <summary>
        /// CopyDirectory
        /// </summary>
        /// <param name="sourceDirName"></param>
        /// <param name="destDirName"></param>
        /// <param name="copySubDirs"></param>
        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        enum VulcanType
        {
            Table = 0,
            Fact = 1,
            Dimension = 2
        }

    }
}
