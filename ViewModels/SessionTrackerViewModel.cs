using System;
using System.Diagnostics;
using System.Windows.Threading;
using IDEManager.Models;
using IDEManager.Services;

namespace IDEManager.ViewModels
{
    public class SessionTrackerViewModel
    {
        private readonly TrackingService _trackingService;
        private readonly ProjectLauncherService _launcherService;
        private DispatcherTimer? _sessionTimer;
        private DateTime? _sessionStartTime;
        private Project? _activeProject;
        private IdeInstallation? _activeIde;

        public event Action<string>? SessionUpdated;
        public event Action? SessionEnded;

        public string CurrentSessionTime { get; private set; } = "00:00:00";
        public bool IsTracking { get; private set; }
        public string? ActiveProjectName { get; private set; }

        public SessionTrackerViewModel(TrackingService trackingService, ProjectLauncherService launcherService)
        {
            _trackingService = trackingService;
            _launcherService = launcherService;
        }

        public void StartSession(Project project, IdeInstallation ide)
        {
            if (IsTracking)
            {
                EndSession();
            }

            _activeProject = project;
            _activeIde = ide;
            _sessionStartTime = DateTime.Now;
            IsTracking = true;
            ActiveProjectName = project.Name;

            _trackingService.StartTracking(project.Id, ide.Id);
            _launcherService.OpenProject(project, ide);

            StartTimer();
        }

        public void EndSession()
        {
            if (!IsTracking || _activeProject == null)
                return;

            StopTimer();
            _trackingService.StopTracking();
            IsTracking = false;
            CurrentSessionTime = "00:00:00";
            ActiveProjectName = null;
            _sessionStartTime = null;

            SessionEnded?.Invoke();
        }

        private void StartTimer()
        {
            _sessionTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _sessionTimer.Tick += (s, e) => UpdateSessionTime();
            _sessionTimer.Start();
        }

        private void StopTimer()
        {
            _sessionTimer?.Stop();
            _sessionTimer = null;
        }

        private void UpdateSessionTime()
        {
            if (_sessionStartTime == null)
                return;

            var elapsed = DateTime.Now - _sessionStartTime.Value;
            CurrentSessionTime = $"{elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}";
            SessionUpdated?.Invoke(CurrentSessionTime);
        }
    }
}
