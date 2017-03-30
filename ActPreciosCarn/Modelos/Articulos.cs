using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActPreciosCarn.Modelos
{
    public class Articulos
    {
        public bool seleccionado { get; set; }
        public long idArticulo { get; set; }
        public string clave { get; set; }
        public string articulo { get; set; }
        public decimal? precLista { get; set; }
        public decimal? precMinimo { get; set; }
        public decimal? precMayoreo { get; set; }
        public decimal? precFilial { get; set; }
        public decimal? precIMSS { get; set; }
        public decimal? medioMayoreo { get; set; }
    }
}
