using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;


namespace Black_jack_e
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Random rnd = new Random();
        List<int> numberSpeler = new List<int>();
        List<int> numberBank = new List<int>();
        string state = "start";
        public MainWindow()
        {
            InitializeComponent();
            veranderstaat("start");

        }
        private void veranderstaat(string niewState)
        {
            state = niewState;
            switch (niewState)
            {
                case "start":
                    BtnDeel.IsEnabled = true;
                    BtnHit.IsEnabled = false;
                    BtnStand.IsEnabled = false;
                    BtnNieuwSpel.Visibility = Visibility.Hidden;
                    BtnDeel.Visibility = Visibility.Visible;
                    BtnHit.Visibility = Visibility.Visible;
                    BtnStand.Visibility = Visibility.Visible;
                    LstSpeler.Items.Clear();
                    LstBank.Items.Clear();
                    numberSpeler.Clear();
                    numberBank.Clear();
                    LbLBankNummer.Content = "0";
                    LblSpelerNummer.Content = "0";
                    
                    break;
                case "speelfase":
                    BtnDeel.IsEnabled = false;
                    BtnHit.IsEnabled = true;
                    BtnStand.IsEnabled = true;
                    break;
                case "gewonnen":
                    BtnDeel.IsEnabled = false;
                    BtnHit.IsEnabled = false;
                    BtnStand.IsEnabled = false;
                    BtnDeel.Visibility = Visibility.Hidden;
                    BtnHit.Visibility = Visibility.Hidden;
                    BtnStand.Visibility = Visibility.Hidden;
                    BtnNieuwSpel.Visibility = Visibility.Visible;

                    break;

            }
        }

        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            
            veranderstaat("speelfase");
            //kaartenBank(true);
            GeefKaart(true);
            GeefKaart(true);
            GeefKaart(false);
        }
        //apend line 
        //apend

        private void GeefKaart(bool isSpeler)
        {
            int kaart = rnd.Next(1, 11);
            int soort = rnd.Next(1, 5);
            string soortNaam = null;
            switch (soort)
            {
                case 1:
                    soortNaam = "klaveren";
                    break;
                case 2:
                    soortNaam = "schuppen";
                    break;
                case 3:
                    soortNaam = "harten";
                    break;
                case 4:
                    soortNaam = "ruiten";
                    break;
            }
            if (isSpeler)
            {
                numberSpeler.Add(kaart);
                LstSpeler.Items.Add(soortNaam + " " + kaart);
                int som = numberSpeler.Sum();
                LblSpelerNummer.Content = som;
                if (numberSpeler.Sum() > 21)
                {
                    MessageBox.Show("verloren");
                    veranderstaat("gewonnen");
                    
                }
                
            }
            else
            {
                numberBank.Add(kaart);
                LstBank.Items.Add(soortNaam + " " + kaart);
                int som = numberBank.Sum();
                LbLBankNummer.Content = som;
            }
        }

        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
            GeefKaart(true);
        }

        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            bool bankStop = false;
            do
            {

                GeefKaart(false);
                if (numberBank.Sum() > 16)
                {
                    if (numberBank.Sum() > numberSpeler.Sum()&&numberBank.Sum() < 22)
                    {
                        bankStop = true;
                        MessageBox.Show("Verloren");
                    }
                    else if (numberBank.Sum() == numberSpeler.Sum())
                    {                       
                        bankStop = true;
                        MessageBox.Show("Push");
                    }
                    else if (numberBank.Sum() > 21)
                    {
                        bankStop = true;
                        MessageBox.Show("gewonnen");
                    }
                }

            } while (bankStop == false);
            veranderstaat("gewonnen");

        }

        private void BtnNieuwSpel_Click(object sender, RoutedEventArgs e)
        {
            veranderstaat("start");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void BedragSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }
    }

}
