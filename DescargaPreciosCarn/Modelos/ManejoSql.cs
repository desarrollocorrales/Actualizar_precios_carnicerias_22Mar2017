using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using FirebirdSql.Data.FirebirdClient;

namespace DescargaPreciosCarn.Modelos
{
    public class ManejoSql_My
    {
        public bool ok { get; set; }
        public MySqlDataReader reader { get; set; }
        public int numErr { get; set; }
        public string descErr { get; set; }
    }

    public class ManejoSql_FB
    {
        public bool ok { get; set; }
        public FbDataReader reader { get; set; }
        public int numErr { get; set; }
        public string descErr { get; set; }
    }
}
