using System.Collections.Generic;

namespace ConsoleApplication2
{
    public class MyData
    {
        public List<MyDataItem> Items { get; set; }

        public MyData(MyDataItem myDataItem)
        {
            Items = new List<MyDataItem> {myDataItem};
        }
      
    }
    public class MyDataItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}