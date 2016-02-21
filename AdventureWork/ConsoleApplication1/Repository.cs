using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApplication1
{
    public class FileManagementRepository
    {
        private string _workdirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        private const string Extension = ".tz";
        public string WorkDirectory { get { return _workdirectory; } }

        private List<Requirement> _requirements;
        private static FileManagementRepository _fileManagementRepository;
        private FileManagementRepository(){}

        public static FileManagementRepository Instance
        {
           get { return _fileManagementRepository ?? (_fileManagementRepository = new FileManagementRepository()); }
        }

        public void Init()
        {
            if (!Directory.Exists(_workdirectory))
            {
                Directory.CreateDirectory(_workdirectory);
            }
            _requirements = Directory.EnumerateFiles(_workdirectory, "*"+Extension).ToList().Select(file =>
                new Requirement() { Code = Path.GetFileNameWithoutExtension(file), Context = File.ReadAllText(file) }).ToList();

        }

        public void Init(string workdirectory)
        {
            _workdirectory = workdirectory;
            Init();
        }

        public void Insert(Requirement item, bool saveImmediately = true)
        {
            if (_requirements.Exists(p=>p.Code==item.Code))
                throw new Exception("Can't insert the data as the object has already existed.");
                  
            _requirements.Add(item);

            if (File.Exists(GetCombinePath(item)))
            {
                File.Delete(GetCombinePath(item));
            }
            using (var streamWriter = new StreamWriter(GetCombinePath(item)))
            {
                streamWriter.Write(item.Context);
            }     
          

        }

        public void Update(Requirement item, bool saveImmediately = true)
        {
            _requirements.Find(requirement => requirement.Code == item.Code).Context = item.Context;

            if (File.Exists(GetCombinePath(item)))
            {
                File.Delete(GetCombinePath(item));
            }
            using (var streamWriter = new StreamWriter(GetCombinePath(item)))
            {
                streamWriter.Write(item.Context);
            }
        }

        public void Delete(Requirement item, bool saveImmediately = true)
        {
            _requirements.Remove(item);
            File.Delete(GetCombinePath(item));
        }

        public List<Requirement> GetRequirements()
        {
            return _requirements;
        }

        public Requirement GetRequirement(string code)
        {
            return _requirements.FirstOrDefault(p => p.Code == code);
        }
        private string GetCombinePath(Requirement item)
        {
            return Path.Combine(WorkDirectory, item.Code+Extension);
        }

    }
}
