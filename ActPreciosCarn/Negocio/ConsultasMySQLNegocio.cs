using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActPreciosCarn.Datos;

namespace ActPreciosCarn.Negocio
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


        public void generaBitacora(string detalle)
        {
            this._consultasMySQLDatos.generaBitacora(detalle);
        }


        public int guardaActualizacion(List<Modelos.Articulos> seleccionados)
        {
            // obtener el proximo bloque
            int bloque = this._consultasMySQLDatos.getSigBloque();

            // guarda actualizacion
            this._consultasMySQLDatos.guardaActualizacion(seleccionados, bloque + 1);

            return bloque + 1;
        }
    }
}
