using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApagadoAutomatico
{
    public abstract class Formato
    {

        public static string RellenarDerecha(string sTexto, string sCaracter, int nCantidad)
        {
            if (sTexto.Length == nCantidad)
            {
                return sTexto;
            }
            else
            {
                return RellenarDerecha(sTexto+""+sCaracter, sCaracter, nCantidad);
            }
        }

        public static string RellenarIzquierda(string sTexto, string sCaracter, int nCantidad)
        {
            if (sTexto.Length == nCantidad)
            {
                return sTexto;
            }
            else
            {
                return RellenarIzquierda(sCaracter + "" + sTexto, sCaracter, nCantidad);
            }
        }
    }
}
