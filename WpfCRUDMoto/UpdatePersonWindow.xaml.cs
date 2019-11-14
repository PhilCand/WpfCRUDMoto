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
    /// Logique d'interaction pour UpdatePersonWindow.xaml
    /// </summary>
    public partial class UpdatePersonWindow : Window
    {

        internal Personne SelectedPersonne { get; set; }

        public UpdatePersonWindow()
        {
            InitializeComponent();
        }

        public UpdatePersonWindow(Personne selectedPersonne)
        {
            InitializeComponent();
            SelectedPersonne = selectedPersonne;
            txtbUpdatePersonName.Text = SelectedPersonne.Nom;
        }
        

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnUpdatePerson_Click(object sender, RoutedEventArgs e)
        {            

            SelectedPersonne.Nom = txtbUpdatePersonName.Text;

            DAL.UpdatePerson(SelectedPersonne);

            this.Close();
        }
    }
}
