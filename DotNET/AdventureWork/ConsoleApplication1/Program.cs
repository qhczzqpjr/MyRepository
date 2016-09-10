using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Security.Cryptography;
using LinqToExcel;
using LinqToExcel.Domain;
using LinqToExcel.Query;
using Excel = Microsoft.Office.Interop.Excel;
using DataTable = System.Data.DataTable;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            query_table_getdata();
        }

        public static void query_table_getdata()
        {
            var sourcePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Test.xlsx");
            var desPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Test1.xlsx");

            Excel.Application mObjExcel = null;
            mObjExcel = new Excel.Application();
            mObjExcel.AlertBeforeOverwriting = false;
            mObjExcel.DisplayAlerts = false;
            
            try
            {
                var mObjBooks = mObjExcel.Workbooks;
                mObjBooks.Open(sourcePath, Type.Missing,true, Type.Missing, Type.Missing,
               Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

                var mObjBook = mObjBooks.Item[1];


                var mObjSheets = mObjBook.Worksheets;
                var mObjSheet = (Excel.Worksheet)mObjSheets.Item[1];
                var mObjRange = mObjSheet.Range["A3", Type.Missing];
                var mObjQryTables = mObjSheet.QueryTables;
                var sqlstr = "SELECT * FROM Info";
                var conn = @"Provider=SQLOLEDB.1;Data Source=.;Integrated Security=SSPI; Initial Catalog=Me;Persist Security Info=False;";

                var mObjQryTable = mObjQryTables.Add("OLEDB;" + conn, mObjRange, sqlstr);

                mObjQryTable.RefreshStyle = Excel.XlCellInsertionMode.xlOverwriteCells;

                mObjQryTable.Refresh(false);

                mObjBook.SaveAs(desPath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                mObjBook.Close(false, Type.Missing, Type.Missing);
            }
            catch (Exception ee)
            {
                // ignored
            }
            finally
            {
                mObjExcel.Quit();
                GC.Collect();
            }

        }

        private static void ReturnSqlToString()
        {
            var connStr = "Data Source=.;Initial Catalog=Me;Integrated Security=SSPI; ";
            var cmdTest = "select * from info";
            var datatable = new DataTable();
            using (var conn = new SqlConnection(connStr))
            {
                conn.Open();
                var sqlcommand = new SqlCommand(cmdTest, conn);
                var sqlDataAdapter = new SqlDataAdapter(sqlcommand);
                sqlDataAdapter.Fill(datatable);
            }

            var result = datatable.Rows.Cast<DataRow>()
                .Aggregate("", (current, row) => current + row[0].ToString() + Environment.NewLine);
        }

        private static void TestCase10_LinqToExcel()
        {
            //https://github.com/paulyoder/LinqToExcel
            var table = ExcelQueryFactory.Worksheet("Info",
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Test.xlsx"));
            var excelQueryFactory =
                new ExcelQueryFactory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    "Test.xlsx"))
                {
                    UsePersistentConnection = true,
                    ReadOnly = true,
                    DatabaseEngine = DatabaseEngine.Ace,
                    TrimSpaces = TrimSpacesType.Both,
                    StrictMapping = StrictMappingType.Both
                };
            var columnNames = excelQueryFactory.GetColumnNames("Info");
            try
            {
                var infos = excelQueryFactory.Worksheet<Information>("Info").Where(p => p.Status == 1);

                var Num = excelQueryFactory.WorksheetRange("A3", "B103", "Sheet1");
            }
            finally
            {
                excelQueryFactory.Dispose();
            }
        }

        private static void TestCase9_WebAutomation()
        {
            var uri = new Uri("http://pan.baidu.com");

            var request = WebRequest.CreateHttp(uri);
            request.Method = "Post";
            //request.UseDefaultCredentials = true;
            CookieContainer cc = new CookieContainer();
            string postData = "User=qhczzqpjr&pass=zhuqian@?";
            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            request.CookieContainer = cc;
            request.ContentLength = bytes.Length;
            request.ContentType = "application/x-www-form-urlencoded";
            var newStream = request.GetRequestStream();
            newStream.Write(bytes, 0, bytes.Length);
            newStream.Close();
            using (var response = request.GetResponse())

            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.Write(result);
            }
        }

        private static void TestCast8_AutoMapper()
        {
            AutoMapper.Mapper.CreateMap<AutoMapDto, AutoMap>();
            AutoMapper.Mapper.CreateMap<AutoMap, AutoMapDto>();
            AutoMapDto dto = new AutoMapDto() { Code = "anything", Context = "hhere" };
            var requirement = AutoMapper.Mapper.Map<AutoMapDto, AutoMap>(dto);
        }


        private static void TestCast7_GetIPAddress()
        {
            GetPrivateIp();
            GetPublicIp();
        }

        static void GetPrivateIp()
        {
            foreach (var address in NetworkInterface.GetAllNetworkInterfaces().SelectMany(interfaces => interfaces.GetIPProperties().UnicastAddresses.Where(address => address.Address.AddressFamily == AddressFamily.InterNetwork)))
            {
                Console.WriteLine("IP Address: " + address.Address);
            }
        }

        static void GetPublicIp()
        {
            var address = "";
            var request = WebRequest.Create("http://checkip.dyndns.com/");
            using (var response = request.GetResponse())
            using (var stream = new StreamReader(response.GetResponseStream()))
            {
                address = stream.ReadToEnd();
            }
            var first = address.IndexOf("Address: ", StringComparison.Ordinal) + 9;
            var last = address.LastIndexOf("</body>", StringComparison.Ordinal);
            address = address.Substring(first, last - first);
            Console.WriteLine("IP Address: " + address);
        }

        private static void TestCase_Encrypt_Dncrypt()
        {
            try
            {
                // Create a new TripleDESCryptoServiceProvider object
                // to generate a key and initialization vector (IV).
                TripleDESCryptoServiceProvider tDESalg = new TripleDESCryptoServiceProvider();

                // Create a string to encrypt.
                string sData = "Here is some data to encrypt.";
                string FileName = "CText.txt";

                // Encrypt text to a file using the file name, key, and IV.
                EncryptTextToFile(sData, FileName, tDESalg.Key, tDESalg.IV);

                // Decrypt the text from a file using the file name, key, and IV.
                string Final = DecryptTextFromFile(FileName, tDESalg.Key, tDESalg.IV);

                // Display the decrypted string to the console.
                Console.WriteLine(Final);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void EncryptTextToFile(String Data, String FileName, byte[] Key, byte[] IV)
        {
            try
            {
                // Create or open the specified file.
                FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate);

                // Create a CryptoStream using the FileStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(fStream,
                    new TripleDESCryptoServiceProvider().CreateEncryptor(Key, IV),
                    CryptoStreamMode.Write);

                // Create a StreamWriter using the CryptoStream.
                StreamWriter sWriter = new StreamWriter(cStream);

                // Write the data to the stream 
                // to encrypt it.
                sWriter.WriteLine(Data);

                // Close the streams and
                // close the file.
                sWriter.Close();
                cStream.Close();
                fStream.Close();
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file access error occurred: {0}", e.Message);
            }

        }

        public static string DecryptTextFromFile(String FileName, byte[] Key, byte[] IV)
        {
            try
            {
                // Create or open the specified file. 
                FileStream fStream = File.Open(FileName, FileMode.OpenOrCreate);

                // Create a CryptoStream using the FileStream 
                // and the passed key and initialization vector (IV).
                CryptoStream cStream = new CryptoStream(fStream,
                    new TripleDESCryptoServiceProvider().CreateDecryptor(Key, IV),
                    CryptoStreamMode.Read);

                // Create a StreamReader using the CryptoStream.
                StreamReader sReader = new StreamReader(cStream);

                // Read the data from the stream 
                // to decrypt it.
                string val = sReader.ReadLine();

                // Close the streams and
                // close the file.
                sReader.Close();
                cStream.Close();
                fStream.Close();

                // Return the string. 
                return val;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file access error occurred: {0}", e.Message);
                return null;
            }
        }
        private static void TestCase4()
        {
            var data = new List<string>();
            Test(data);
        }

        private static void TestCast3()
        {
            var workingdirectory = @"G:\GitHub\MyRepository\Test";
            var data = Path.GetInvalidFileNameChars();
            if (workingdirectory.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                throw new ArgumentException();
            }
            ;


            var repository = FileManagementRepository.Instance;
            repository.Init(workingdirectory);
            repository.Insert(new Requirement() { Code = "test", Context = "Hello System" });
            var requirement = repository.GetRequirement("test");

            repository.Delete(new Requirement() { Code = "test", Context = "Hello System" });
            var requirement2 = repository.GetRequirement("test");
            Console.ReadKey();
        }

        private static void Test(ICollection<string> test)
        {
            Console.WriteLine("Hello");
        }

        private static void TestCast2()
        {
            var result = CheckPowOfTwo(14);
            var data1 = IntToBinaryString(14);
            var data2 = IntToBinaryString(13);
            Console.WriteLine(result);
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            Console.WriteLine(Path.GetDirectoryName(path));
        }

        static bool CheckPowOfTwo(ulong number)
        {
            return (number != 0) && ((number & (number - 1)) == 0);
        }

        static string IntToBinaryString(int number)
        {
            const int mask = 1;
            var binary = string.Empty;
            while (number > 0)
            {
                // Logical AND the number and prepend it to the result string
                binary = (number & 1) + binary;
                number = number >> 1;
            }

            return binary;
        }

        private static void TestCase1()
        {
            //const string filepath = @"C:\Users\v-thzhu\Desktop\Test.txt";
            var filepath = AppDomain.CurrentDomain.BaseDirectory + @"Queries\Test.sql";
            var connection = AppDomain.CurrentDomain.BaseDirectory + @"Queries\connection.txt";
            var program = new Program();
            var data = File.ReadAllText(filepath);
            var data2 = File.ReadAllText(connection);
            data = program.ReadFromSqlServer(data, data2);
            Console.WriteLine(data);
        }
        string ReadFromSqlServer(string query, string connStr)
        {
            using (var connection = new SqlConnection(connStr))
            {
                connection.Open();
                var sqlCommand = new SqlCommand(query, connection);
                return sqlCommand.ExecuteScalar().ToString();
            }
        }


    }
}
