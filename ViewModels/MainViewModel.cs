using System.Collections.ObjectModel;
using IDEManager.Models;
using IDEManager.Services;

namespace IDEManager.ViewModels
{
    public class MainViewModel
    {
        private readonly ProjectService _projectService;
        private readonly IDEDiscoveryService _ideDiscoveryService;
        private readonly SDKDetectionService _sdkDetectionService;

        public ObservableCollection<Project> Projects { get; }
        public ObservableCollection<IdeInstallation> InstalledIdeList { get; }
        public ObservableCollection<SDKInfo> InstalledSdkList { get; }

        public MainViewModel()
        {
            _projectService = new ProjectService();
            _ideDiscoveryService = new IDEDiscoveryService();
            _sdkDetectionService = new SDKDetectionService();

            Projects = new ObservableCollection<Project>();
            InstalledIdeList = new ObservableCollection<IdeInstallation>();
            InstalledSdkList = new ObservableCollection<SDKInfo>();

            LoadAll();
        }

        public void LoadAll()
        {
            LoadProjects();
            LoadIDEs();
            LoadSDKs();
        }

        public void LoadProjects()
        {
            Projects.Clear();

            foreach (var project in _projectService.GetAllProjects())
            {
                Projects.Add(project);
            }
        }

        public void LoadIDEs()
        {
            InstalledIdeList.Clear();

            foreach (var ide in _ideDiscoveryService.DiscoverInstalledIDEs())
            {
                InstalledIdeList.Add(ide);
            }
        }

        public void LoadSDKs()
        {
            InstalledSdkList.Clear();

            foreach (var sdk in _sdkDetectionService.DetectSDKs())
            {
                InstalledSdkList.Add(sdk);
            }
        }

        public void AddProject(string path)
        {
            var project = _projectService.AddProject(path);
            Projects.Add(project);
        }
    }
}
