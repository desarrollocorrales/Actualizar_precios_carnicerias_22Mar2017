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

        public long generaBitacora(string detalle)
        {
            return this._consultasMySQLDatos.generaBitacora(detalle);
        }

        public int guardaActualizacion(List<Modelos.Articulos> seleccionados)
        {
            // obtener el proximo bloque
            int bloque = this._consultasMySQLDatos.getSigBloque();

            // guarda actualizacion
            this._consultasMySQLDatos.guardaActualizacion(seleccionados, bloque + 1);

            return bloque + 1;
        }

        public Modelos.Response creaUsuario(string nombreCompleto, string correo, string usuario, string clave)
        {
            Modelos.Response result = new Modelos.Response();

            // buscar si el usuario ya existe
            bool us = this._consultasMySQLDatos.buscaUsuario(usuario.Trim().ToLower());

            if (us)
            {
                result.status = Modelos.Estatus.ERROR;
                result.error = "El usuario ya existe";
                return result;
            }

            // buscar si el correo existe
            bool corr = this._consultasMySQLDatos.buscaCorreo(correo.Trim().ToLower());

            if (corr)
            {
                result.status = Modelos.Estatus.ERROR;
                result.error = "El correo ya se registro para otro usuario";
                return result;
            }

            // inserta el usuario
            bool inserta = this._consultasMySQLDatos.insertaUsuario(nombreCompleto, correo, usuario, clave);

            result.status = Modelos.Estatus.OK;

            return result;
        }

        public bool validaClave(string claveActual, int _idUsuario)
        {
            return this._consultasMySQLDatos.validaClave(claveActual, _idUsuario);
        }

        public bool actualizaClave(string clave, int idUsuario, string usuario)
        {
            return this._consultasMySQLDatos.actualizaClave(clave, idUsuario, usuario);
        }

        public List<Modelos.Actualizacion> obtieneInformacion(string fechaIni, string fechaFin, int bloque, bool pendiente, bool realizado)
        {
            return this._consultasMySQLDatos.obtieneInformacion(fechaIni, fechaFin, bloque, pendiente, realizado);
        }

        public List<int> obtieneBloques(string fechaIni, string fechaFin)
        {
            return this._consultasMySQLDatos.obtieneBloques(fechaIni, fechaFin);
        }

        public Modelos.ActualizacionDet obtieneDetalle(int idActualizacion)
        {
            return this._consultasMySQLDatos.obtieneDetalle(idActualizacion);
        }

        public void insertaArticulos(List<Modelos.Articulos> articulos)
        {
            this._consultasMySQLDatos.insertaArticulos(articulos);
        }

        public List<Modelos.Articulos> obtieneArticulos()
        {
            return this._consultasMySQLDatos.obtieneArticulos();
        }

        public void guardaBitacora(List<Modelos.Articulos> anteriores, List<Modelos.Articulos> seleccionados, long resultado)
        {
            this._consultasMySQLDatos.guardaBitacora(anteriores, seleccionados, resultado);
        }
    }
}
