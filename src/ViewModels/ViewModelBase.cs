namespace FileDateCracker.ViewModels
{
    using System;
    using System.ComponentModel;
    using System.Windows.Input;
    using MaterialDesignThemes.Wpf;

    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ViewModelBase()
        {
            Messages = new SnackbarMessageQueue(TimeSpan.FromSeconds(6));
            ErrorMessages = new SnackbarMessageQueue(TimeSpan.FromSeconds(8));
        }

        private SnackbarMessageQueue messages;
        public SnackbarMessageQueue Messages
        {
            get
            {
                return messages;
            }
            set
            {
                messages = value;
                RaisePropertyChanged("Messages");
            }
        }

        private SnackbarMessageQueue errorMessages;
        public SnackbarMessageQueue ErrorMessages
        {
            get
            {
                return errorMessages;
            }
            set
            {
                errorMessages = value;
                RaisePropertyChanged("ErrorMessages");
            }
        }

        public void EnqueueMessage(string message, string action = "", bool isError = false)
        {
            if (string.IsNullOrEmpty(action))
            {
                if (isError)
                {
                    ErrorMessages.Enqueue(message);
                }
                else
                {
                    Messages.Enqueue(message);
                }
            }
            else
            {
                if (isError)
                {
                    ErrorMessages.Enqueue(message, action, null);
                }
                else
                {
                    Messages.Enqueue(message, action, null);
                }
            }
        }

    }

    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return canExecute(parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            execute?.Invoke(parameter);
        }

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
    }
}
