using System;
using System.Collections.Generic;
using System.IO;
using IDEManager.Models;

namespace IDEManager.Services
{
    public class SDKDetectionService
    {
        public List<SDKInfo> DetectSDKs()
        {
            var list = new List<SDKInfo>();

            AddDotNetSDKs(list);
            AddPythonSDKs(list);
            AddJavaSDKs(list);

            return list;
        }

        private void AddDotNetSDKs(List<SDKInfo> list)
        {
            var dotnetPath = @"C:\Program Files\dotnet\sdk";

            if (!Directory.Exists(dotnetPath))
                return;

            foreach (var dir in Directory.GetDirectories(dotnetPath))
            {
                var version = Path.GetFileName(dir);

                list.Add(new SDKInfo
                {
                    Id = list.Count + 1,
                    Name = ".NET SDK",
                    Version = version,
                    InstallPath = dir,
                    Type = ".NET",
                    IsDefault = false
                });
            }
        }

        private void AddPythonSDKs(List<SDKInfo> list)
        {
            var pythonRoot = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "Python");

            if (!Directory.Exists(pythonRoot))
                return;

            foreach (var dir in Directory.GetDirectories(pythonRoot))
            {
                var version = Path.GetFileName(dir).Replace("Python", string.Empty);

                list.Add(new SDKInfo
                {
                    Id = list.Count + 1,
                    Name = "Python",
                    Version = version,
                    InstallPath = dir,
                    Type = "Python",
                    IsDefault = false
                });
            }
        }

        private void AddJavaSDKs(List<SDKInfo> list)
        {
            var javaRoot = @"C:\Program Files\Java";

            if (!Directory.Exists(javaRoot))
                return;

            foreach (var dir in Directory.GetDirectories(javaRoot))
            {
                list.Add(new SDKInfo
                {
                    Id = list.Count + 1,
                    Name = "Java",
                    Version = Path.GetFileName(dir),
                    InstallPath = dir,
                    Type = "Java",
                    IsDefault = false
                });
            }
        }
    }
}
