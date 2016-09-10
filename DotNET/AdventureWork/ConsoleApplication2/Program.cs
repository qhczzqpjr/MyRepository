using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication2
{
    class Program
    {
        static void Main(string[] args)
        {
        }

        private static void TestCase3()
        {
            DerivedTemplate1 t1 = new DerivedTemplate1();
            string result = t1.TransformText();
            Console.WriteLine(result);
        }

        private static void TestCase2()
        {
            MyTextTemplate1 t1 = new MyTextTemplate1();
            string result = t1.TransformText();
            Console.WriteLine(result);
        }

        private static void TestCase1()
        {
            MyData data = new MyData(new MyDataItem() {Name = "Test", Value = "Test"});
            MyWebPage page = new MyWebPage(data);
            String pageContent = page.TransformText();
            System.IO.File.WriteAllText("outputPage.html", pageContent);
        }
    }
}
