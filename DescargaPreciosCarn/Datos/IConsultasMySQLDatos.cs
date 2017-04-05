using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DescargaPreciosCarn.Datos
{
    public interface IConsultasMySQLDatos
    {
        bool pruebaConn();

        bool buscaUsuario(string usuario);

        Modelos.Usuarios validaAcceso(string usuario, string pass);

        long generaBitacora(string detalle);

        bool verifDescargas(string sucursal);

        List<Modelos.Articulos> getArticulosActualizar(string sucursal, int bloque);

        int obtBloqueAnt(string sucursal);

        bool liberaArticulo(string claveArt, int bloque, string sucursal);

        bool bloquesLiberados(int bloque, string sucursal);

        void liberaBloque(int bloque);

        bool bloquesLiberados(int bloque);
    }
}
