using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace IDEManager.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string AuthType { get; set; } = "Local";
        public string AccessToken { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }

    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string Type { get; set; } = "Unknown";
        public bool IsFavorite { get; set; }
        public DateTime LastOpenedAt { get; set; } = DateTime.Now;
        public long TotalSecondsSpent { get; set; }
        public int? RelatedRepositoryId { get; set; }
        public int? RelatedIdeId { get; set; }
    }

    public class IdeInstallation
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ExecutablePath { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string InstallPath { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public string IconPath { get; set; } = string.Empty;
    }

    public class RepositoryInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string RemoteUrl { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;
        public string Provider { get; set; } = "GitHub";
        public string Branch { get; set; } = "main";
        public DateTime LastSyncAt { get; set; } = DateTime.Now;
        public string LocalPath { get; set; } = string.Empty;
    }

    public class SDKInfo
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Version { get; set; } = string.Empty;
        public string InstallPath { get; set; } = string.Empty;
        public string Type { get; set; } = "Unknown";
        public bool IsDefault { get; set; }
    }

    public class SecretEntry
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Category { get; set; } = "General";
        public string Notes { get; set; } = string.Empty;
    }

    public class NoteEntry
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }

    public class WorkSession
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int IDEId { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.Now;
        public DateTime? EndedAt { get; set; }
        public long TotalSeconds { get; set; }
    }
}
