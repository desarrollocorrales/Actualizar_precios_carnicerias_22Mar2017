using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DescargaPreciosCarn.Modelos;
using MySql.Data.MySqlClient;

namespace DescargaPreciosCarn.Datos
{
    public class ConsultasMySQLDatos : IConsultasMySQLDatos
    {
        // Variable que almacena el estado de la conexión a la base de datos
        IConexionMySQL _conexionMySQL;

        public ConsultasMySQLDatos()
        {
            this._conexionMySQL = new ConexionMySQL(Modelos.ConectionString.connMySQL);
        }

        // realiza una prueba de conexion
        public bool pruebaConn()
        {
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (MySqlException)
                {
                    return false;
                }
            }
        }

        // busca si el usuario no existe
        public bool buscaUsuario(string usuario)
        {
            bool result = false;

            string sql = "select count(*) from usuarios where trim(usuario) = @usuario and status = 'A'";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    // define parametros
                    cmd.Parameters.AddWithValue("@usuario", usuario);

                    ManejoSql_My res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        while (res.reader.Read())
                        {
                            int count = Convert.ToInt16(res.reader[0]);

                            if (count > 0) result = true;
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

        // valida las credenciales del usuario
        public Usuarios validaAcceso(string usuario, string pass)
        {
            Usuarios result = null;

            string sql =
                        "select u.id_usuario, u.nombre_completo, u.correo, u.fecha_creacion, u.usuario, u.status " +
                        "from usuarios u " +
                        "where usuario = @usuario and clave = @clave and u.status = 'A'";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    // define parametros
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@clave", Utilerias.Base64Encode(pass));

                    ManejoSql_My res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        if (res.reader.HasRows)
                            while (res.reader.Read())
                            {
                                result = new Usuarios();

                                result.idUsuario = Convert.ToInt16(res.reader["id_usuario"]);
                                result.nombreCompleto = Convert.ToString(res.reader["nombre_completo"]);
                                result.correo = Convert.ToString(res.reader["correo"]);

                                result.fechaCreacion = Convert.ToString(res.reader["fecha_creacion"]);
                                result.usuario = Convert.ToString(res.reader["usuario"]);
                                result.status = Convert.ToString(res.reader["status"]);

                            }
                        else result = null;
                    }
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();

                }
            }

