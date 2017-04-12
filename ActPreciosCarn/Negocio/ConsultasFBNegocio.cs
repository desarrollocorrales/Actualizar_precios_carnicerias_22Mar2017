using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActPreciosCarn.Datos;

namespace ActPreciosCarn.Negocio
{
    public class ConsultasFBNegocio : IConsultasFBNegocio
    {
        private IConsultasFBDatos _consultasFBDatos;

        public ConsultasFBNegocio()
        {
            this._consultasFBDatos = new ConsultasFBDatos();
        }

        public bool pruebaConn()
        {
            return this._consultasFBDatos.pruebaConn();
        }


        public List<Modelos.Articulos> obtieneArticulos(Dictionary<string, long> precEmpr)
        {
            return this._consultasFBDatos.obtieneArticulos(precEmpr);
        }


        public Dictionary<string, long> getPreciosEmpresas()
        {
            return this._consultasFBDatos.getPreciosEmpresas();
        }


        public string getFecha()
        {
            return this._consultasFBDatos.getFecha();
        }
    }
}
