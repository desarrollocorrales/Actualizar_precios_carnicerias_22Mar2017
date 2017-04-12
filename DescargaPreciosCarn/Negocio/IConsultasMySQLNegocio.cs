using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DescargaPreciosCarn.Negocio
{
    public interface IConsultasMySQLNegocio
    {
        bool pruebaConn();

        Modelos.Response validaAcceso(string usuario, string pass);

        long generaBitacora(string detalle, string fecha);

        bool verifDescargas(string sucursal);

        List<Modelos.Articulos> getArticulosActualizar(string sucursal);

        bool liberaArticulo(string claveArt, int bloque, string sucursal);

        bool liberaBloque(int bloque, string sucursal);

        bool liberarBloques(int bloque);
    }
}
