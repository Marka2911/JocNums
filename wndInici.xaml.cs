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

namespace JocNums
{
    /// <summary>
    /// Lógica de interacción para wndInici.xaml
    /// </summary>
    public partial class wndInici : Window
    {
        public wndInici()
        {
            InitializeComponent();
        }

        private void btnEnvia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(numColumnes.Text) < 2 || Convert.ToInt32(numColumnes.Text) > 20
                    ||
                    Convert.ToInt32(numFiles.Text) < 2 || Convert.ToInt32(numFiles.Text) > 20)
                {
                    MessageBox.Show("El num de columnes i files té que estar entre 2 i 20", "Error", MessageBoxButton.OK
                        , MessageBoxImage.Error);
                }
                else
                {
                    MainWindow finestra = new MainWindow(this);
                    finestra.Owner = this;
                    finestra.Show();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("El num de columnes i files no pot ser null i té que ser un numero", "Error", MessageBoxButton.OK
                    , MessageBoxImage.Error);
            }
        }
    }
}
