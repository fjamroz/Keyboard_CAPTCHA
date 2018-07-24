using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.IO;

namespace CAPTCHA
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        private bool _canExecute;
        string _input = string.Empty;
        

        public MainWindowViewModel()
        {
            _canExecute = true;
        }

        
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName_)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName_));
            }
        }

        private ICommand _clickKeyboardBtn;
        public ICommand KeyboardBtn
        {
            get
            {
                return _clickKeyboardBtn ?? (_clickKeyboardBtn = new CommandHandler(() => KeyboardBtnAction(), _canExecute));
            }
        }
        public void KeyboardBtnAction()
        {
            Keyboard keyboard = new Keyboard();           
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = keyboard;
            keyboard.Show();

        }

        private ICommand _clickAsirraBtn;
        public ICommand AsirraBtn
        {
            get
            {
                return _clickAsirraBtn ?? (_clickAsirraBtn = new CommandHandler(() => AsirraBtnAction(), _canExecute));
            }
        }
        public void AsirraBtnAction()
        {
            Asirra asirra = new Asirra();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = asirra;
            asirra.Show();
        }

        private ICommand _clickEscapeBtn;
        public ICommand EscapeBtn
        {
            get
            {
                return _clickEscapeBtn ?? (_clickEscapeBtn = new CommandHandler(() => EscapeBtnAction(), _canExecute));
            }
        }
        public void EscapeBtnAction()
        {
            Environment.Exit(1);
        }
       

        /*private ICommand _clickBack;
        public ICommand Back
        {
            get
            {
                return _clickBack ?? (_clickBack = new CommandHandler(() => BackAction(), _canExecute));
            }
        }*/
      
    }

    /*public class CommandHandler : ICommand
    {
        private Action _action;
        private bool _canExecute;
        public CommandHandler(Action action, bool canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _action();
        }
    }*/
}
