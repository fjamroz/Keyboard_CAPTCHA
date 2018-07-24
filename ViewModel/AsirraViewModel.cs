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
    class AsirraViewModel : INotifyPropertyChanged
    {
        static string datasource = "Data Source = C:\\Users\\Filip\\documents\\visual studio 2017\\Projects\\CAPTCHA\\CAPTCHA\\Captcha.s3db";
        SQLiteConnection conn = new SQLiteConnection(datasource);

        private bool _canExecute;
        Random rnd = new Random();
        string obraz1, obraz2, obraz3, obraz4;
        int state1, state2, state3, state4;
        int max = 60;
        int _Pic1, _Pic2, _Pic3, _Pic4;

        private bool isSelected1;
        private bool isSelected2;
        private bool isSelected3;
        private bool isSelected4;


        //metoda zwracajaca URL z SQLite
        public void SQLAction()
        {
            SQLiteCommand command = conn.CreateCommand();
            command.CommandType = System.Data.CommandType.Text;
            command.Parameters.Add("ID", DbType.String).Value = _Pic1;
            command.CommandText = "SELECT URL FROM Pets WHERE ID = @ID";
            obraz1 = command.ExecuteScalar().ToString();

            command.Parameters.Add("ID", DbType.String).Value = _Pic2;
            command.CommandText = "SELECT URL FROM Pets WHERE ID = @ID";
            obraz2 = command.ExecuteScalar().ToString();

            command.Parameters.Add("ID", DbType.String).Value = _Pic3;
            command.CommandText = "SELECT URL FROM Pets WHERE ID = @ID";
            obraz3 = command.ExecuteScalar().ToString();

            command.Parameters.Add("ID", DbType.String).Value = _Pic4;
            command.CommandText = "SELECT URL FROM Pets WHERE ID = @ID";
            obraz4 = command.ExecuteScalar().ToString();

            command.Parameters.Add("ID", DbType.Int32).Value = _Pic1;
            command.CommandText = "SELECT BOOL FROM Pets WHERE ID = @ID";
            state1 = Int32.Parse(command.ExecuteScalar().ToString());

            command.Parameters.Add("ID", DbType.Int32).Value = _Pic2;
            command.CommandText = "SELECT BOOL FROM Pets WHERE ID = @ID";
            state2 = Int32.Parse(command.ExecuteScalar().ToString());
            
            command.Parameters.Add("ID", DbType.Int32).Value = _Pic3;
            command.CommandText = "SELECT BOOL FROM Pets WHERE ID = @ID";
            state3 = Int32.Parse(command.ExecuteScalar().ToString());

            command.Parameters.Add("ID", DbType.Int32).Value = _Pic4;
            command.CommandText = "SELECT BOOL FROM Pets WHERE ID = @ID";
            state4 = Int32.Parse(command.ExecuteScalar().ToString());         
        }

      
        public AsirraViewModel()
        {
            conn.Open();
            Mix();
            SQLAction();
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

        private ICommand _clickSubmit;
        public ICommand Submit
        {
            get
            {
                return _clickSubmit ?? (_clickSubmit = new CommandHandler(() => SubmitAction(), _canExecute));
            }
        }

        public void SubmitAction()
        { 
            
            if(state1 == Convert.ToInt32(IsSelected1) && state2 == Convert.ToInt32(IsSelected2) && state3 == Convert.ToInt32(IsSelected3) && state4 == Convert.ToInt32(IsSelected4))
            {
                MessageBox.Show("Test zaliczony");
                Mix();
                SQLAction();
                RaisePropertyChanged("DisplayImage1");
                RaisePropertyChanged("DisplayImage2");
                RaisePropertyChanged("DisplayImage3");
                RaisePropertyChanged("DisplayImage4");
                IsSelected1 = false;
                IsSelected2 = false;
                IsSelected3 = false;
                IsSelected4 = false;
            }
            else { MessageBox.Show("Spróbuj ponownie"); }
                   
        }

        

        public void Mix()
        {
            _Pic1 = rnd.Next(1, max);
            _Pic2 = rnd.Next(1, max);
            _Pic3 = rnd.Next(1, max);
            _Pic4 = rnd.Next(1, max);
            while (true)
            {
                if (_Pic2 == _Pic1) { _Pic2 = rnd.Next(1, max); continue; }
                if (_Pic3 == _Pic1 || _Pic3 == _Pic2) { _Pic3 = rnd.Next(1, max); continue; }
                if (_Pic4 == _Pic1 || _Pic4 == _Pic2 || _Pic4 == _Pic3)
                {
                    _Pic4 = rnd.Next(1, max);
                    continue;
                }
                else
                {
                    if (_Pic1 < 31 || _Pic2 < 31 || _Pic3 < 31 || _Pic4 < 31) { break; }
                    else { _Pic1 = rnd.Next(1, max); }
                }
                
            }
        }

            

        private ICommand _clickBack;
        public ICommand Back
        {
            get
            {
                return _clickBack ?? (_clickBack = new CommandHandler(() => BackAction(), _canExecute));
            }
        }

        public void BackAction()
        {
            conn.Close();
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();

        }

        public string DisplayImage1
        {
            get
            {
                return obraz1;
            }
        }

        public string DisplayImage2
        {
            get
            {
                return obraz2;
            }
        }

        public string DisplayImage3
        {
            get
            {
                return obraz3;
            }
        }

        public string DisplayImage4
        {
            get
            {
                return obraz4;
            }
        }

        public bool IsSelected1
        {
            get { return isSelected1; }
            set
            {
                if (isSelected1 != value)
                {
                    isSelected1 = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IsSelected1"));
                    }
                }
            }
        }

        public bool IsSelected2
        {
            get { return isSelected2; }
            set
            {
                if (isSelected2 != value)
                {
                    isSelected2 = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IsSelected2"));
                    }
                }
            }
        }

        public bool IsSelected3
        {
            get { return isSelected3; }
            set
            {
                if (isSelected3 != value)
                {
                    isSelected3 = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IsSelected3"));
                    }
                }
            }
        }

        public bool IsSelected4
        {
            get { return isSelected4; }
            set
            {
                if (isSelected4 != value)
                {
                    isSelected4 = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("IsSelected4"));
                    }
                }
            }
        }
    }

}
