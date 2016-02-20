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
            // ReSharper disable once AssignNullToNotNullAttribute
            var list = (ConfigurationManager.GetSection("WorkspaceSetting") as System.Collections.Hashtable)
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

            var vulcanObjectsSettings = ConfigurationManager.GetSection("VulcanObjectsSettings") as VulcanObjectsSettings;
            if (vulcanObjectsSettings != null)
            {
                var vulcanObject = vulcanObjectsSettings.CustomConfiguration.Cast<VulcanObject>().ToList<VulcanObject>();


                foreach (var item in vulcanObject)
                {
                    var configItem = new VulcanConfigurationItems(item);
                    try
                    {
                        Console.WriteLine("Start to Process Object: {0} ", item.Name);
                        var rgx = new Regex(@"([\w\s]*?).([\w\s]*?).([\w\s]*?)");
                        if (rgx.IsMatch(item.Key))
                        {
                            string[] splits = item.Key.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                            configItem.ConnectionName = splits[0];
                            configItem.Table = splits[2];

                            switch (splits[1].ToUpper())
                            {
                                case "DIM":
                                    configItem.FileName = workspace + @"\Dimensions\Dim" + configItem.Table + ".xml";
                                    configItem.Schema = "Dimension";
                                    break;
                                case "FACT":
                                    configItem.FileName = workspace + @"\FactTables\Fact" + configItem.Table + ".xml";
                                    configItem.Schema = "Fact";
                                    break;
                                default:
                                    configItem.FileName = workspace + @"\Tables\Staging" + configItem.Table + ".xml";
                                    configItem.Schema = "Table";
                                    break;
                            }
                            WriteXml(GetData(configItem.Schema, configItem.Schema, configItem.Table, configItem.ConnectionName), configItem.FileName);
                        }
                        else
                        {
                            if (configItem.IsItemConfigured())
                            {
                                WriteXml(GetData(configItem.Schema, configItem.Schema, configItem.Table, configItem.ConnectionName), configItem.FileName);
                            }
                            else
                            {
                                var e = new Exception("Please provide enough config information!");
                                throw e;
                            }

                        }


                        Console.WriteLine("Successfully Processed Object: {0}", configItem.Table);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Failed to Process Object: {0} \n Details: {0}", ex.Message);
                        Console.WriteLine("End to Process Object: {0} \n", configItem.Table);
                    }

                }
            }
            Console.WriteLine("-----------------------   Processing End --------------------- \n");
            Console.WriteLine("Enter any key to close the window");
            Console.ReadKey();

        }

        private static string GetData(string type, string schemaName, string name, string connectionName)
        {
            var result = "";
            var connstr = ConfigurationManager.ConnectionStrings[connectionName].ToString();
            var columns = "<Columns> \n" + TableToString(GetQueryFromFile(GetColumnsQueryPath).Replace("@Schema", schemaName).Replace("@Table", name), connstr) + "</Columns>";
            string primaryKey;
            string uniqueKey;
            string index;
            string customExension;

            try
            {
                primaryKey = TableToString(GetQueryFromFile(GetPrimaryKeysQueryPath).Replace("@Schema", schemaName).Replace("@Table", name), connstr);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed when excute query in {0}. \n Details:{1}", GetPrimaryKeysQueryPath, e.Message);
                throw;
            }
            try
            {
                uniqueKey = TableToString(GetQueryFromFile(GetUniqueKeysQueryPath).Replace("@Schema", schemaName).Replace("@Table", name), connstr);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed when excute query in {0}. \n Details:{1}", GetUniqueKeysQueryPath, e.Message);
                throw;
            }
            var keys = "<Keys>\n" + primaryKey + uniqueKey + "\n</Keys>";
            if (primaryKey == "" && uniqueKey == "")
            {
                keys = "";
            }

            try
            {
                index = TableToString(GetQueryFromFile(GetIndexesQueryPath).Replace("@Schema", schemaName).Replace("@Table", name), connstr);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed when excute query in {0}. \n Details:{1}", GetIndexesQueryPath, e.Message);
                throw;
            }
            var indexes = "<Indexes>\n" + index + "\n</Indexes>";
            if (index == "")
            {
                indexes = "";
            }

            try
            {
                customExension = TableToString(GetQueryFromFile(GetCustomExtensionQueryPath).Replace("@Schema", schemaName).Replace("@Table", name), connstr);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed when excute query in {0}. \n Details:{1}", GetCustomExtensionQueryPath, e.Message);
                throw;
            }
            var customExensions = "<CustomExtensions Name=\"Alter Keys\">\n <Tasks>\n <ExecuteSQL Name=\"Custom Update\" ConnectionName=\"" +
                connectionName + "\">\n <Query QueryType=\"Standard\">\n <Body>\n" +
                customExension + "\n</Body> </Query>\n </ExecuteSQL>\n </Tasks>\n </CustomExtensions>";
            if (customExension == "")
            {
                customExensions = "";
            }

            var vulcanHeader = "<?xml version=\"1.0\" encoding=\"utf-8\"?> \n <Vulcan xmlns=\"http://tempuri.org/vulcan2.xsd\"> \n <@Types> \n <@Type Name=\"@Table\" SchemaName=\"@Schema\" ConnectionName=\"@ConnectionName\" EmitVersionNumber=\"false\"> \n"
                .Replace("@Type", type).Replace("@Schema", schemaName).Replace("@Table", name).Replace("@ConnectionName", connectionName);
            var vulcanFooter = "\n </@Type> \n </@Types> \n </Vulcan>".Replace("@Type", type);
            result = vulcanHeader + columns + "\n" + keys + "\n" + indexes + "\n" + customExensions + vulcanFooter;
            return result;
        }

        static readonly string GetColumnsQueryPath = AppDomain.CurrentDomain.BaseDirectory + @"GetColumnsQuery.txt";
        static readonly string GetPrimaryKeysQueryPath = AppDomain.CurrentDomain.BaseDirectory + @"GetPrimaryKeysQuery.txt";
        static readonly string GetCustomExtensionQueryPath = AppDomain.CurrentDomain.BaseDirectory + @"GetCustomExtensionQuery.txt";
        static readonly string GetUniqueKeysQueryPath = AppDomain.CurrentDomain.BaseDirectory + @"GetUniqueKeysQuery.txt";
        static readonly string GetIndexesQueryPath = AppDomain.CurrentDomain.BaseDirectory + @"GetIndexesQuery.txt";

        private static void WriteXml(string data, string xmlPath)
        {
            var doc = XElement.Parse(data);
            doc.Save(xmlPath);
        }
        static string GetQueryFromFile(string fileName)
        {
            try
            {
                using (var sr = new StreamReader(fileName))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to Open query configuration file.\n Details:{0}", e.Message);
                throw;
            }

        }
        static string TableToString(string query, string connstr)
        {

            var dt = new DataTable();
            try
            {
                using (var conn = new SqlConnection(connstr))
                {
                    conn.Open();
                    var dap = new SqlDataAdapter(query, connstr);
                    dap.Fill(dt);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to connect to the Database for retriving the data!  Details:{0}", e.Message);
                //\n Please check your connection setting and server running status.\n
                throw e;
            }

            return string.Join("\n", dt.AsEnumerable().Select(p => p[0].ToString()));

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
            var dir = new DirectoryInfo(sourceDirName);
            var dirs = dir.GetDirectories();

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
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                var temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (!copySubDirs) return;
            foreach (var subdir in dirs)
            {
                var temppath = Path.Combine(destDirName, subdir.Name);
                DirectoryCopy(subdir.FullName, temppath, true);
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
