using System;
using System.Collections.ObjectModel;
using System.Linq;
using IDEManager.Models;
using IDEManager.Services;

namespace IDEManager.ViewModels
{
    public class MainViewModel
    {
        private readonly ProjectService _projectService;
        private readonly IDEDiscoveryService _ideDiscoveryService;
        private readonly SDKDetectionService _sdkDetectionService;
        private readonly TrackingService _trackingService;
        private readonly ProjectLauncherService _launcherService;
        private readonly SessionTrackerViewModel _sessionTracker;

        public ObservableCollection<Project> Projects { get; }
        public ObservableCollection<IdeInstallation> InstalledIdeList { get; }
        public ObservableCollection<SDKInfo> InstalledSdkList { get; }
        public ObservableCollection<WorkSession> RecentSessions { get; }

        public Project? SelectedProject { get; set; }
        public IdeInstallation? SelectedIde { get; set; }
        public string CurrentTheme { get; set; } = "Dark";

        public int TotalProjects => Projects.Count;
        public string TotalTrackedHours => GetTotalTrackedHours();
        public string ActiveIdeName => SelectedIde?.Name ?? (InstalledIdeList.Count > 0 ? InstalledIdeList[0].Name : "Not detected");
        public string CurrentSessionTime => _sessionTracker.CurrentSessionTime;
        public bool IsSessionActive => _sessionTracker.IsTracking;
        public string? ActiveProjectName => _sessionTracker.ActiveProjectName;

        public MainViewModel()
        {
            _projectService = new ProjectService();
            _ideDiscoveryService = new IDEDiscoveryService();
            _sdkDetectionService = new SDKDetectionService();
            _trackingService = new TrackingService();
            _launcherService = new ProjectLauncherService();
            _sessionTracker = new SessionTrackerViewModel(_trackingService, _launcherService);

            Projects = new ObservableCollection<Project>();
            InstalledIdeList = new ObservableCollection<IdeInstallation>();
            InstalledSdkList = new ObservableCollection<SDKInfo>();
            RecentSessions = new ObservableCollection<WorkSession>();

            _sessionTracker.SessionUpdated += (time) => { };
            _sessionTracker.SessionEnded += () => RefreshSessions();

            LoadAll();
        }

        public void LoadAll()
        {
            LoadProjects();
            LoadIDEs();
            LoadSDKs();
            RefreshSessions();
        }

        public void LoadProjects()
        {
            Projects.Clear();

            foreach (var project in _projectService.GetAllProjects())
            {
                Projects.Add(project);
            }

            if (Projects.Count > 0 && SelectedProject == null)
            {
                SelectedProject = Projects[0];
            }
        }

        public void LoadIDEs()
        {
            InstalledIdeList.Clear();

            foreach (var ide in _ideDiscoveryService.DiscoverInstalledIDEs())
            {
                InstalledIdeList.Add(ide);
            }

            if (InstalledIdeList.Count > 0 && SelectedIde == null)
            {
                SelectedIde = InstalledIdeList[0];
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

        public void RefreshSessions()
        {
            RecentSessions.Clear();

            var sessions = _trackingService.GetSessions()
                .OrderByDescending(s => s.StartedAt)
                .Take(10);

            foreach (var session in sessions)
            {
                RecentSessions.Add(session);
            }
        }

        public void AddProject(string path)
        {
            var project = _projectService.AddProject(path);
            Projects.Add(project);
            SelectedProject = project;
        }

        public void OpenSelectedProject()
        {
            if (SelectedProject == null)
            {
                return;
            }

            var ide = SelectedIde ?? InstalledIdeList.FirstOrDefault();
            if (ide == null)
            {
                return;
            }

            _sessionTracker.StartSession(SelectedProject, ide);
        }

        public void EndActiveSession()
        {
            _sessionTracker.EndSession();
            RefreshSessions();
        }

        public void SwitchTheme()
        {
            CurrentTheme = CurrentTheme == "Dark" ? "Light" : "Dark";
        }

        public string GetTotalTrackedHours()
        {
            long totalSeconds = 0;

            foreach (var session in _trackingService.GetSessions())
            {
                totalSeconds += session.TotalSeconds;
            }

            var hours = totalSeconds / 3600;
            var minutes = (totalSeconds % 3600) / 60;
            return $"{hours}h {minutes}m";
        }
    }
}
