namespace ConsoleApplication1
{
    public interface IInputManager
    {
        void ReadFromFile(string filePath);
        //void ReadFromRepostory(Requirement requirement);
        //void ReadFromFile(FileInfo file);
        //void ReadFromDb(string query,string connectionString);
        //void ReadFromDb(string query,DbConnection dbConnection);
    }
}
