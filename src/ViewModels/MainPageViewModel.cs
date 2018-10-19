namespace FileDateCracker.ViewModels
{
    using System;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Windows.Input;
    using FileDateCracker.Helpers;
    using File = FileDateCracker.Models.File;

    public class MainPageViewModel : ViewModelBase
    {
        public ObservableCollection<File> Files { get; set; }
        public ICommand AddFileCommand { get; }
        public ICommand CloseCommand { get; }
        public ICommand DeleteFileCommand { get; }
        public ICommand SaveCommand { get; }
        public ICommand ClearFilesCommand { get; }

        public MainPageViewModel()
        {
            Files = new ObservableCollection<File>();

            AddFileCommand = new RelayCommand(OnAddFileCommand, null);
            CloseCommand = new RelayCommand(OnCloseCommand, null);
            DeleteFileCommand = new RelayCommand(OnDeleteFileCommand, null);
            SaveCommand = new RelayCommand(OnSaveCommand, null);
            ClearFilesCommand = new RelayCommand(OnClearFilesCommand, null);

            NoFiles = true;
        }

        public bool NoFiles { get; set; }

        public bool HasFiles
        {
            get
            {
                NoFiles = !Files.Any();
                RaisePropertyChanged("NoFiles");
                return Files.Any();
            }
        }

        #region Dates

        private DateTime? createdDate;
        public DateTime? CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                createdDate = value;
                RaisePropertyChanged("CreatedDate");
            }
        }

        private DateTime? createdTime;
        public DateTime? CreatedTime
        {
            get
            {
                return createdTime;
            }
            set
            {
                createdTime = value;
                RaisePropertyChanged("CreatedTime");
            }
        }


        private DateTime? lastWriteDate;
        public DateTime? LastWriteDate
        {
            get
            {
                return lastWriteDate;
            }
            set
            {
                lastWriteDate = value;
                RaisePropertyChanged("LastWriteDate");
            }
        }

        private DateTime? lastWriteTime;
        public DateTime? LastWriteTime
        {
            get
            {
                return lastWriteTime;
            }
            set
            {
                lastWriteTime = value;
                RaisePropertyChanged("LastWriteTime");
            }
        }


        private DateTime? lastAccessDate;
        public DateTime? LastAccessDate
        {
            get
            {
                return lastAccessDate;
            }
            set
            {
                lastAccessDate = value;
                RaisePropertyChanged("LastAccessDate");
            }
        }

        private DateTime? lastAccessTime;
        public DateTime? LastAccessTime
        {
            get
            {
                return lastAccessTime;
            }
            set
            {
                lastAccessTime = value;
                RaisePropertyChanged("LastAccessTime");
            }
        }

        #endregion

        private void OnAddFileCommand(object obj)
        {

            var file = DialogHelper.OpenFileDialog();
            if (file != null)
            {
                if (Files.Any(f => f.Path.ToLower() == file.ToLower()))
                {
                    EnqueueMessage("File already exists", isError: true);
                    return;
                }
                AddFile(file);
            }
        }

        public void AddFile(string path)
        {
            Files.Add(new FileInfo(path).ToModelFile());
            RaisePropertyChanged("HasFiles");
        }

        private void OnClearFilesCommand(object obj)
        {
            Files.Clear();
            RaisePropertyChanged("HasFiles");
        }

        private void OnCloseCommand(object obj)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void OnDeleteFileCommand(object obj)
        {
            var fileToRemove = Files.FirstOrDefault(f => f.Path.ToLower() == obj.ToString().ToLower());
            if (fileToRemove != null)
            {
                Files.Remove(fileToRemove);
            }

            RaisePropertyChanged("HasFiles");
        }

        private void OnSaveCommand(object obj)
        {
            try
            {
                var filesToUpdate = Files.Where(f => f.IsChecked).ToList();
                FileHelper.ChangeFilesDates(filesToUpdate, CreatedDate, CreatedTime, LastWriteDate, LastWriteTime, LastAccessDate, LastAccessTime);

                reloadFilesInfos();

                EnqueueMessage("File successfully updated", isError: false);
            }
            catch (Exception ex)
            {
                EnqueueMessage(ex.Message, isError: true);
            }
        }

        private void reloadFilesInfos()
        {
            for (int i = 0; i < Files.Count; i++)
            {
                Files[i] = new FileInfo(Files[i].Path).ToModelFile();
            }
        }
    }
}
