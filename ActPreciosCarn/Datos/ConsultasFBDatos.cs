using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using ActPreciosCarn.Modelos;

namespace ActPreciosCarn.Datos
{
    public class ConsultasFBDatos : IConsultasFBDatos
    {
        // Variable que almacena el estado de la conexión a la base de datos
        IConexionFB _conexionFB;

        public ConsultasFBDatos()
        {
            this._conexionFB = new ConexionFB(Modelos.ConectionString.connFB);
        }

        // realiza una prueba de conexion a la base de datos de FIREBIRD
        public bool pruebaConn()
        {
            using (var conn = this._conexionFB.getConexionFB())
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (FbException)
                {
                    return false;
                }
            }
        }

        // obtiene todos los articulos de microsip (FIREBIRD)
        public List<Articulos> obtieneArticulos(Dictionary<string, long> precEmpr)
        {
            List<Modelos.Articulos> result = new List<Modelos.Articulos>();
            Modelos.Articulos ent;

            string sql =
                "SELECT CA.CLAVE_ARTICULO, A.ARTICULO_ID, A.NOMBRE,  " +
                        "LISTA.PRECIO AS LISTA, " +
                        "MINIMO.PRECIO AS MINIMO, " +
                        "MAYOREO.PRECIO AS MAYOREO, " +
                        "FILIAL.PRECIO AS FILIAL, " +
                        "IMSS.PRECIO AS IMSS, " +
                        "MEDIO_MAYO.PRECIO AS MEDIO_MAYO " +

                "FROM CLAVES_ARTICULOS CA " +

                "LEFT JOIN ARTICULOS A ON (CA.ARTICULO_ID = A.ARTICULO_ID) " +

                "LEFT JOIN " +
                   "(SELECT ARTICULO_ID, PRECIO FROM PRECIOS_ARTICULOS WHERE PRECIO_EMPRESA_ID = @idLista) LISTA " +
                   "ON (CA.ARTICULO_ID = LISTA.ARTICULO_ID) " +

                "LEFT JOIN " +
                   "(SELECT ARTICULO_ID, PRECIO FROM PRECIOS_ARTICULOS WHERE PRECIO_EMPRESA_ID = @idMinimo) MINIMO " +
                   "ON (CA.ARTICULO_ID = MINIMO.ARTICULO_ID) " +

                "LEFT JOIN " +
                   "(SELECT ARTICULO_ID, PRECIO FROM PRECIOS_ARTICULOS WHERE PRECIO_EMPRESA_ID = @idMayoreo) MAYOREO " +
                   "ON (CA.ARTICULO_ID = MAYOREO.ARTICULO_ID) " +

                "LEFT JOIN " +
                   "(SELECT ARTICULO_ID, PRECIO FROM PRECIOS_ARTICULOS WHERE PRECIO_EMPRESA_ID = @idFilial) FILIAL " +
                   "ON (CA.ARTICULO_ID = FILIAL.ARTICULO_ID) " +

                "LEFT JOIN " +
                   "(SELECT ARTICULO_ID, PRECIO FROM PRECIOS_ARTICULOS WHERE PRECIO_EMPRESA_ID = @idImss) IMSS " +
                   "ON (CA.ARTICULO_ID = IMSS.ARTICULO_ID) " +

                "LEFT JOIN " +
                   "(SELECT ARTICULO_ID, PRECIO FROM PRECIOS_ARTICULOS WHERE PRECIO_EMPRESA_ID = @idMedioMayoreo) MEDIO_MAYO " +
                   "ON (CA.ARTICULO_ID = MEDIO_MAYO.ARTICULO_ID) " +

                "WHERE A.ESTATUS = 'A' AND CA.ROL_CLAVE_ART_ID = 17 " +

                "ORDER BY A.NOMBRE";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionFB.getConexionFB())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new FbCommand())
                {
                    cmd.Connection = conn;
                    
                    // parametros
                    cmd.Parameters.AddWithValue("@idLista", precEmpr["precio de lista"]);
                    cmd.Parameters.AddWithValue("@idMinimo", precEmpr["precio mínimo"]);
                    cmd.Parameters.AddWithValue("@idMayoreo", precEmpr["prec.mayoreo"]);
                    cmd.Parameters.AddWithValue("@idFilial", precEmpr["prec.filial"]);
                    cmd.Parameters.AddWithValue("@idImss", precEmpr["prec.i.m.s.s."]);
                    cmd.Parameters.AddWithValue("@idMedioMayoreo", precEmpr["medio mayoreo"]);
                    
                    ManejoSql_FB res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        if (res.reader.HasRows)
                            while (res.reader.Read())
                            {
                                ent = new Modelos.Articulos();

                                ent.idArticulo = Convert.ToInt64(res.reader["ARTICULO_ID"]);
                                ent.clave = Convert.ToString(res.reader["CLAVE_ARTICULO"]);
                                ent.articulo = Convert.ToString(res.reader["NOMBRE"]);

                                // lista
                                if (res.reader["LISTA"] is DBNull) ent.precLista = null;
                                else ent.precLista = Convert.ToDecimal(res.reader["LISTA"]);

                                // minimo
                                if (res.reader["MINIMO"] is DBNull) ent.precMinimo = null;
                                else ent.precMinimo = Convert.ToDecimal(res.reader["MINIMO"]);

                                // mayoreo
                                if (res.reader["MAYOREO"] is DBNull) ent.precMayoreo = null;
                                else ent.precMayoreo = Convert.ToDecimal(res.reader["MAYOREO"]);

                                // filial
                                if (res.reader["FILIAL"] is DBNull) ent.precFilial = null;
                                else ent.precFilial = Convert.ToDecimal(res.reader["FILIAL"]);

                                // imss
                                if (res.reader["IMSS"] is DBNull) ent.precIMSS = null;
                                else ent.precIMSS = Convert.ToDecimal(res.reader["IMSS"]);

                                // medio mayoreo
                                if (res.reader["MEDIO_MAYO"] is DBNull) ent.medioMayoreo = null;
                                else ent.medioMayoreo = Convert.ToDecimal(res.reader["MEDIO_MAYO"]);

                                result.Add(ent);
                            }
                    }
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();

                }
            }

            return result;
        }

        // obtiene precios empresas
        public Dictionary<string, long> getPreciosEmpresas()
        {
            Dictionary<string, long> result = new Dictionary<string, long>();

            string sql = "select precio_empresa_id, lower(nombre) as nombre from precios_empresa";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionFB.getConexionFB())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new FbCommand())
                {
                    cmd.Connection = conn;

                    ManejoSql_FB res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        if (res.reader.HasRows)
                            while (res.reader.Read())
                            {
                                result.Add(
                                    Convert.ToString(res.reader["nombre"]).Trim(), 
                                    Convert.ToInt64(res.reader["precio_empresa_id"])
                                    );
                            }
                    }
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();

                }
            }

            return result;
        }
    }
}
