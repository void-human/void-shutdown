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
            tmContador.Elapsed += new ElapsedEventHandler(SegundosContar);
            tmContador.Start();
        }

        public void SegundosContar(object sender, ElapsedEventArgs e)
        {
            if (bInicia)
            {
                this.Dispatcher.Invoke(() =>
                {
                    if (Reducir(lbl_Segundos) == 59)
                    {
                        if (Reducir(lbl_Minutos) == 59)
                        {
                            Reducir(lbl_Horas);
                        }
                    }
                });
            }
        }

        public void Aumentar(Label label)
        {
            int nActual = Int32.Parse(label.Content.ToString());
            
            nActual++;
            if (nActual == 60)
            {
                nActual = 0;
            }

            string sNuevo = Formato.RellenarIzquierda(nActual+"","0",2);
            label.Content = sNuevo;
        }

        public int Reducir(Label label)
        {
            int nActual = Int32.Parse(label.Content.ToString());
            nActual--;
            if (nActual == -1)
            {
                nActual = 59;
            }

            string sNuevo = Formato.RellenarIzquierda(nActual + "", "0", 2);
            label.Content = sNuevo;
            
            return nActual;
        }

        public int TiempoSegundos()
        {
            nHoras = Int32.Parse(lbl_Horas.Content.ToString());
            nMinutos = Int32.Parse(lbl_Minutos.Content.ToString());
            nSegundos = Int32.Parse(lbl_Segundos.Content.ToString());

            nHoras *= 60 * 60;
            nMinutos *= 60;
            nSegundos += nHoras + nMinutos;

            return nSegundos;
        }

        public void CambiarVisibilidad()
        {
            Visibility visibility = Visibility.Visible;
            if (bInicia)
            {
                visibility = Visibility.Hidden;
            }

            btn_MasHoras.Visibility = visibility;
            btn_MasMinutos.Visibility = visibility;
            btn_MasSegundos.Visibility = visibility;

            btn_MenosHoras.Visibility = visibility;
            btn_MenosMinutos.Visibility = visibility;
            btn_MenosSegundos.Visibility = visibility;

        }

        public void Activar()
        {
            bInicia = !bInicia;

            if (!bInicia)
            {
                btn_Iniciar.Content = "Cancelar";
                Process.Start("shutdown", "-a ");
            }
            else
            {
                int nSegundosTotales = TiempoSegundos();
                btn_Iniciar.Content = "Iniciar";
                Process.Start("shutdown", "-s -t " + nSegundosTotales);
            }

            CambiarVisibilidad();
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
            Aumentar(lbl_Horas);
        }

        private void btn_MasMinutos_Click(object sender, RoutedEventArgs e)
        {
            Aumentar(lbl_Minutos);
        }

        private void btn_MasSegundos_Click(object sender, RoutedEventArgs e)
        {
            Aumentar(lbl_Segundos);
        }

        private void btn_MenosHoras_Click(object sender, RoutedEventArgs e)
        {
            Reducir(lbl_Horas);
        }

        private void btn_MenosMinutos_Click(object sender, RoutedEventArgs e)
        {
            Reducir(lbl_Minutos);
        }

        private void btn_MenosSegundos_Click(object sender, RoutedEventArgs e)
        {
            Reducir(lbl_Segundos);
        }

        private void btn_Iniciar_Click(object sender, RoutedEventArgs e)
        {
            Activar();
        }

        private void btn_Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
