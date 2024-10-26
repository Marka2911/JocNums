using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace JocNums
{
    public class Tauler : Grid
    {
        int NFiles;
        int NColumnes;
        Casella CasellaBuida;
        int NumCasellesBenColocades;
        bool EstaSolucionat;
        string finalTime;
        private MainWindow mainWindow;
        public Tauler()
        {

        }
        public string FINALTIME
        {
            get { return finalTime; }
            set { finalTime = value; }
        }

        public int NFILES
        {
            get { return NFiles; }
            set { NFiles = value; }
        }

        public int NCOLUMNES
        {
            get { return NColumnes; }
            set { NColumnes = value; }
        }

        public bool ESTASOLUCIONAT 
        {

            get { return EstaSolucionat; }
            set { EstaSolucionat = value; }
        }

        Random rnd = new Random();

        public List<int> nombresRandoms(int nFiles, int nColumnes)
        {
            List<int> rndNums = new List<int>();
            for (int i = 0; i < nFiles * nColumnes - 1; i++)
            {
                bool add = false;
                while (!add)
                {
                    int num = rnd.Next(1, nFiles * nColumnes);
                    if (!rndNums.Contains(num))
                    {
                        rndNums.Add(num);
                        add = true;
                    }
                }
            }

            int desordres = 0;
            for (int i = 0; i < rndNums.Count; i++)
            {
                for (int j = i + 1; j < rndNums.Count; j++)
                {
                    if (rndNums[i] > rndNums[j])
                    {
                        desordres++;
                    }
                }
            }

            if (desordres % 2 != 0)
            {
                int temp = rndNums[rndNums.Count - 1];
                rndNums[rndNums.Count - 1] = rndNums[rndNums.Count - 2];
                rndNums[rndNums.Count - 2] = temp;
            }

            rndNums.Add(0);
            return rndNums;
        }

        public void Inicialitza(MainWindow mainWindow)
        {
            RowDefinitions.Clear();
            ColumnDefinitions.Clear();
            this.mainWindow = mainWindow;

            for (int i = 0; i < NFiles; i++)
            {
                RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < NColumnes; i++)
            {
                ColumnDefinitions.Add(new ColumnDefinition());
            }

            List<int> rndNums = nombresRandoms(NFiles, NColumnes);
            int idx = 0;
            for (int i = 0; i < NFiles; i++)
            {
                for (int j = 0; j < NColumnes; j++)
                {
                    if (idx < rndNums.Count)
                    {
                        Casella casella = new Casella
                        {
                            Fila = i,
                            Columna = j,
                            ValorActual = rndNums[idx],
                            ValorDesitjat = idx + 1
                        };

                        if (casella.ValorActual == 0)
                        {
                            casella.Text = string.Empty;
                        }
                        else
                        {
                            casella.Text = casella.ValorActual.ToString();
                        }

                        casella.EsVisible = casella.ValorActual != 0;

                        casella.ActualizarColores();

                        if (casella.EstaBenColocada == true && casella.EsVisible)
                        {
                            NumCasellesBenColocades++;
                        }

                        if (NumCasellesBenColocades == NFiles * NColumnes - 1)
                        {
                            EstaSolucionat = true;
                        }
                        Grid.SetRow(casella, i);
                        Grid.SetColumn(casella, j);
                        Children.Add(casella);
                        idx++;

                        if (casella.ValorActual == 0)
                        {
                            CasellaBuida = casella;
                        }

                        casella.MouseDown += MouFitxa;
                    }
                }
            }
        }


        private void MouFitxa(object sender, MouseButtonEventArgs e)
        {
            Casella clickedCasella = (Casella)sender;

            if (EsBuida(clickedCasella, CasellaBuida))
            {
                int tempValue = clickedCasella.ValorActual;
                string tempText = clickedCasella.Text;

                CasellaBuida.ValorActual = tempValue;
                CasellaBuida.Text = tempText;
                CasellaBuida.EsVisible = true;

                clickedCasella.ValorActual = 0;
                clickedCasella.Text = string.Empty;
                clickedCasella.EsVisible = false;

                CasellaBuida = clickedCasella;

                NumCasellesBenColocades = 0;
                foreach (Casella casella in Children)
                {
                    casella.ActualizarColores();
                    if (casella.EstaBenColocada == true && casella.EsVisible)
                    {
                        NumCasellesBenColocades++;
                    }
                }
                if (NumCasellesBenColocades == NFiles * NColumnes - 1)
                {
                    finalTime = mainWindow.txtCrono.Text;
                    MessageBox.Show("Felicitats, has guanyat!", "Victoria!", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    EstaSolucionat = true;
                    mainWindow.Tanca();
                }
            }
        }

        


        private bool EsBuida(Casella clicked, Casella empty)
        {
            int clickedRow = clicked.Fila;
            int clickedCol = clicked.Columna;
            int emptyRow = empty.Fila;
            int emptyCol = empty.Columna;
            bool esBuida = false;

            if (clickedRow - 1 == emptyRow && clickedCol == emptyCol)
            {
                esBuida = true;
            }

            if (clickedRow + 1 == emptyRow && clickedCol == emptyCol)
            {
                esBuida = true;
            }

            if (clickedCol - 1 == emptyCol && clickedRow == emptyRow)
            {
                esBuida = true;
            }

            if (clickedCol + 1 == emptyCol && clickedRow == emptyRow)
            {
                esBuida = true;
            }

            return esBuida;
        }

    }
}
