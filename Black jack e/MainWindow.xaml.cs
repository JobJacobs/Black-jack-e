using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Black_jack_e
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> newDeck = new List<string>();
        Random rnd = new Random();
        List<int> numberSpeler = new List<int>();
        List<int> numberBank = new List<int>();
        string state = "start";
        public MainWindow()
        {
            InitializeComponent();
            veranderstaat("start");
            newDeck=vulDeck();
            
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
        private List<string> vulDeck()
        {
            List<string> kaarten = new List<string>();
               string[] suits = { "Clubs", "Diamonds", "Hearts", "Spades" };
            string[] ranks = { "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King", "Ace" };
            foreach (string suit in suits)
            {
                foreach (string rank in ranks)
                {
                    kaarten.Add(suit.ToLower() +"_"+ rank.ToLower()+".png");
                    
                }
            }
            return kaarten;
        } 

        private void GeefKaart(bool isSpeler)
        {
            if(newDeck.Count< 10)
            {
                newDeck= vulDeck();
            }
            string kaart = newDeck[rnd.Next(newDeck.Count)];
            kaart1Speler.Source = new BitmapImage(new Uri($"kaarten/{kaart}", UriKind.Relative));
            //geef de waarde van de kaard aan de speler

            string waarde = kaart.Split('.')[0].Split('_')[1];
            if (waarde == "jack" || waarde == "queen" || waarde == "king")
            {
                waarde = "10";
            }
            if(waarde== "ace")
            {
                waarde = "11";
            }
            numberSpeler.Add(Convert.ToInt32(waarde));
            //verwijder kaar uit dek
            newDeck.Remove(kaart);



            /*int kaart = rnd.Next(1, 11);
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
            }*/
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
                    if (numberBank.Sum() > numberSpeler.Sum() && numberBank.Sum() < 22)
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
            int bedrag = Convert.ToInt32(LblgeldInBank.Content);
            bedrag = Convert.ToInt32(bedrag * (e.NewValue / 100));
            LblgeldInBank.Content=100-bedrag;
            LblInzet.Content = bedrag.ToString();
        }
    }

}
