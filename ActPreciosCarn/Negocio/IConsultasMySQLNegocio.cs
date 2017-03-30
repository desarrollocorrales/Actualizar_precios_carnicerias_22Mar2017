using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActPreciosCarn.Negocio
{
    public interface IConsultasMySQLNegocio
    {
        bool pruebaConn();

        Modelos.Response validaAcceso(string usuario, string pass);

        void generaBitacora(string detalle);

        int guardaActualizacion(List<Modelos.Articulos> seleccionados);
    }
}
