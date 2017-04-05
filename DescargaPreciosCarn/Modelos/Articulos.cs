using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DescargaPreciosCarn.Modelos
{
    public class Articulos
    {
        public int bloque { get; set; }
        public string clave { get; set; }
        public string articulo { get; set; }
        public decimal? precLista { get; set; }
        public decimal? precMinimo { get; set; }
        public decimal? precMayoreo { get; set; }
        public decimal? precFilial { get; set; }
        public decimal? precIMSS { get; set; }
        public decimal? medioMayoreo { get; set; }
    }

    public class ArticulosFB
    {
        public long articuloId { get; set; }
        public string articulo { get; set; }
        public string cveArticulo { get; set; }
        public string estatus { get; set; }
    }

    public class PrecioArticulo
    {
        public long precioArticuloId { get; set; }
        public long articuloId { get; set; }
        public long precioEmpresaId { get; set; }
        public decimal precio { get; set; }
        public int monedaId { get; set; }
        public decimal margen {get;set;}
        public string fechaHoraUltModif { get; set; }
    }
}
