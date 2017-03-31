using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActPreciosCarn.Datos
{
    public interface IConsultasMySQLDatos
    {
        bool pruebaConn();

        bool buscaUsuario(string usuario);

        Modelos.Usuarios validaAcceso(string usuario, string pass);

        long generaBitacora(string detalle);

        int getSigBloque();

        bool guardaActualizacion(List<Modelos.Articulos> seleccionados, int bloque);

        bool buscaCorreo(string correo);

        bool insertaUsuario(string nombreCompleto, string correo, string usuario, string clave);

        bool validaClave(string claveActual, int _idUsuario);

        bool actualizaClave(string clave, int idUsuario, string usuario);

        List<Modelos.Actualizacion> obtieneInformacion(string fechaIni, string fechaFin, int bloque, bool pendiente, bool realizado);

        List<int> obtieneBloques(string fechaIni, string fechaFin);

        Modelos.ActualizacionDet obtieneDetalle(int idActualizacion);

        void insertaArticulos(List<Modelos.Articulos> articulos);

        List<Modelos.Articulos> obtieneArticulos();

        void guardaBitacora(List<Modelos.Articulos> anteriores, List<Modelos.Articulos> seleccionados, long resultado);
    }
}
