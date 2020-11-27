using System;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ApagadoAutomatico
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool bInicia = false;

        private string sHoras = "00";
        private string sMinutos = "45";
        private string sSegundos = "00";

        private int nHoras = 0;
        private int nMinutos = 0;
        private int nSegundos = 0;

        private Timer tmContador;

        public MainWindow()
        {
            InitializeComponent();

            lbl_Horas.Content = sHoras;
            lbl_Minutos.Content = sMinutos;
            lbl_Segundos.Content = sSegundos;

            tmContador = new Timer(1000);
            tmContador.Elapsed += new ElapsedEventHandler(dispararTimer);
            tmContador.Start();
        }

        public void dispararTimer(object sender, ElapsedEventArgs e)
        {
            if (bInicia)
            {
                this.Dispatcher.Invoke(() =>
                {
                    if (reducirValor(lbl_Segundos) == 59)
                    {
                        if (reducirValor(lbl_Minutos) == 59)
                        {
                            reducirValor(lbl_Horas);
                        }
                    }
                });
            }
        }

        public void aumentarValor(Label label)
        {
            int nActual = Int32.Parse(label.Content.ToString());
            
            nActual++;
            if (nActual == 60)
            {
                nActual = 0;
            }

            string sNuevo = rellenar(nActual+"");
            label.Content = sNuevo;
        }

        public int reducirValor(Label label)
        {
            int nActual = Int32.Parse(label.Content.ToString());
            nActual--;
            if (nActual == -1)
            {
                nActual = 59;
            }

            string sNuevo = rellenar(nActual + "");
            label.Content = sNuevo;
            
            return nActual;
        }

        public string rellenar(string sValor)
        {
            if (sValor.Length != 2)
            {
                return rellenar("0"+sValor);
            }
            else
            {
                return sValor;
            }
        }

        public int calcularTiempo()
        {
            nHoras = Int32.Parse(lbl_Horas.Content.ToString());
            nMinutos = Int32.Parse(lbl_Minutos.Content.ToString());
            nSegundos = Int32.Parse(lbl_Segundos.Content.ToString());

            nHoras *= 60 * 60;
            nMinutos *= 60;
            nSegundos += nHoras + nMinutos;

            return nSegundos;
        }

        public void cambiarVisibilidad()
        {
            if (bInicia)
            {
                btn_MasHoras.Visibility = Visibility.Hidden;
                btn_MasMinutos.Visibility = Visibility.Hidden;
                btn_MasSegundos.Visibility = Visibility.Hidden;

                btn_MenosHoras.Visibility = Visibility.Hidden;
                btn_MenosMinutos.Visibility = Visibility.Hidden;
                btn_MenosSegundos.Visibility = Visibility.Hidden;
            }
            else
            {
                btn_MasHoras.Visibility = Visibility.Visible;
                btn_MasMinutos.Visibility = Visibility.Visible;
                btn_MasSegundos.Visibility = Visibility.Visible;

                btn_MenosHoras.Visibility = Visibility.Visible;
                btn_MenosMinutos.Visibility = Visibility.Visible;
                btn_MenosSegundos.Visibility = Visibility.Visible;
            }
        }

        public void activarApagado()
        {
            bInicia = !bInicia;

            if (bInicia)
            {
                btn_Iniciar.Content = "Cancelar";
                Process.Start("shutdown", "/a ");
            }
            else
            {
                int nSegundosTotales = calcularTiempo();
                btn_Iniciar.Content = "Iniciar";
                Process.Start("shutdown", "/s /t " + nSegundosTotales);
            }

            cambiarVisibilidad();
        }

        #region Eventos

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                this.DragMove();
            }
            catch
            {

            }
        }

        private void btn_MasHoras_Click(object sender, RoutedEventArgs e)
        {
            aumentarValor(lbl_Horas);
        }

        private void btn_MasMinutos_Click(object sender, RoutedEventArgs e)
        {
            aumentarValor(lbl_Minutos);
        }

        private void btn_MasSegundos_Click(object sender, RoutedEventArgs e)
        {
            aumentarValor(lbl_Segundos);
        }

        private void btn_MenosHoras_Click(object sender, RoutedEventArgs e)
        {
            reducirValor(lbl_Horas);
        }

        private void btn_MenosMinutos_Click(object sender, RoutedEventArgs e)
        {
            reducirValor(lbl_Minutos);
        }

        private void btn_MenosSegundos_Click(object sender, RoutedEventArgs e)
        {
            reducirValor(lbl_Segundos);
        }

        private void btn_Iniciar_Click(object sender, RoutedEventArgs e)
        {
            activarApagado();
        }

        private void btn_Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
