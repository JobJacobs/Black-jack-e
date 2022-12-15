using System;
using System.Collections.Generic;
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

namespace Black_jack_e
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //random is type rnd is naam
        Random rnd = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {

        }
        //apend line 
        //apend
        private void random()
        {
            cardValue = rnd.next(1, 14);
           
        }
        private
    }
    
}
