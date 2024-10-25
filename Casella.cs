using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace JocNums
{
    class Casella : ContentControl
    {
        SolidColorBrush colorFonsCorrecte = new SolidColorBrush(Colors.Green);
        SolidColorBrush colorFonsIncorrecte = new SolidColorBrush(Colors.Red);
        SolidColorBrush pinzellMarc = new SolidColorBrush(Colors.Black);
        SolidColorBrush colorLletraCorrecte = new SolidColorBrush(Colors.White);
        SolidColorBrush colorLletraIncorrecte = new SolidColorBrush(Colors.Black);
        TextBlock textBlock;
        string text;
        int fila;
        int columna;
        int valorActual;
        int valorDesitjat;
        bool esVisible;

        public Casella()
        {
            Border marc = new Border();
            textBlock = new TextBlock();
            Viewbox viewBox = new Viewbox();

            viewBox.Child = textBlock;
            marc.Child = viewBox;
            marc.BorderThickness = new Thickness(1);
            marc.BorderBrush = pinzellMarc;

            textBlock.FontSize = 15;
            textBlock.HorizontalAlignment = HorizontalAlignment.Center;
            textBlock.VerticalAlignment = VerticalAlignment.Center;

            this.Content = marc;
        }

        public string Text
        {
            get { return textBlock.Text; }
            set { textBlock.Text = value; }
        }

        public bool EsVisible
        {
            get { return esVisible; }
            set
            {
                esVisible = value;
                if (esVisible)
                {
                    this.Visibility = Visibility.Visible;
                }
                else
                    this.Visibility = Visibility.Hidden;
            }
        }

        public bool EstaBenColocada => ValorActual == ValorDesitjat;

        public int Fila { get => fila; set => fila = value; }
        public int Columna { get => columna; set => columna = value; }
        public int ValorActual { get => valorActual; set => valorActual = value; }
        public int ValorDesitjat { get => valorDesitjat; set => valorDesitjat = value; }

        public void ActualizarColores()
        {
            if (EstaBenColocada)
            {
                ((Border)this.Content).Background = colorFonsCorrecte;
                textBlock.Foreground = colorLletraCorrecte; 
            }
            else
            {
                ((Border)this.Content).Background = colorFonsIncorrecte;
                textBlock.Foreground = colorLletraIncorrecte;
            }
        }
    }
}
