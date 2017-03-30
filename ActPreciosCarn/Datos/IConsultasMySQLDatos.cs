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

        void generaBitacora(string detalle);

        int getSigBloque();

        bool guardaActualizacion(List<Modelos.Articulos> seleccionados, int bloque);
    }
}
