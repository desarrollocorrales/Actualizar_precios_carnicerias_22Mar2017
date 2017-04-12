using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FirebirdSql.Data.FirebirdClient;
using DescargaPreciosCarn.Modelos;

namespace DescargaPreciosCarn.Datos
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

        // obtiene si el articulo tiene como estatus ACTIVO o no
        public Modelos.ArticulosFB buscaActivo(string clave)
        {
            Modelos.ArticulosFB result = new ArticulosFB();

            string sql =
                "select a.articulo_id, a.nombre, ca.clave_articulo, a.estatus " +
                "from articulos a " +
                "left join claves_articulos ca on (a.articulo_id = ca.articulo_id) " +
                "where ca.clave_articulo = @cveArt and ca.rol_clave_art_id = 17";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionFB.getConexionFB())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new FbCommand())
                {
                    cmd.Connection = conn;

                    // parametros
                    cmd.Parameters.AddWithValue("@cveArt", clave);

                    ManejoSql_FB res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        if (res.reader.HasRows)
                            while (res.reader.Read())
                            {
                                result = new ArticulosFB();
                                result.articulo = Convert.ToString(res.reader["nombre"]);
                                result.articuloId = Convert.ToInt64(res.reader["articulo_id"]);
                                result.cveArticulo = Convert.ToString(res.reader["clave_articulo"]);
                                result.estatus = Convert.ToString(res.reader["estatus"]);
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

        // busca si el precio existe segun el articulo y el precio_empresa_id
        public Modelos.PrecioArticulo buscaPrecExistencia(string clave, long precioEmpresaId)
        {
            Modelos.PrecioArticulo result = null;

            string sql =
                "select pa.precio_articulo_id, pa.articulo_id, pa.precio_empresa_id, pa.precio, pa.moneda_id, pa.margen, pa.fecha_hora_ult_modif " +
                "from articulos a " +
                "left join precios_articulos pa on (a.articulo_id = pa.articulo_id) " +
                "left join claves_articulos ca on (a.articulo_id = ca.articulo_id) " +
                "where ca.clave_articulo = @cveArt and pa.precio_empresa_id = @precEmId";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionFB.getConexionFB())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new FbCommand())
                {
                    cmd.Connection = conn;

                    // parametros
                    cmd.Parameters.AddWithValue("@cveArt", clave);
                    cmd.Parameters.AddWithValue("@precEmId", precioEmpresaId);

                    ManejoSql_FB res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        while (res.reader.Read())
                        {
                            result = new PrecioArticulo();
                            result.precioArticuloId = Convert.ToInt64(res.reader["precio_articulo_id"]);
                            result.articuloId = Convert.ToInt64(res.reader["articulo_id"]);
                            result.precioEmpresaId = Convert.ToInt64(res.reader["precio_empresa_id"]);
                            result.precio = Convert.ToDecimal(res.reader["precio"]);
                            result.monedaId = Convert.ToInt16(res.reader["moneda_id"]);
                            result.margen = Convert.ToDecimal(res.reader["margen"]);
                            result.fechaHoraUltModif = Convert.ToString(res.reader["fecha_hora_ult_modif"]);
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

        // actualiza el precio segun el precio empresa
        public bool actPrecio(long precioArticuloId, decimal? precio)
        {
            bool result = false;
            int rows = 0;

            // define conexion con la cadena de conexion
            using (var conn = this._conexionFB.getConexionFB())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new FbCommand())
                {
                    cmd.Connection = conn;

                    string sql =
                        "update precios_articulos set precio = @precio, fecha_hora_ult_modif = current_timestamp " +
                        "where precio_articulo_id = @precArtId ";

                    cmd.Parameters.AddWithValue("@precio", precio ?? 0);
                    cmd.Parameters.AddWithValue("@precArtId", precioArticuloId);

                    ManejoSql_FB res = Utilerias.EjecutaSQL(sql, ref rows, cmd);

                    if (!res.ok) throw new Exception(res.numErr + ": " + res.descErr);
                }
            }

            return result;
        }

        // insertar nuevo precio para el articulo
        public bool insertPrecio(long articuloId, long precioEmpresaId, decimal? precLista)
        {
            bool result = false;
            int rows = 0;

            // define conexion con la cadena de conexion
            using (var conn = this._conexionFB.getConexionFB())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new FbCommand())
                {
                    cmd.Connection = conn;

                    string sql =
                        "insert into precios_articulos (precio_articulo_id, articulo_id, precio_empresa_id, precio, moneda_id, margen) " +
                        "values (-1, @artId, @precEmpId, @precio, 1, 0)";

                    cmd.Parameters.AddWithValue("@artId", articuloId);
                    cmd.Parameters.AddWithValue("@precEmpId", precioEmpresaId);
                    cmd.Parameters.AddWithValue("@precio", precLista);

                    ManejoSql_FB res = Utilerias.EjecutaSQL(sql, ref rows, cmd);

                    if (!res.ok) throw new Exception(res.numErr + ": " + res.descErr);
                }
            }

            return result;
        }

        // obtiene la fecha de firebird, es la hora exacta mas cercana posible
        // ya que se encuentra en un servidor en linea
        public string getFecha()
        {
            string result = Convert.ToString(DateTime.Now);

            string sql = "select current_timestamp from rdb$database";

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
                        while (res.reader.Read()) result = Convert.ToString(res.reader["current_timestamp"]).Trim();
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();

                }
            }

            DateTime dt = DateTime.Parse(result);

            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
