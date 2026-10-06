using System.Windows;
using Microsoft.Win32;
using IDEManager.ViewModels;

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
                var selectedPath = System.IO.Path.GetDirectoryName(dialog.FileName);
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
            MessageBox.Show("Открытие проекта в IDE будет добавлено в следующем этапе.", "IDE Manager");
        }
    }
}
