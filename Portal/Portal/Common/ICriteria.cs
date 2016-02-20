namespace Common
{
    public interface ICriteria
    {
        bool IsSearch { get; }

        int PageSize { get; }

        int PageIndex { get; }

        string SortColumn { get; }

        string SortOrder { get; }

        string FilterColumn { get; }

        string GetFieldData(string fieldName);
    }
}