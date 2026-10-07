using System.Windows;
using System.IO;
using IDEManager.ViewModels;
using Microsoft.Win32;

namespace IDEManager
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
        }

        private void AddProjectButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = false,
                ValidateNames = false,
                FileName = "Select folder",
                Title = "Выберите папку проекта"
            };

            if (dialog.ShowDialog() == true)
            {
                var selectedPath = Path.GetDirectoryName(dialog.FileName);
                if (!string.IsNullOrWhiteSpace(selectedPath))
                {
                    _viewModel.AddProject(selectedPath);
                }
            }
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.LoadAll();
        }

        private void OpenInIdeButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedProject == null)
            {
                MessageBox.Show("Сначала выберите проект из списка.", "IDE Manager");
                return;
            }

            _viewModel.OpenSelectedProject();
        }

        private void StopSessionButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.EndActiveSession();
        }

        private void ThemeButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SwitchTheme();
        }
    }
}