            return result;
        }

        // genera bitacora
        public long generaBitacora(string detalle)
        {
            int rows = 0;
            long result = 0;

            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    string bitacora =
                        "insert into bitacora (id_usuario, fecha, detalle, host) " +
                        "values (@idusu, now(), @detalle, (SELECT SUBSTRING_INDEX(HOST, ':', 1) AS 'ip' FROM information_schema.PROCESSLIST WHERE ID = connection_id()))";

                    cmd.Parameters.AddWithValue("@idusu", Modelos.Login.idUsuario);
                    cmd.Parameters.AddWithValue("@detalle", detalle);

                    ManejoSql_My res = Utilerias.EjecutaSQL(bitacora, ref rows, cmd);

                    if (!res.ok) throw new Exception(res.numErr + ": " + res.descErr);
                    else result = cmd.LastInsertedId;
                }
            }

            return result;
        }

        // genera bitacora
        public bool verifDescargas(string sucursal)
        {
            bool result = false;

            string sql = string.Format(
                "select count(*) pendientes from actualizacion where {0} = 'P'",
                sucursal);

            // define conexion con la cadena de conexion
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    ManejoSql_My res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        if (res.reader.HasRows)
                            while (res.reader.Read())
                            {
                                int count = Convert.ToInt16(res.reader["pendientes"]);
                                
                                if(count > 0) result = true;
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

        // obtiene los articulos a actualizar
        public List<Articulos> getArticulosActualizar(string sucursal, int bloque)
        {
            List<Articulos> result = new List<Articulos>();
            Articulos ent = null;

            string sql = string.Format(
                        "select a.num_bloque, a.clave_articulo, ar.nombre, " +
                            "ad.prec_lista, ad.prec_minimo, ad.prec_mayoreo, " +
                            "ad.prec_filial, ad.prec_imss, ad.medio_mayoreo " +
                        "from actualizacion a " +
                        "left join actualizacion_det ad on (a.id_actualizacion = ad.id_actualizacion) " +
                        "left join articulos ar on (a.clave_articulo = ar.clave) " +
                        "where a.{0} = 'P' and a.num_bloque = @bloque", sucursal);

            // define conexion con la cadena de conexion
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    // define parametros
                    cmd.Parameters.AddWithValue("@bloque", bloque);

                    ManejoSql_My res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        if (res.reader.HasRows)
                            while (res.reader.Read())
                            {
                                ent = new Articulos();

                                ent.bloque = Convert.ToInt16(res.reader["num_bloque"]);
                                ent.articulo = Convert.ToString(res.reader["nombre"]);
                                ent.clave = Convert.ToString(res.reader["clave_articulo"]);

                                // lista
                                if (res.reader["prec_lista"] is DBNull) ent.precLista = null;
                                else
                                {
                                    ent.precLista = Convert.ToDecimal(Convert.ToString(res.reader["prec_lista"]).TrimEnd(new Char[] { '0' }));
                                    //ent.precLista = Convert.ToDecimal(res.reader["prec_lista"]);
                                }

                                // minimo
                                if (res.reader["prec_minimo"] is DBNull) ent.precMinimo = null;
                                else
                                {
                                    ent.precMinimo = Convert.ToDecimal(Convert.ToString(res.reader["prec_minimo"]).TrimEnd(new Char[] { '0' }));
                                    //ent.precMinimo = Convert.ToDecimal(res.reader["prec_minimo"]);
                                }

                                // mayoreo
                                if (res.reader["prec_mayoreo"] is DBNull) ent.precMayoreo = null;
                                else
                                {
                                    ent.precMayoreo = Convert.ToDecimal(Convert.ToString(res.reader["prec_mayoreo"]).TrimEnd(new Char[] { '0' }));
                                    //ent.precMayoreo = Convert.ToDecimal(res.reader["prec_mayoreo"]);
                                }

                                // filial
                                if (res.reader["prec_filial"] is DBNull) ent.precFilial = null;
                                else
                                {
                                    ent.precFilial = Convert.ToDecimal(Convert.ToString(res.reader["prec_filial"]).TrimEnd(new Char[] { '0' }));
                                    //ent.precFilial = Convert.ToDecimal(res.reader["prec_filial"]);
                                }

                                // imss
                                if (res.reader["prec_imss"] is DBNull) ent.precIMSS = null;
                                else
                                {
                                    ent.precIMSS = Convert.ToDecimal(Convert.ToString(res.reader["prec_imss"]).TrimEnd(new Char[] { '0' }));
                                    //ent.precIMSS = Convert.ToDecimal(res.reader["prec_imss"]);
                                }

                                // medio mayoreo
                                if (res.reader["medio_mayoreo"] is DBNull) ent.medioMayoreo = null;
                                else
                                {
                                    ent.medioMayoreo = Convert.ToDecimal(Convert.ToString(res.reader["medio_mayoreo"]).TrimEnd(new Char[] { '0' }));
                                    //ent.medioMayoreo = Convert.ToDecimal(res.reader["medio_mayoreo"]);
                                }

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

        // obtiene el bloque mas antiguo que se tenga
        public int obtBloqueAnt(string sucursal)
        {
            int result = 0;

            string sql = string.Format(
                "select min(num_bloque) num_bloque from actualizacion where {0} = 'P'",
                sucursal);

            // define conexion con la cadena de conexion
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    ManejoSql_My res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                        if (res.reader.HasRows)
                            while (res.reader.Read())
                                result = Convert.ToInt16(res.reader["num_bloque"]);
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();

                }
            }

            return result;
        }

        // libera el articulo como pendiente de descarga
        public bool liberaArticulo(string claveArt, int bloque, string sucursal)
        {
            bool result = false;
            int rows = 0;

            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    string bitacora = string.Format(
                        "update actualizacion set {0} = 'R' " +
                        "where num_bloque = @bloque and clave_articulo = @cveArt", sucursal);

                    cmd.Parameters.AddWithValue("@cveArt", claveArt);
                    cmd.Parameters.AddWithValue("@bloque", bloque);

                    ManejoSql_My res = Utilerias.EjecutaSQL(bitacora, ref rows, cmd);

                    if (!res.ok) throw new Exception(res.numErr + ": " + res.descErr);
                    else result = true;
                }
            }

            return result;
        }

        // busca si el bloque esta liberado totalmente
        public bool bloquesLiberados(int bloque, string sucursal)
        {
            bool result = true;

            string sql = string.Format(
                "select count(*) as count from actualizacion where num_bloque = @bloque and {0} = 'P'",
                sucursal);

            // define conexion con la cadena de conexion
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@bloque", bloque);

                    ManejoSql_My res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        if (res.reader.HasRows)
                            while (res.reader.Read())
                            {
                                int count = Convert.ToInt16(res.reader["count"]);

                                if (count > 0) result = false;
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

        // libera un bloque completo
        public void liberaBloque(int bloque)
        {
            int rows = 0;

            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    string bitacora =
                        "update actualizacion set status = 'R' " +
                        "where num_bloque = @bloque";

                    cmd.Parameters.AddWithValue("@bloque", bloque);

                    ManejoSql_My res = Utilerias.EjecutaSQL(bitacora, ref rows, cmd);

                    if (!res.ok) throw new Exception(res.numErr + ": " + res.descErr);
                }
            }
        }

        // verifica si un bloque ha sido descargado en todas las sucursales
        public bool bloquesLiberados(int bloque)
        {
            bool result = true;

            string sql =
                "select count(*) as count from actualizacion where num_bloque = @bloque and (fidel = 'P' or libertad = 'P' or heroico = 'P')";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@bloque", bloque);

                    ManejoSql_My res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        if (res.reader.HasRows)
                            while (res.reader.Read())
                            {
                                int count = Convert.ToInt16(res.reader["count"]);

                                if (count > 0) result = false;
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
