﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActPreciosCarn.Negocio
{
    public interface IConsultasMySQLNegocio
    {
        bool pruebaConn();

        Modelos.Response validaAcceso(string usuario, string pass);

        long generaBitacora(string detalle);

        int guardaActualizacion(List<Modelos.Articulos> seleccionados);

        Modelos.Response creaUsuario(string nombreCompleto, string correo, string usuario, string clave);

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
