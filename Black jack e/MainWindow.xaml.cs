using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
            switch(niewState)
            {
                case "start":
                    BtnDeel.IsEnabled= true;
                    BtnHit.IsEnabled= false;
                    BtnStand.IsEnabled= false;
                    break;
                case "speelfase":
                    BtnDeel.IsEnabled = false;
                    BtnHit.IsEnabled = true;
                    BtnStand.IsEnabled = true;
                    break;
                case "gewonnen":
                    BtnDeel.IsEnabled = true;
                    BtnHit.IsEnabled = false;
                    BtnStand.IsEnabled = false;
                    break;

            }
        }

        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            veranderstaat("speelfase");
            kaartenBank(true);
            GeefKaart(true);
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

        private void kaartenBank(bool isBank)
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
            if (isBank)
            {
                numberBank.Add(kaart);
                LstBank.Items.Add(soortNaam + " " + kaart);
                int som = numberBank.Sum();
                LbLBankNummer.Content = som;

            }
            else
            {
                numberBank.Add(kaart);
                LstSpeler.Items.Add(soortNaam + " " + kaart);
                int som = numberBank.Sum();
                LblSpelerNummer.Content = som;
            }
        }


        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            kaartenBank(true);
        }

        private void WinnerLoser()
        {

        }
    }

}
