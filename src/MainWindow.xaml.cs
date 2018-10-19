namespace FileDateCracker
{
    using System.Windows;
    using FileDateCracker.ViewModels;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainPageViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            viewModel = new MainPageViewModel();
            this.DataContext = viewModel;
        }

        private void ListViewFiles_OnDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (var file in files)
                {
                    viewModel.AddFile(file);
                }
            }
        }

        private void HeaderMouseClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
