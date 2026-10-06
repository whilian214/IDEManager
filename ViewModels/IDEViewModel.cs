using System.Collections.ObjectModel;
using IDEManager.Models;
using IDEManager.Services;

namespace IDEManager.ViewModels
{
    public class IDEViewModel
    {
        private readonly IDEDiscoveryService _ideDiscoveryService;

        public ObservableCollection<IdeInstallation> IDEs { get; }

        public IDEViewModel(IDEDiscoveryService ideDiscoveryService)
        {
            _ideDiscoveryService = ideDiscoveryService;
            IDEs = new ObservableCollection<IdeInstallation>();
        }

        public void LoadIDEs()
        {
            IDEs.Clear();

            foreach (var ide in _ideDiscoveryService.DiscoverInstalledIDEs())
            {
                IDEs.Add(ide);
            }
        }
    }
}
