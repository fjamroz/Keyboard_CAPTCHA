using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CAPTCHA
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        

        public MainWindow()
        {

            
            //string datasource = "Data Source = C:\\Users\\Filip\\documents\\visual studio 2017\\Projects\\CAPTCHA\\CAPTCHA\\Captcha.s3db";
            //SQLiteConnection conn = new SQLiteConnection(datasource);
            //string text ="1";
           
            //conn.Open();
            //var command = conn.CreateCommand();
            

            
            
            
            //command.CommandType = CommandType.Text;
            //command.Parameters.Add("ID", DbType.String).Value = text;
            //command.CommandText = "SELECT URL FROM Pets WHERE ID = @ID";
            //object target =  command.ExecuteScalar();

           // Console.WriteLine(target);


            InitializeComponent();
           
            DataContext = new MainWindowViewModel();
            
        }

        
    }
}
