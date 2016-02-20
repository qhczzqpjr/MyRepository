using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Business.Models;
using Business.Services;

namespace Portal
{
    public class DataLoader : IDisposable
    {

        private readonly DbManagementService _dbManagementService = new DbManagementService();
        private string _fileformat;
        private static DataLoader _dataLoader;

        private List<Requirement> _requirements;
        private string _workdirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);

        private DataLoader()
        {

        }

        public static DataLoader Instance
        {
            get { return _dataLoader ?? (_dataLoader = new DataLoader()); }
        }

        public void Init()
        {
            _workdirectory = ConfigurationManager.AppSettings["WorkingDirectory"] ?? string.Empty;
            _fileformat = ConfigurationManager.AppSettings["Fileformat"] ?? string.Empty;
            Init(_workdirectory, _fileformat);

        }
        public void Init(string workdirectory, string regex)
        {

            _workdirectory = workdirectory;

            _requirements = Directory.EnumerateFiles(_workdirectory, regex).ToList().Select(file =>
                new Requirement { Code = Path.GetFileNameWithoutExtension(file), Context = File.ReadAllText(file) })
                .ToList();
            if (_requirements.Count == 0)
                return;

            _requirements.ForEach(p =>
            {
                if (_dbManagementService.GetRequirement(p.Code) == null)
                    _dbManagementService.CreateRequirement(p);
            });
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}