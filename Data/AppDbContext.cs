using System;
using System.Collections.Generic;
using System.IO;

namespace IDEManager.Data
{
    public class AppDbContext
    {
        private readonly string _dbPath;

        public AppDbContext(string dbPath)
        {
            _dbPath = dbPath;
            EnsureDatabaseExists();
        }

        private void EnsureDatabaseExists()
        {
            var directory = Path.GetDirectoryName(_dbPath);
            if (!string.IsNullOrWhiteSpace(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(_dbPath))
            {
                File.WriteAllText(_dbPath, string.Empty);
            }
        }

        public List<object> GetAccounts() => new();
        public List<object> GetProjects() => new();
        public List<object> GetIdeInstallations() => new();
        public List<object> GetRepositories() => new();
        public List<object> GetSdks() => new();
        public List<object> GetSecrets() => new();
        public List<object> GetNotes() => new();
        public List<object> GetWorkSessions() => new();
    }
}
