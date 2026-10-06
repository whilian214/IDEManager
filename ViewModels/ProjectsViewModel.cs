using System.Collections.ObjectModel;
using IDEManager.Models;
using IDEManager.Services;

namespace IDEManager.ViewModels
{
    public class ProjectsViewModel
    {
        private readonly ProjectService _projectService;

        public ObservableCollection<Project> Projects { get; }

        public ProjectsViewModel(ProjectService projectService)
        {
            _projectService = projectService;
            Projects = new ObservableCollection<Project>();
        }

        public void LoadProjects()
        {
            Projects.Clear();

            foreach (var project in _projectService.GetAllProjects())
            {
                Projects.Add(project);
            }
        }

        public void AddProject(string path)
        {
            var project = _projectService.AddProject(path);
            Projects.Add(project);
        }
    }
}
