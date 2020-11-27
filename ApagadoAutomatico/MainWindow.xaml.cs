using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ApagadoAutomatico
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool bCancelar = false;
        bool bInicia = false;
        System.Timers.Timer tmContador;
        Label lbl_Segundoss = null;
        Label lbl_Minutoss = null;
        Label lbl_Horass = null;

        public MainWindow()
        {
            InitializeComponent();

            tmContador = new System.Timers.Timer(1000);
            tmContador.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            lbl_Segundoss = lbl_Segundos;
            lbl_Minutoss = lbl_Minutos;
            lbl_Horass = lbl_Horas;
        }

        public void subirValor(Label label)
        {
            int nActual = Int32.Parse(label.Content.ToString());
            
            nActual++;
            if (nActual == 60)
            {
                nActual = 0;
            }

            string sNuevo = fill(nActual+"");
            label.Content = sNuevo;
        }

        public void reducirValor(Label label)
        {
            int nActual = Int32.Parse(label.Content.ToString());
            nActual--;
            if (nActual == -1)
            {
                nActual = 59;
            }

            string sNuevo = fill(nActual + "");
            label.Content = sNuevo;
        }

        public string fill(string sValor)
        {
            if (sValor.Length != 2)
            {
                return fill("0"+sValor);
            }
            else
            {
                return sValor;
            }
        }

        public int calcularTiempo()
        {
            int nHoras = Int32.Parse(lbl_Horas.Content.ToString());
            int nMinutos = Int32.Parse(lbl_Minutos.Content.ToString());
            int nSegundos = Int32.Parse(lbl_Segundos.Content.ToString());

            nHoras *= 60 * 60;
            nMinutos *= 60;
            nSegundos += nHoras + nMinutos;

            return nSegundos;
        }

        public void apagar(int nSegundos)
        {
            //Process.Start("shutdown", "/s /t "+ nSegundos);
            bInicia = true;
            tmContador.Start();
        }

        public void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (bInicia)
            {
                reducirValor(lbl_Segundoss);
            }
            else
            {

            }
        }


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
            subirValor(lbl_Horas);
        }

        private void btn_MasMinutos_Click(object sender, RoutedEventArgs e)
        {
            subirValor(lbl_Minutos);
        }

        private void btn_MasSegundos_Click(object sender, RoutedEventArgs e)
        {
            subirValor(lbl_Segundos);
        }

        private void btn_MenosHoras_Click(object sender, RoutedEventArgs e)
        {
            reducirValor(lbl_Horass);
        }

        private void btn_MenosMinutos_Click(object sender, RoutedEventArgs e)
        {
            reducirValor(lbl_Minutoss);
        }

        private void btn_MenosSegundos_Click(object sender, RoutedEventArgs e)
        {
            reducirValor(lbl_Segundoss);
        }

        private void btn_Iniciar_Click(object sender, RoutedEventArgs e)
        {
            apagar(calcularTiempo());
        }

        private void btn_Cerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
