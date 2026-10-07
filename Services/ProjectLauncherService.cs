using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using IDEManager.Models;

namespace IDEManager.Services
{
    public class ProjectLauncherService
    {
        public void OpenProject(Project project, IdeInstallation? ide = null)
        {
            if (project == null)
                throw new ArgumentNullException(nameof(project));

            if (string.IsNullOrWhiteSpace(project.Path) || !Directory.Exists(project.Path))
                throw new DirectoryNotFoundException($"Папка проекта не найдена: {project.Path}");

            var executablePath = ide?.ExecutablePath ?? DetectPreferredExecutable(project);

            if (string.IsNullOrWhiteSpace(executablePath))
            {
                throw new InvalidOperationException("Не удалось найти IDE. Установите VS Code, Visual Studio или Rider.");
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = executablePath,
                Arguments = $"\"{project.Path}\"",
                WorkingDirectory = project.Path,
                UseShellExecute = true
            };

            Process.Start(startInfo);
        }

        public string DetectPreferredExecutable(Project project)
        {
            var candidates = new[]
            {
                @"C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\IDE\devenv.exe",
                @"C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\IDE\devenv.exe",
                @"C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\IDE\devenv.exe",
                @"C:\Program Files\Microsoft VS Code\Code.exe",
                @"C:\Program Files\JetBrains\Rider\bin\rider64.exe",
                @"C:\Program Files\JetBrains\IntelliJ IDEA\bin\idea64.exe"
            };

            foreach (var candidate in candidates)
            {
                if (File.Exists(candidate))
                {
                    return candidate;
                }
            }

            return string.Empty;
        }
    }
}
