using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DescargaPreciosCarn.Datos;

namespace DescargaPreciosCarn.Negocio
{
    public class ConsultasMySQLNegocio : IConsultasMySQLNegocio
    {
        private IConsultasMySQLDatos _consultasMySQLDatos;

        public ConsultasMySQLNegocio()
        {
            this._consultasMySQLDatos = new ConsultasMySQLDatos();
        }

        public bool pruebaConn()
        {
            return this._consultasMySQLDatos.pruebaConn();
        }

        public Modelos.Response validaAcceso(string usuario, string pass)
        {
            Modelos.Response result = new Modelos.Response();
            result.status = Modelos.Estatus.OK;

            // buscar si el usuario ya existe
            bool us = this._consultasMySQLDatos.buscaUsuario(usuario.Trim().ToLower());

            if (!us)
            {
                result.status = Modelos.Estatus.ERROR;
                result.error = "El usuario no existe";
                return result;
            }

            Modelos.Usuarios resultado = this._consultasMySQLDatos.validaAcceso(usuario, pass);

            if (resultado == null)
            {
                result.status = Modelos.Estatus.ERROR;
                result.error = "Contraseña incorrecta";
                return result;
            }

            result.usuario = resultado;

            return result;
        }

        public long generaBitacora(string detalle, string fecha)
        {
            return this._consultasMySQLDatos.generaBitacora(detalle, fecha);
        }

        public bool verifDescargas(string sucursal)
        {
            return this._consultasMySQLDatos.verifDescargas(sucursal);
        }

        public List<Modelos.Articulos> getArticulosActualizar(string sucursal)
        {
            // obtiene el bloque a descargar, es el mas antiguo que se tenga pendiente
            int bloque = this._consultasMySQLDatos.obtBloqueAnt(sucursal);

            return this._consultasMySQLDatos.getArticulosActualizar(sucursal, bloque);
        }

        public bool liberaArticulo(string claveArt, int bloque, string sucursal)
        {
            return this._consultasMySQLDatos.liberaArticulo(claveArt, bloque, sucursal);
        }

        public bool liberaBloque(int bloque, string sucursal)
        {
            bool result = true;

            // busca si todos los articulos del bloque han sido liberados
            bool bloLib = this._consultasMySQLDatos.bloquesLiberados(bloque, sucursal);

            // libera el bloque completo
            /*
            if (bloLib)
                this._consultasMySQLDatos.liberaBloque(bloque);
            */
            return result;
        }

        public bool liberarBloques(int bloque)
        {
            bool result = true;

            // busca si todos los articulos del bloque han sido liberados en todas las sucursales
            result = this._consultasMySQLDatos.bloquesLiberados(bloque);

            // libera el bloque completo en todas las sucursales
            if (result)
                this._consultasMySQLDatos.liberaBloque(bloque);
            
            return result;
        }
    }
}
