using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DescargaPreciosCarn.Datos
{
    public interface IConsultasFBDatos
    {
        bool pruebaConn();

        Dictionary<string, long> getPreciosEmpresas();

        Modelos.ArticulosFB buscaActivo(string clave);

        Modelos.PrecioArticulo buscaPrecExistencia(string clave, long precioEmpresaId);

        bool actPrecio(long precioArticuloId, decimal? precio);

        bool insertPrecio(long articuloId, long precioEmpresaId, decimal? precLista);
    }
}
