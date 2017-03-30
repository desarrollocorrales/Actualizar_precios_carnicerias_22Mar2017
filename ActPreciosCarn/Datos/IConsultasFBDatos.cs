using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActPreciosCarn.Datos
{
    public interface IConsultasFBDatos
    {
        bool pruebaConn();

        List<Modelos.Articulos> obtieneArticulos(Dictionary<string, long> precEmpr);

        Dictionary<string, long> getPreciosEmpresas();
    }
}
