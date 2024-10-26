using System;
using System.Windows;
using System.Windows.Threading;

namespace JocNums
{
    public partial class MainWindow : Window
    {
        public int numRows;
        public int numColumns;
        DispatcherTimer cronometre = new DispatcherTimer();
        TimeSpan acumulat = TimeSpan.Zero;
        DateTime inici = DateTime.Now;
        private wndInici owner;

        public MainWindow(wndInici owner)
        {
            InitializeComponent();
            this.owner = owner;

            cronometre.Tick += Cronometre_Tick;

            numRows = Convert.ToInt32(owner.numFiles.Text);
            numColumns = Convert.ToInt32(owner.numColumnes.Text);
            grTauler.NFILES = numRows;
            grTauler.NCOLUMNES = numColumns;

            grTauler.Inicialitza(this);
            IniciarCronometro();
        }

        private void IniciarCronometro()
        {
            inici = DateTime.Now; 
            cronometre.Start(); 
        }

        private void Cronometre_Tick(object? sender, EventArgs e)
        {
            EscriuTemps(); 
        }

        private void EscriuTemps()
        {
            DateTime ara = DateTime.Now; 
            TimeSpan diferencia = ara.Subtract(inici);
            diferencia = diferencia.Add(acumulat);

            
            txtCrono.Text =
                String.Format($"{diferencia.Hours:00}:{diferencia.Minutes:00}:{diferencia.Seconds:00}:{diferencia.Milliseconds:0}"); 
        }

        public void Tanca()
        {
            cronometre.Stop();
            owner.txtTempsFinal.Text = $"Ultim temps registrat - {grTauler.FINALTIME}"; 
            this.Close(); 
        }
    }
}
