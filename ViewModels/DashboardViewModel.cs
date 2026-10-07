using System.Collections.ObjectModel;
using IDEManager.Models;
using IDEManager.Services;

namespace IDEManager.ViewModels
{
    public class DashboardViewModel
    {
        private readonly TrackingService _trackingService;

        public ObservableCollection<ProjectTimeStat> ProjectStats { get; }

        public DashboardViewModel(TrackingService trackingService)
        {
            _trackingService = trackingService;
            ProjectStats = new ObservableCollection<ProjectTimeStat>();
        }

        public void RefreshStats(List<Project> projects)
        {
            ProjectStats.Clear();

            foreach (var project in projects)
            {
                var totalSeconds = _trackingService.GetTotalSecondsForProject(project.Id);
                ProjectStats.Add(new ProjectTimeStat
                {
                    ProjectName = project.Name,
                    TotalSeconds = totalSeconds
                });
            }
        }
    }

    public class ProjectTimeStat
    {
        public string ProjectName { get; set; } = string.Empty;
        public long TotalSeconds { get; set; }
    }
}
