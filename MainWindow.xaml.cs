using System;
using System.Windows;

namespace JocNums
{
    public partial class MainWindow : Window
    {
        public int numRows;
        public int numColumns;

        public MainWindow(wndInici owner)
        {
            InitializeComponent();
            numRows = Convert.ToInt32(owner.numFiles.Text);
            numColumns = Convert.ToInt32(owner.numColumnes.Text);

            grTauler.NFILES = numRows;
            grTauler.NCOLUMNES = numColumns;

            grTauler.Inicialitza();
        }
    }
}
