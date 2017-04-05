using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace DescargaPreciosCarn.Modelos
{
    public interface IConexionFB
    {
        FbConnection getConexionFB();
    }
}
