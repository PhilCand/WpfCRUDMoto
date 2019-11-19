using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

            PersonneValidator validator = new PersonneValidator();
            FluentValidation.Results.ValidationResult results = validator.Validate(SelectedPersonne);

            if (results.IsValid == false)
            {
                foreach (ValidationFailure failure in results.Errors)
                {
                    MessageBox.Show($"{failure.ErrorMessage}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

                }
                return;
            }

            DAL.UpdatePerson(SelectedPersonne);

            this.Close();
        }


    }
}
