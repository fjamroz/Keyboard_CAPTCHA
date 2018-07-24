using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows;
using System.IO;
using System.Data.SQLite;
using System.Data;

namespace CAPTCHA
{

    class KeyboardViewModel : INotifyPropertyChanged
    {
        //połączenie z SQLite
        static string datasource = "Data Source = C:\\Users\\Filip\\documents\\visual studio 2017\\Projects\\CAPTCHA\\CAPTCHA\\Captcha.s3db";
        SQLiteConnection conn = new SQLiteConnection(datasource);
        
        
        Random rnd = new Random();
        private bool _canExecute;
        int _Pic;           // przechowuje ID  
        int _Pic2;          // zmienna pomocnicza do _Pic
        int max = 61;       // zakres bazy danych
        int _suma, _iloczyn = 1,i=0;    // zmienne do warunku zaliczenia
        int sum;            // przchowuje sume
        int multi;          // przechowuje iloczyn
        string obraz;       // przechowuje URL zdjecia
        string _input = string.Empty;   // zawiera tekst wpisany
        

      
        //metoda zwracajaca URL z SQLite
        public void SQLAction()
        {
            SQLiteCommand command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add("ID", DbType.String).Value = _Pic.ToString();
            command.CommandText = "SELECT URL FROM Keyboard WHERE ID = @ID";
            obraz = command.ExecuteScalar().ToString();

            command.Parameters.Add("ID", DbType.String).Value = _Pic;
            command.CommandText = "SELECT SUMA FROM Keyboard WHERE ID = @ID";
            sum = Int32.Parse(command.ExecuteScalar().ToString());

            command.Parameters.Add("ID", DbType.String).Value = _Pic;
            command.CommandText = "SELECT ILOCZYN FROM Keyboard WHERE ID = @ID";
            multi = Int32.Parse(command.ExecuteScalar().ToString());

        }
                      
        public KeyboardViewModel()
        {
            _canExecute = true;
            conn.Open();
            RefreshAction();
            
                        
        }

        //metoda losująca zmienna
        public int Pic
        {
            set
            {
                _Pic = rnd.Next(1, max);
            }
        }

        //wyswietlanie zdjecia
        public string DisplayImage
        {
            get
            {
                return obraz;
            }
        }
    
        //textbox
        public string Input
        {
            get { return _input; }
            set
            {
                _input = value;
                RaisePropertyChanged("Input");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName_)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName_));
            }
        }

        //obsługa przycisku "Zatwierdź"
        private ICommand _clickSubmit;
        public ICommand Submit
        {
            get
            {
                return _clickSubmit ?? (_clickSubmit = new CommandHandler(() => SubmitAction(), _canExecute));
            }
        }

        //metoda wywoływana przez przycisk "Zatwierdź"
        public void SubmitAction()
        {
            _input = _input.ToUpper();      //ujednolicenie tekstu wchodzącego
            byte[] ascii = Encoding.ASCII.GetBytes(_input); // zamiana tesktu wchodzącego na tablice ascii

            //pętla podliczająca sumę i iloczyn znaków w tabeli ascii
            foreach(char c in _input)
            {
                
                if (ascii[i] != 32) // warunek pomijania spacji
                {
                    _suma = _suma + ascii[i];
                    _iloczyn = _iloczyn * ascii[i];
                }
                i++;
            }

            //warunek zaliczenia testu. Sprawdza sumę i iloczyn z SQLite z wprowadzonym
            if (_suma == sum && _iloczyn == multi) { MessageBox.Show("Test zaliczony"); RefreshAction(); Input = string.Empty; }
            else { MessageBox.Show("Spróbuj jeszcze raz"); }

            //powrót to stanu początkowego
            Array.Clear(ascii,0,ascii.Length);
            _suma = 0;
            _iloczyn = 1;
            i = 0;
                
            
        }

        //obsługa przycisku "Nowy obraz"
        private ICommand _clickRefresh;
        public ICommand Refresh
        {
            get
            {
                return _clickRefresh ?? (_clickRefresh = new CommandHandler(() => RefreshAction(), _canExecute));
            }
        }

        //metoda wykonywana po wciśnięciu "Nowy obraz"
        public void RefreshAction()
        {
            _Pic2 = _Pic;   //zmienna pomocnicza
            while (true)    //pętla zapobiegająca wylosowaniu się obecnego obrazu
            {
                _Pic = rnd.Next(1, max);
                if (_Pic2 != _Pic) { break; }
            }
            SQLAction();
            //odświerzenie obrazu
            RaisePropertyChanged("DisplayImage");
        }

        //obsługa przycisku "Wstecz"
        private ICommand _clickBack;
        public ICommand Back
        {
            get
            {
                return _clickBack ?? (_clickBack = new CommandHandler(() => BackAction(), _canExecute));
            }
        }

        //metoda wykonywana po wciśnięciu "Wstecz"
        public void BackAction()
        {
            conn.Close();
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
    }

    public class CommandHandler : ICommand
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
    }
}
