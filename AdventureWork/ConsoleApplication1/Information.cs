using LinqToExcel.Attributes;

namespace ConsoleApplication1
{
    internal class Information
    {
        [ExcelColumn("InfoId")]
        public int Id { get; set; }
        public string Info { get; set; }
        public string Catagory { get; set; }
        public string SearchCode { get; set; }
        public string SearchDescription { get; set; }
        public int Status { get; set; }

    }
}