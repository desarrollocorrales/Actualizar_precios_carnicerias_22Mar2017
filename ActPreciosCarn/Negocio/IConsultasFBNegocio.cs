using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ActPreciosCarn.Negocio
{
    public interface IConsultasFBNegocio
    {
        bool pruebaConn();

        List<Modelos.Articulos> obtieneArticulos(Dictionary<string, long> precEmpr);

        Dictionary<string, long> getPreciosEmpresas();

        string getFecha();
    }
}
