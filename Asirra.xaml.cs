using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SQLite;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data;

namespace CAPTCHA
{
    /// <summary>
    /// Interaction logic for Asirra.xaml
    /// </summary>
    public partial class Asirra : Window
    {
        

        public Asirra()
        {
            InitializeComponent();
            DataContext = new AsirraViewModel();
            
        }

    }
}
