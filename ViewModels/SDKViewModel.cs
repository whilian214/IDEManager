using System.Collections.ObjectModel;
using IDEManager.Models;
using IDEManager.Services;

namespace IDEManager.ViewModels
{
    public class SDKViewModel
    {
        private readonly SDKDetectionService _sdkDetectionService;

        public ObservableCollection<SDKInfo> SDKs { get; }

        public SDKViewModel(SDKDetectionService sdkDetectionService)
        {
            _sdkDetectionService = sdkDetectionService;
            SDKs = new ObservableCollection<SDKInfo>();
        }

        public void LoadSDKs()
        {
            SDKs.Clear();

            foreach (var sdk in _sdkDetectionService.DetectSDKs())
            {
                SDKs.Add(sdk);
            }
        }
    }
}
