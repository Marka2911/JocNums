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

        public Tauler()
        {
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

        public void Inicialitza()
        {
            RowDefinitions.Clear();
            ColumnDefinitions.Clear();

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
                            ValorDesitjat = (i * NColumnes + j + 1) % (NFiles * NColumnes)
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

            if (IsAdjacent(clickedCasella, CasellaBuida))
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

                // Update colors of all cells to check if they are now correctly placed
                foreach (UIElement child in Children)
                {
                    if (child is Casella casella)
                    {
                        casella.ActualizarColores();
                    }
                }
            }
        }

        private bool IsAdjacent(Casella clicked, Casella empty)
        {
            int clickedRow = clicked.Fila;
            int clickedCol = clicked.Columna;
            int emptyRow = empty.Fila;
            int emptyCol = empty.Columna;

            return (Math.Abs(clickedRow - emptyRow) == 1 && clickedCol == emptyCol) ||
                   (Math.Abs(clickedCol - emptyCol) == 1 && clickedRow == emptyRow);
        }
    }
}
