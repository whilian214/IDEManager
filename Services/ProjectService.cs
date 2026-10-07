using System;
using System.Collections.Generic;
using System.IO;
using IDEManager.Models;

namespace IDEManager.Services
{
    public class ProjectService
    {
        private readonly List<Project> _projects = new();

        public Project AddProject(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentException("Путь проекта не может быть пустым.");

            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException($"Папка проекта не найдена: {path}");

            var project = new Project
            {
                Id = _projects.Count + 1,
                Name = Path.GetFileName(path),
                Path = path,
                Type = DetectProjectType(path),
                LastOpenedAt = DateTime.Now,
                TotalSecondsSpent = 0
            };

            _projects.Add(project);
            return project;
        }

        public List<Project> GetAllProjects()
        {
            return _projects;
        }

        public string DetectProjectType(string path)
        {
            if ((File.Exists(Path.Combine(path, "Program.cs")) ||
                Directory.Exists(Path.Combine(path, ".git")) && Directory.Exists(Path.Combine(path, "src"))))
            {
                return "C#";
            }

            if (File.Exists(Path.Combine(path, "package.json")))
            {
                return "Node";
            }

            if (File.Exists(Path.Combine(path, "requirements.txt")) ||
                File.Exists(Path.Combine(path, "pyproject.toml")))
            {
                return "Python";
            }

            if (File.Exists(Path.Combine(path, "pom.xml")) ||
                File.Exists(Path.Combine(path, "build.gradle")))
            {
                return "Java";
            }

            if (File.Exists(Path.Combine(path, "index.html")) ||
                File.Exists(Path.Combine(path, "vite.config.js")))
            {
                return "Web";
            }

            return "Unknown";
        }
    }
}
