using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActPreciosCarn.Modelos
{
    public class ActualizacionDet
    {
        public int idActualizacionDet { get; set; }
        public int idActualizacion { get; set; }
        public decimal? precLista { get; set; }
        public decimal? precMinimo { get; set; }
        public decimal? precMayoreo { get; set; }
        public decimal? precFilial { get; set; }
        public decimal? precIMSS { get; set; }
        public decimal? medioMayoreo { get; set; }
    }
}
