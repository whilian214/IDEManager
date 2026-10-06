using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using IDEManager.Models;

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
            Directory.CreateDirectory(Path.GetDirectoryName(_dbPath) ?? Environment.CurrentDirectory);

            if (!File.Exists(_dbPath))
            {
                File.WriteAllText(_dbPath, string.Empty);
            }
        }

        public List<Account> GetAccounts()
        {
            return new List<Account>();
        }

        public List<Project> GetProjects()
        {
            return new List<Project>();
        }

        public List<IdeInstallation> GetIdeInstallations()
        {
            return new List<IdeInstallation>();
        }

        public List<RepositoryInfo> GetRepositories()
        {
            return new List<RepositoryInfo>();
        }

        public List<SDKInfo> GetSdks()
        {
            return new List<SDKInfo>();
        }

        public List<SecretEntry> GetSecrets()
        {
            return new List<SecretEntry>();
        }

        public List<NoteEntry> GetNotes()
        {
            return new List<NoteEntry>();
        }

        public List<WorkSession> GetWorkSessions()
        {
            return new List<WorkSession>();
        }
    }
}
