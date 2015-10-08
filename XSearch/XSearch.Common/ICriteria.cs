using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSearch.Common
{
    public interface ICriteria
    {
        bool IsSearch { get; }

        int PageSize { get; }

        int PageIndex { get; }

        string SortColumn { get; }

        string SortOrder { get; }

        string GetFieldData(string fieldName);

        string FilterColumn { get; }
    }
}
