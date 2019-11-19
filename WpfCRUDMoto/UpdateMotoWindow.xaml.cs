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
    /// Logique d'interaction pour UpdateMotoWindow.xaml
    /// </summary>
    public partial class UpdateMotoWindow : Window
    {

        internal Moto SelectedMoto { get; set; }

        public UpdateMotoWindow()
        {
            InitializeComponent();
        }

        public UpdateMotoWindow(Moto selectedMoto, int index)
        {
            InitializeComponent();
            SelectedMoto = selectedMoto;
            txtMarque.Text = SelectedMoto.Marque;
            txtCylindree.Text = SelectedMoto.Cylindree.ToString();
            cboxProprio.ItemsSource = DAL.GetPersonMoto();
            cboxProprio.SelectedIndex = index;

        }

        private void BtnUpdateMoto_Click(object sender, RoutedEventArgs e)
        {

            SelectedMoto.Marque = txtMarque.Text;
            
            int cyl;
            bool isOK = Int32.TryParse(txtCylindree.Text, out cyl);
            if (isOK && cyl < 30000) SelectedMoto.Cylindree = cyl;
            else
            {
                MessageBox.Show("Cylindree incorrecte", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SelectedMoto.IdProprietaire = (cboxProprio.SelectedItem as Personne).Id;

            MotoValidator validator = new MotoValidator();
            FluentValidation.Results.ValidationResult results = validator.Validate(SelectedMoto);

            if (results.IsValid == false)
            {
                foreach (ValidationFailure failure in results.Errors)
                {
                    MessageBox.Show($"{failure.ErrorMessage}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                return;
            }


            DAL.UpdateMoto(SelectedMoto);
            this.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
