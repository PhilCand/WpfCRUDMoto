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

namespace WpfCRUDMoto
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listePersonne.ItemsSource = DAL.GetPersonMoto();
        }

        private void BtnSupprimerPersonne_Click(object sender, RoutedEventArgs e)
        {

            Personne unMotard = listePersonne.SelectedItem as Personne;
            DAL.DeletePerson(unMotard.Id);
            listePersonne.ItemsSource = DAL.GetPersonMoto();

            if (listePersonne.Items.Count > 0) listePersonne.SelectedIndex = 0;

        }

        private void BtnAjoutPersonne_Click(object sender, RoutedEventArgs e)
        {
            NewPersonWindow n = new NewPersonWindow();
            n.ShowDialog();
            listePersonne.ItemsSource = DAL.GetPersonMoto();
        }

        private void ListePersonne_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listePersonne.SelectedIndex < 0) return;

            listeMoto.Items.Clear();

            Personne selectedMotard = listePersonne.SelectedItem as Personne;

            foreach (Moto selectedMoto in selectedMotard.Garage)
            {
                listeMoto.Items.Add(selectedMoto);
            }
        }

        private void BtnAjoutMoto_Click(object sender, RoutedEventArgs e)
        {
            int index = listePersonne.SelectedIndex;
            NewMotoWindow nm = new NewMotoWindow((listePersonne.SelectedItem as Personne).Id);
            nm.ShowDialog();
            listePersonne.ItemsSource = DAL.GetPersonMoto();
            listePersonne.SelectedIndex = index;
        }

        private void BtnSupprimerMoto_Click(object sender, RoutedEventArgs e)
        {
            int index = listePersonne.SelectedIndex;
            Moto uneMoto = listeMoto.SelectedItem as Moto;
            DAL.DeleteMoto(uneMoto.Id);
            listePersonne.ItemsSource = DAL.GetPersonMoto();
            listePersonne.SelectedIndex = index;
            if (listeMoto.Items.Count > 0) listeMoto.SelectedIndex = 0;
        }

        private void BtnUpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            int index = listePersonne.SelectedIndex;
            UpdatePersonWindow u = new UpdatePersonWindow((listePersonne.SelectedItem as Personne));
            u.ShowDialog();
            listePersonne.ItemsSource = DAL.GetPersonMoto();
            listePersonne.SelectedIndex = index;
        }

        private void BtnEditerMoto_Click(object sender, RoutedEventArgs e)
        {
            int index = listePersonne.SelectedIndex;
            UpdateMotoWindow um = new UpdateMotoWindow(listeMoto.SelectedItem as Moto);
            um.ShowDialog();
            listePersonne.ItemsSource = DAL.GetPersonMoto();
            listePersonne.SelectedIndex = index;
            if (listeMoto.Items.Count > 0) listeMoto.SelectedIndex = 0;

        }

        private void BtnQuitterAppli_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Voulez vous vraiment quitter ?", "Exit", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.No) e.Cancel = true;
        }

        private void BtnAjoutdata_Click(object sender, RoutedEventArgs e)
        {
            int newMotardId = DAL.CreatePerson(new Personne{ Nom = "toto"});
            DAL.CreateMoto(new Moto { Marque = "Honda", Cylindree = 500, IdProprietaire = newMotardId });
            DAL.CreateMoto(new Moto { Marque = "Kawasaki", Cylindree = 850, IdProprietaire = newMotardId });
            DAL.CreateCotisation(new Cotisation { Annee = 2018, Montant = 200, MotardId = newMotardId });
            DAL.CreateCotisation(new Cotisation { Annee = 2019, Montant = 250, MotardId = newMotardId });

            newMotardId = DAL.CreatePerson(new Personne { Nom = "tata" });
            DAL.CreateCotisation(new Cotisation { Annee = 2018, Montant = 200, MotardId = newMotardId });

            newMotardId = DAL.CreatePerson(new Personne { Nom = "tutu" });
            DAL.CreateMoto(new Moto { Marque = "Peugeot", Cylindree = 125, IdProprietaire = newMotardId });
            DAL.CreateCotisation(new Cotisation { Annee = 2019, Montant = 250, MotardId = newMotardId });

            listePersonne.ItemsSource = DAL.GetPersonMoto();

        }

        private void BtnCotisations_Click(object sender, RoutedEventArgs e)
        {
            CotisationsWindow cw = new CotisationsWindow(listePersonne.SelectedItem as Personne);
            cw.ShowDialog();
            listePersonne.ItemsSource = DAL.GetPersonMoto();

        }
    }
}
