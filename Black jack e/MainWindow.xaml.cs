using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;

namespace Black_jack_e
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int geldInbank = 100;
        
        List<string> newDeck = new List<string>();
        Random rnd = new Random();
        List<int> numberSpeler = new List<int>();
        List<int> numberBank = new List<int>();
        

        
        public MainWindow()
        {
            InitializeComponent();
            Veranderstaat("start");
            newDeck=VulDeck();
            
        }
        private void Veranderstaat(string niewState)
        {
            
            switch (niewState)
            {
                case "start":
                    
                    BtnDeel.IsEnabled = true;
                    BtnHit.IsEnabled = false;
                    BtnStand.IsEnabled = false;
                    BedragSlider.Visibility = Visibility.Visible;
                    BtnNieuwRonde.Visibility = Visibility.Hidden;
                    BtnNieuwSpel.Visibility = Visibility.Hidden;
                    BtnDeel.Visibility = Visibility.Visible;
                    BtnHit.Visibility = Visibility.Visible;
                    BtnStand.Visibility = Visibility.Visible;
                    //LstSpeler.Items.Clear();
                    //LstBank.Items.Clear();
                    numberSpeler.Clear();
                    numberBank.Clear();
                    LbLBankNummer.Content = "0";
                    LblSpelerNummer.Content = "0";
                    LblVerloren.Visibility = Visibility.Hidden;
                    LblGewonen.Visibility = Visibility.Hidden;
                    LblPush.Visibility = Visibility.Hidden;
                    if (TxtGeldInBank.Text == "0" && TxtInzet.Text == "0")
                    {
                        TxtGeldInBank.Text = "100";
                        Veranderstaat("start");
                    }

                    break;
                case "speelfase":
                    BtnDeel.IsEnabled = false;
                    BtnHit.IsEnabled = true;
                    BtnStand.IsEnabled = true;
                    BedragSlider.Visibility = Visibility.Hidden;
                    break;
                case "gewonnen":
                    BtnDeel.IsEnabled = false;
                    BtnHit.IsEnabled = false;
                    BtnStand.IsEnabled = false;
                    BtnDeel.Visibility = Visibility.Hidden;
                    BtnHit.Visibility = Visibility.Hidden;
                    BtnStand.Visibility = Visibility.Hidden;
                    if(TxtGeldInBank.Text!="0" && TxtInzet.Text!="0")
                    {                   
                    BtnNieuwRonde.Visibility = Visibility.Visible;
                    BtnNieuwSpel.Visibility = Visibility.Hidden;
                    }
                    break;
            }
        }
        
        private void BtnDeel_Click(object sender, RoutedEventArgs e)
        {
            int geldBank = int.Parse(TxtGeldInBank.Text);
            int inzet = int.Parse(TxtInzet.Text);
            geldBank -= inzet;
            //geld gaat van bank af
            TxtGeldInBank.Text = geldBank.ToString();
            Veranderstaat("speelfase");
            //start spel
            GeefKaart(true, numberSpeler,LblSpelerNummer,SpelerKaartContainer);
            GeefKaart(true, numberSpeler, LblSpelerNummer, SpelerKaartContainer);
            // geeft kaart aan speler 
            GeefKaart(true, numberBank, LbLBankNummer, BankKaartContainer);
            //Geef Kaart aan bank
            
        }

        //apend line 
        //apend
        private List<string> VulDeck()
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
        /// <summary> 
        /// This method calculates the square root of a number. 
        /// To use it, call the method like this:
        /// if(het dek < 1)
        /// maak nieuw dek aan       
        /// </summary>
        private void GeefKaart(bool isSpeler , List<int> spelerBank, Label label, StackPanel kaartcontainer)
        {           
            if(newDeck.Count< 1)
            {
                newDeck= VulDeck();
                TxtKaartenInDeck.Text = "52";
            }
            string kaart = newDeck[rnd.Next(newDeck.Count)];
            
            Image image = new Image();
            image.MaxHeight = 80;            
            image.Source = new BitmapImage(new Uri($"kaarten/{kaart}", UriKind.Relative));           
            kaartcontainer.Children.Add(image);
            string waarde = kaart.Split('.')[0].Split('_')[1];
            if (waarde == "jack" || waarde == "queen" || waarde == "king")
            {
                waarde = "10";
            }
            if(waarde== "ace")
            {
                waarde = "11";
            }
            spelerBank.Add(Convert.ToInt32(waarde));            
            int som = 0;
            newDeck.Remove(kaart);
            //verwijder kaar uit dek
            string kaartCounter = TxtKaartenInDeck.Text;
            
            foreach (var item in spelerBank)
            {
                
                som += item;
                
            }           

            label.Content = som;

            if (som > 21 && spelerBank.Contains(11))
            {
                som -= 10;
                label.Content = som;                
            }
            
            if (som > 21)
            {
                MessageBox.Show("busted");
                LblVerloren.Visibility = Visibility.Visible;
                Veranderstaat("gewonnen");               
                TxtInzet.Text = "0";
                blut();
                return;
            }            
                       
        }

        private void BtnHit_Click(object sender, RoutedEventArgs e)
        {
            GeefKaart(true, numberSpeler, LblSpelerNummer, SpelerKaartContainer);


        }
        private void Winst()
        {
            double kapitaal = double.Parse(TxtGeldInBank.Text);
            double inzet = double.Parse(TxtInzet.Text);
            kapitaal += inzet * 2;
            TxtGeldInBank.Text = kapitaal.ToString();
            //numberBank.Sum()

        }
        private void Push()
        {
            double kapitaal = double.Parse(TxtGeldInBank.Text);
            double inzet = double.Parse(TxtInzet.Text);
            kapitaal += inzet;
            TxtGeldInBank.Text = kapitaal.ToString();
        }
        private void BtnStand_Click(object sender, RoutedEventArgs e)
        {
            bool bankStop = false;
            
            do
            {

                GeefKaart(false, numberBank,LbLBankNummer, BankKaartContainer);
                if (numberBank.Sum() > 16)
                {
                    if (numberBank.Sum() > numberSpeler.Sum() && numberBank.Sum() < 21)
                    {
                        bankStop = true;
                        LblVerloren.Visibility = Visibility.Visible;
                        TxtInzet.Text = "0";
                        BtnNieuwRonde.Visibility = Visibility.Visible;
                        blut();
                    }
                    else if (numberBank.Sum() == numberSpeler.Sum())
                    {
                        bankStop = true;
                        LblPush.Visibility = Visibility.Visible;
                        LblVerloren.Visibility = Visibility.Hidden;
                        Push();
                    }
                    else if (numberBank.Sum() > 21)
                    {
                        bankStop = true;                        
                        LblGewonen.Visibility = Visibility.Visible;
                        LblVerloren.Visibility = Visibility.Hidden;
                        Winst();                        
                    }
                }

            } while (bankStop == false);
            Veranderstaat("gewonnen");
            
        }       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BedragSlider.Maximum = geldInbank;
        }

        private void BedragSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> waardeslider)
        {
                        
            int bedrag = Convert.ToInt32(TxtGeldInBank.Text);
            bedrag = Convert.ToInt32(bedrag * (waardeslider.NewValue / 100));            
            TxtInzet.Text = bedrag.ToString();
            
        }


        private void BtnNieuwRonde_Click(object sender, RoutedEventArgs e)
        {
            spelreset();
        }
        
        private void blut()
        {
            if (TxtGeldInBank.Text =="0" && TxtInzet.Text =="0")
            {
                
                MessageBox.Show("blut");
                BedragSlider.Visibility = Visibility.Hidden;
                BtnNieuwSpel.Visibility = Visibility.Visible;
                BtnNieuwRonde.Visibility = Visibility.Hidden;
            }
            
            
        }
        private void spelreset()
        {
            Veranderstaat("start");
            SpelerKaartContainer.Children.Clear();
            BankKaartContainer.Children.Clear();
            TxtInzet.Text = Math.Floor((Convert.ToInt32(TxtGeldInBank.Text) * 0.1)).ToString();
        }

        private void BtnNieuwspel_Click(object sender, RoutedEventArgs e)
        {
            spelreset();
            Veranderstaat("blut");
        }
    }

}
