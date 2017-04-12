using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DescargaPreciosCarn.Datos;

namespace DescargaPreciosCarn.Negocio
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

        public Dictionary<string, long> getPreciosEmpresas()
        {
            return this._consultasFBDatos.getPreciosEmpresas();
        }

        public Modelos.ArticulosFB buscaActivo(string clave)
        {
            return this._consultasFBDatos.buscaActivo(clave);
        }

        public Modelos.PrecioArticulo buscaPrecExistencia(string clave, long precioEmpresaId)
        {
            return this._consultasFBDatos.buscaPrecExistencia(clave, precioEmpresaId);
        }

        public bool actPrecio(long precioArticuloId, decimal? precio)
        {
            return this._consultasFBDatos.actPrecio(precioArticuloId, precio);
        }

        public bool insertPrecio(long articuloId, long precioEmpresaId, decimal? precLista)
        {
            return this._consultasFBDatos.insertPrecio(articuloId, precioEmpresaId, precLista);
        }

        public string getFecha()
        {
            return this._consultasFBDatos.getFecha();
        }
    }
}
