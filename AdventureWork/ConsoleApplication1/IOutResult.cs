using System.Collections.Generic;

namespace ConsoleApplication1
{
    interface IOutResult
    {
        List<object> OutMultiResult(List<object> list,object template);
        object OutOneResult(List<object> list, object template);
        object OutOneResult(object list, object template);
        
    }
}
