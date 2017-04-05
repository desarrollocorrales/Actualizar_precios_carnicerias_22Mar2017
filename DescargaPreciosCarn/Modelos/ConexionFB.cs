using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;

namespace DescargaPreciosCarn.Modelos
{
    public class ConexionFB : IConexionFB
    {
        private string _cadenaConexionFB;

        public ConexionFB(string cadenaConexionFB)
        {
            this._cadenaConexionFB = cadenaConexionFB;
        }

        public string getCadenaFB()
        {
            return this._cadenaConexionFB;
        }

        public FbConnection getConexionFB()
        {
            return new FbConnection(this._cadenaConexionFB);
        }
    }
}
