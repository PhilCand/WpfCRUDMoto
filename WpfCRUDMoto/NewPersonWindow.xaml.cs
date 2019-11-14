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
    /// Logique d'interaction pour NewPersonWindow.xaml
    /// </summary>
    public partial class NewPersonWindow : Window
    {
        public NewPersonWindow()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnCreatePerson_Click(object sender, RoutedEventArgs e)
        {
            Personne newMotard = new Personne();

            newMotard.Nom = txtbNewPersonName.Text;

            int newMotardID = DAL.CreatePerson(newMotard);

            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtbNewPersonName.Focus();
        }
    }
}
