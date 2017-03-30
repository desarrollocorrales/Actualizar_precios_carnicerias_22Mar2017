using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using MySql.Data.MySqlClient;

namespace ActPreciosCarn.Modelos
{
    public interface IConexionFB
    {
        FbConnection getConexionFB();
    }
}
