using FluentValidation.Results;
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
using WpfCRUDMoto.Validators;

namespace WpfCRUDMoto
{
    /// <summary>
    /// Logique d'interaction pour NewMotoWindow.xaml
    /// </summary>
    public partial class NewMotoWindow : Window
    {

        private int _index;

        public int Index { get => _index; set => _index = value; }


        public NewMotoWindow()
        {
            InitializeComponent();
        }

        public NewMotoWindow(int index)
        {
            Index = index;
            InitializeComponent();
        }



        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnCreateMoto_Click(object sender, RoutedEventArgs e)
        {
            Moto newMoto = new Moto();

            newMoto.Marque = txtMarque.Text;

            int cyl;
            bool isOK = Int32.TryParse(txtCylindree.Text, out cyl);
            if (isOK && cyl < 30000) newMoto.Cylindree = cyl;
            else
            {
                MessageBox.Show("Cylindree incorrecte", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            newMoto.IdProprietaire = Index;

            MotoValidator validator = new MotoValidator();
            FluentValidation.Results.ValidationResult results = validator.Validate(newMoto);

            if (results.IsValid == false)
            {
                foreach (ValidationFailure failure in results.Errors)
                {
                    MessageBox.Show($"{failure.ErrorMessage}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                return;
            }

            DAL.CreateMoto(newMoto);

            this.Close();


        }

      

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            txtMarque.Focus();
        }
    }
}
