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
using System.Windows.Shapes;

namespace WpfCRUDMoto
{
    /// <summary>
    /// Logique d'interaction pour CotisationsWindow.xaml
    /// </summary>
    public partial class CotisationsWindow : Window
    {

        public Personne Motard { get;  set; }

        public CotisationsWindow()
        {
            InitializeComponent();
        }

        public CotisationsWindow(Personne motard)
        {
            InitializeComponent();
            Motard = motard;
            labelMotard.Content = motard.Nom;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listeCotisations.ItemsSource = DAL.GetCotisation(Motard);
        }

        private void BtnAjoutCotis_Click(object sender, RoutedEventArgs e)
        {
            Cotisation newCotiz = new Cotisation();

            double montant;
            bool isOK = double.TryParse(txtbMontant.Text, out montant);
            if(isOK) newCotiz.Montant = montant;
            else
            {
                MessageBox.Show("Montant incorrect", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int annee;
            bool isYOK = int.TryParse(txtbAnnee.Text, out annee);
            if (isYOK && annee > 1950 && annee <= DateTime.Now.Year) newCotiz.Annee = annee;
            else
            {
                MessageBox.Show("Année incorrect", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            newCotiz.Annee = annee;


            newCotiz.MotardId = Motard.Id;
            DAL.CreateCotisation(newCotiz);
            listeCotisations.ItemsSource = DAL.GetCotisation(Motard);
        }

        private void BtnSupprCotis_Click(object sender, RoutedEventArgs e)
        {
            DAL.DeleteCotisation((listeCotisations.SelectedItem as Cotisation).Id);
            listeCotisations.ItemsSource = DAL.GetCotisation(Motard);
            if (listeCotisations.Items.Count > 0) listeCotisations.SelectedIndex = 0;
        }

        private void BtnFermerCotis_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
