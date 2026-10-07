using System;
using System.Collections.Generic;
using System.IO;
using IDEManager.Models;

namespace IDEManager.Services
{
    public class IDEDiscoveryService
    {
        public List<IdeInstallation> DiscoverInstalledIDEs()
        {
            var result = new List<IdeInstallation>();

            var candidatePaths = new[]
            {
                @"C:\Program Files\Microsoft Visual Studio",
                @"C:\Program Files\JetBrains",
                @"C:\Program Files\Microsoft VS Code",
                @"C:\Program Files\Rider",
                @"C:\Program Files\IntelliJ IDEA"
            };

            foreach (var basePath in candidatePaths)
            {
                if (!Directory.Exists(basePath))
                    continue;

                foreach (var dir in Directory.GetDirectories(basePath, "*", SearchOption.AllDirectories))
                {
                    var exeFiles = Directory.GetFiles(dir, "*.exe", SearchOption.TopDirectoryOnly);

                    foreach (var exe in exeFiles)
                    {
                        var fileName = Path.GetFileName(exe).ToLower();

                        if (fileName.Contains("devenv") ||
                            fileName.Contains("code") ||
                            fileName.Contains("idea") ||
                            fileName.Contains("rider"))
                        {
                            result.Add(new IdeInstallation
                            {
                                Id = result.Count + 1,
                                Name = DetectName(fileName),
                                ExecutablePath = exe,
                                InstallPath = dir,
                                Version = "Unknown",
                                IsDefault = false,
                                IconPath = string.Empty
                            });
                        }
                    }
                }
            }

            return result;
        }

        private string DetectName(string fileName)
        {
            if (fileName.Contains("devenv"))
                return "Visual Studio";

            if (fileName.Contains("code"))
                return "Visual Studio Code";

            if (fileName.Contains("idea"))
                return "IntelliJ IDEA";

            if (fileName.Contains("rider"))
                return "Rider";

            return "Unknown IDE";
        }
    }
}
