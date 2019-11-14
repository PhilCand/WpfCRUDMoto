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
    /// Logique d'interaction pour UpdateMotoWindow.xaml
    /// </summary>
    public partial class UpdateMotoWindow : Window
    {

        internal Moto SelectedMoto { get; set; }

        public UpdateMotoWindow()
        {
            InitializeComponent();
        }

        public UpdateMotoWindow(Moto selectedMoto)
        {
            InitializeComponent();
            SelectedMoto = selectedMoto;
            txtMarque.Text = SelectedMoto.Marque;
            txtCylindree.Text = SelectedMoto.Cylindree.ToString();
        }

        private void BtnUpdateMoto_Click(object sender, RoutedEventArgs e)
        {

            SelectedMoto.Marque = txtMarque.Text;
            SelectedMoto.Cylindree = int.Parse(txtCylindree.Text);

            DAL.UpdateMoto(SelectedMoto);
            this.Close();
        }
    }
}
