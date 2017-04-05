using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActPreciosCarn.Modelos;
using MySql.Data.MySqlClient;

namespace ActPreciosCarn.Datos
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

            // string sql = "select idusuario, usuario from activos_usuarios where usuario = @usuario and clave = @clave";

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

        // obtiene siguiente bloque
        public int getSigBloque()
        {
            int result = 0;

            string sql = "select ifnull(max(num_bloque), 0) bloque from actualizacion";

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
                                result = Convert.ToInt32(res.reader["bloque"]);

                            }
                        else result = 0;
                    }
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();

                }
            }

            return result;
        }

        // guarda actualizacion
        public bool guardaActualizacion(List<Articulos> seleccionados, int bloque)
        {
            MySqlTransaction trans;

            bool result = true;

            string sql = string.Empty;

            long idInserted = 0;
            int rows = 0;

            string error = string.Empty;


            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                conn.Open();
                using (var cmd = new MySqlCommand())
                {
                    trans = conn.BeginTransaction();

                    try
                    {
                        cmd.Connection = conn;
                        cmd.Transaction = trans;

                        cmd.Parameters.Clear();

                        foreach (Modelos.Articulos art in seleccionados)
                        {
                            // inserta actuaizacion
                            sql =
                                "INSERT INTO actualizacion (num_bloque, fecha, clave_articulo, fidel, heroico, libertad, status) " +
                                "VALUES (@numBloque, now(), @claveArticulo, @fidel, @heroico, @libertad, @status)";

                            // define parametros
                            cmd.Parameters.AddWithValue("@numBloque", bloque);
                            cmd.Parameters.AddWithValue("@claveArticulo", art.clave);
                            cmd.Parameters.AddWithValue("@fidel", "P");
                            cmd.Parameters.AddWithValue("@heroico", "P");
                            cmd.Parameters.AddWithValue("@libertad", "P");
                            cmd.Parameters.AddWithValue("@status", "P");

                            ManejoSql_My res = Utilerias.EjecutaSQL(sql, ref rows, cmd);

                            if (res.ok)
                            {
                                if (rows != 0) idInserted = cmd.LastInsertedId;
                            }
                            else 
                                throw new Exception(res.numErr + ": " + res.descErr);

                            cmd.Parameters.Clear();

                            // inserta actualizacion_det
                            string sqlDet =
                                "INSERT INTO actualizacion_det (id_actualizacion, prec_lista, prec_minimo, prec_mayoreo, prec_filial, prec_imss, medio_mayoreo) " +
                                "VALUES (@idAct, @precLista, @precMinimo, @precMayo, @precFilial, @precImss, @medioMayo)";

                            // define parametros
                            cmd.Parameters.AddWithValue("@idAct", idInserted);
                            cmd.Parameters.AddWithValue("@precLista", art.precLista);
                            cmd.Parameters.AddWithValue("@precMinimo", art.precMinimo);
                            cmd.Parameters.AddWithValue("@precMayo", art.precMayoreo);
                            cmd.Parameters.AddWithValue("@precFilial", art.precFilial);
                            cmd.Parameters.AddWithValue("@precImss", art.precIMSS);
                            cmd.Parameters.AddWithValue("@medioMayo", art.medioMayoreo);

                            res = Utilerias.EjecutaSQL(sqlDet, ref rows, cmd);

                            if (!res.ok) throw new Exception(res.numErr + ": " + res.descErr);

                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        throw new Exception(e.Message);
                    }
                }
            }

            return result;
        }

        // busca un correo en usuarios
        public bool buscaCorreo(string correo)
        {
            bool result = false;

            string sql = "select count(*) from usuarios where trim(correo) = @correo and status = 'A'";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    // define parametros
                    cmd.Parameters.AddWithValue("@correo", correo);

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

        // inserta un nuevo usuario
        public bool insertaUsuario(string nombreCompleto, string correo, string usuario, string clave)
        {
            MySqlTransaction trans;

            bool result = true;

            string sql = string.Empty;

            int rows = 0;

            string error = string.Empty;

            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                conn.Open();
                using (var cmd = new MySqlCommand())
                {
                    trans = conn.BeginTransaction();

                    try
                    {
                        cmd.Connection = conn;
                        cmd.Transaction = trans;

                        // inserta actuaizacion
                        sql =
                            "INSERT INTO usuarios (nombre_completo, correo, fecha_creacion, usuario, clave, status) " +
                            "VALUES (@nombreCompleto, @correo, now(), @usuario, @clave, @status)";

                        string claveBase64 = Utilerias.Base64Encode(clave);

                        // define parametros
                        cmd.Parameters.AddWithValue("@nombreCompleto", nombreCompleto);
                        cmd.Parameters.AddWithValue("@correo", correo);
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@clave", claveBase64);
                        cmd.Parameters.AddWithValue("@status", "A");

                        ManejoSql_My res = Utilerias.EjecutaSQL(sql, ref rows, cmd);

                        if (!res.ok)
                            throw new Exception(res.numErr + ": " + res.descErr);

                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        throw new Exception(e.Message);
                    }
                }
            }

            return result;
        }

        // valida que la clave es correcta
        public bool validaClave(string claveActual, int _idUsuario)
        {
            bool result = false;

            string sql = "select count(*) from usuarios where clave = @clave and id_usuario = @idUsuario";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    // define parametros
                    cmd.Parameters.AddWithValue("@clave", Utilerias.Base64Encode(claveActual));
                    cmd.Parameters.AddWithValue("@idUsuario", _idUsuario);

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

        // actualiza clave
        public bool actualizaClave(string clave, int idUsuario, string usuario)
        {
            string sql = "update usuarios set clave = @clave where id_usuario = @idUsuario";

            bool result = true;

            int rows = 0;

            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    // define parametros
                    cmd.Parameters.AddWithValue("@clave", Utilerias.Base64Encode(clave));
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

                    ManejoSql_My res = Utilerias.EjecutaSQL(sql, ref rows, cmd);

                    if (!res.ok)
                        throw new Exception(res.numErr + ": " + res.descErr);
                }
            }

            return result;
        }

        // regresa toda la informacion sobre los articulos a actualizar
        public List<Actualizacion> obtieneInformacion(string fechaIni, string fechaFin, int bloque, bool pendiente, bool realizado)
        {
            List<Actualizacion> result = new List<Actualizacion>();
            Actualizacion ent;

            // string sql = "select idusuario, usuario from activos_usuarios where usuario = @usuario and clave = @clave";

            string sql =
                        "select ac.id_actualizacion, ac.num_bloque, ac.fecha, ac.clave_articulo, a.nombre, " +
                        "if(ac.fidel = 'P' , 'PENDIENTE', 'REALIZADO') as fidel, " +
                        "if(ac.heroico = 'P' , 'PENDIENTE', 'REALIZADO') as heroico, " +
                        "if(ac.libertad = 'P' , 'PENDIENTE', 'REALIZADO') as libertad, " +
                        "if(ac.status = 'P' , 'PENDIENTE', 'REALIZADO') as status  " +
                        "from actualizacion ac " +
                        "left join articulos a on (ac.clave_articulo = a.clave) " +
                        "where ac.fecha between @fechaIni and @fechaFin ";

            if (bloque > 0)
                sql += "and ac.num_bloque = @bloque ";

            if (pendiente)
                if (realizado)
                    sql += "and (ac.status = 'R' or ac.status = 'P')";
                else
                    sql += "and (ac.status = 'P')";
            else
                if (realizado)
                    sql += "and (ac.status = 'R')";

            sql += " order by ac.num_bloque asc";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    // define parametros
                    cmd.Parameters.AddWithValue("@fechaIni", fechaIni);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);

                    if(bloque > 0) cmd.Parameters.AddWithValue("@bloque", bloque);

                    ManejoSql_My res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                    {
                        while (res.reader.Read())
                        {
                            ent = new Modelos.Actualizacion();

                            ent.idActualizacion = Convert.ToInt16(res.reader["id_actualizacion"]);
                            ent.claveArticulo = Convert.ToString(res.reader["clave_articulo"]);
                            ent.numBloque = Convert.ToInt16(res.reader["num_bloque"]);

                            ent.fecha = Convert.ToString(res.reader["fecha"]);

                            ent.fidel = Convert.ToString(res.reader["fidel"]);
                            ent.heroico = Convert.ToString(res.reader["heroico"]);
                            ent.libertad = Convert.ToString(res.reader["libertad"]);

                            ent.status = Convert.ToString(res.reader["status"]);

                            ent.nombreArticulo = Convert.ToString(res.reader["nombre"]);

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

        // obtiene los bloques dentro de un rango de fechas
        public List<int> obtieneBloques(string fechaIni, string fechaFin)
        {
            List<int> result = new List<int>();

            string sql =
                        "select num_bloque from actualizacion " +
                        "where fecha between @fechaIni and @fechaFin group by num_bloque";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    // define parametros
                    cmd.Parameters.AddWithValue("@fechaIni", fechaIni);
                    cmd.Parameters.AddWithValue("@fechaFin", fechaFin);

                    ManejoSql_My res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                        while (res.reader.Read())
                            result.Add(Convert.ToInt16(res.reader["num_bloque"]));
                    else
                        throw new Exception(res.numErr + ": " + res.descErr);

                    // cerrar el reader
                    res.reader.Close();

                }
            }

            return result;
        }

        // obtiene los precios detalle
        public ActualizacionDet obtieneDetalle(int idActualizacion)
        {
            ActualizacionDet result = new ActualizacionDet();

            string sql =
                        "select id_actualizacion_det, id_actualizacion, " +
                            "prec_lista, prec_minimo, prec_mayoreo, " +
                            "prec_filial, prec_imss, medio_mayoreo " +
                        "from actualizacion_det where id_actualizacion = @idAct";

            // define conexion con la cadena de conexion
            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                // abre la conexion
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = conn;

                    // define parametros
                    cmd.Parameters.AddWithValue("@idAct", idActualizacion);

                    ManejoSql_My res = Utilerias.EjecutaSQL(sql, cmd);

                    if (res.ok)
                        while (res.reader.Read())
                        {
                            result.idActualizacionDet = Convert.ToInt16(res.reader["id_actualizacion_det"]);
                            result.idActualizacion = Convert.ToInt16(res.reader["id_actualizacion"]);

                            // lista
                            if (res.reader["prec_lista"] is DBNull) result.precLista = null;
                            else
                            {
                                result.precLista = Convert.ToDecimal(Convert.ToString(res.reader["prec_lista"]).TrimEnd(new Char[] { '0' }));
                                //ent.precLista = Convert.ToDecimal(res.reader["prec_lista"]);
                            }

                            // minimo
                            if (res.reader["prec_minimo"] is DBNull) result.precMinimo = null;
                            else
                            {
                                result.precMinimo = Convert.ToDecimal(Convert.ToString(res.reader["prec_minimo"]).TrimEnd(new Char[] { '0' }));
                                //ent.precMinimo = Convert.ToDecimal(res.reader["prec_minimo"]);
                            }

                            // mayoreo
                            if (res.reader["prec_mayoreo"] is DBNull) result.precMayoreo = null;
                            else
                            {
                                result.precMayoreo = Convert.ToDecimal(Convert.ToString(res.reader["prec_mayoreo"]).TrimEnd(new Char[] { '0' }));
                                //ent.precMayoreo = Convert.ToDecimal(res.reader["prec_mayoreo"]);
                            }

                            // filial
                            if (res.reader["prec_filial"] is DBNull) result.precFilial = null;
                            else
                            {
                                result.precFilial = Convert.ToDecimal(Convert.ToString(res.reader["prec_filial"]).TrimEnd(new Char[] { '0' }));
                                //ent.precFilial = Convert.ToDecimal(res.reader["prec_filial"]);
                            }

                            // imss
                            if (res.reader["prec_imss"] is DBNull) result.precIMSS = null;
                            else
                            {
                                result.precIMSS = Convert.ToDecimal(Convert.ToString(res.reader["prec_imss"]).TrimEnd(new Char[] { '0' }));
                                //ent.precIMSS = Convert.ToDecimal(res.reader["prec_imss"]);
                            }

                            // medio mayoreo
                            if (res.reader["medio_mayoreo"] is DBNull) result.medioMayoreo = null;
                            else
                            {
                                result.medioMayoreo = Convert.ToDecimal(Convert.ToString(res.reader["medio_mayoreo"]).TrimEnd(new Char[] { '0' }));
                                //ent.medioMayoreo = Convert.ToDecimal(res.reader["medio_mayoreo"]);
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

        // inserta todos los articulos obtenidos de microsip
        // antes elimina los que ya existen
        public void insertaArticulos(List<Modelos.Articulos> articulos)
        {
            int rows = 0;
            MySqlTransaction trans;

            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                conn.Open();

                using (var cmd = new MySqlCommand())
                {
                    
                    trans = conn.BeginTransaction();

                    try
                    {
                        cmd.Connection = conn;
                        cmd.Transaction = trans;

                        // elimina los articulos
                        string sql = "delete from articulos";

                        ManejoSql_My res = Utilerias.EjecutaSQL(sql, ref rows, cmd);

                        if (!res.ok)
                            throw new Exception(res.numErr + ": " + res.descErr);


                        // inserta los articulos
                        string sqlDet =
                            "INSERT INTO articulos (clave, nombre, prec_lista, prec_minimo, prec_mayoreo, prec_filial, prec_imss, medio_mayoreo) " +
                            "VALUES (@clave, @nombre, @precLista, @precMinimo, @precMayo, @precFilial, @precImss, @medioMayo)";

                        foreach (Modelos.Articulos art in articulos)
                        {
                            // define parametros
                            cmd.Parameters.AddWithValue("@clave", art.clave);
                            cmd.Parameters.AddWithValue("@nombre", art.articulo);
                            cmd.Parameters.AddWithValue("@precLista", art.precLista);
                            cmd.Parameters.AddWithValue("@precMinimo", art.precMinimo);
                            cmd.Parameters.AddWithValue("@precMayo", art.precMayoreo);
                            cmd.Parameters.AddWithValue("@precFilial", art.precFilial);
                            cmd.Parameters.AddWithValue("@precImss", art.precIMSS);
                            cmd.Parameters.AddWithValue("@medioMayo", art.medioMayoreo);

                            res = Utilerias.EjecutaSQL(sqlDet, ref rows, cmd);

                            if (!res.ok) throw new Exception(res.numErr + ": " + res.descErr);

                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        throw new Exception(e.Message);
                    }
                }
            }
        }

        // regresa la lista de los articulos 
        public List<Articulos> obtieneArticulos()
        {
            List<Articulos> result = new List<Articulos>();
            Articulos ent;

            // string sql = "select idusuario, usuario from activos_usuarios where usuario = @usuario and clave = @clave";

            string sql =
                        "select id_articulo, clave, nombre, " +
                            "prec_lista, prec_minimo, prec_mayoreo, " +
                            "prec_filial, prec_imss, medio_mayoreo " +
                        "from articulos";

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
                        while (res.reader.Read())
                        {
                            ent = new Modelos.Articulos();

                            ent.idArticulo = Convert.ToInt64(res.reader["id_articulo"]);
                            ent.articulo = Convert.ToString(res.reader["nombre"]);
                            ent.clave = Convert.ToString(res.reader["clave"]);

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

        // guarda bitacora de cambios de precios
        public void guardaBitacora(List<Articulos> anteriores, List<Articulos> seleccionados, long resultado)
        {
            int rows = 0;
            MySqlTransaction trans;

            using (var conn = this._conexionMySQL.getConexionMySQL())
            {
                conn.Open();

                using (var cmd = new MySqlCommand())
                {

                    trans = conn.BeginTransaction();

                    try
                    {
                        cmd.Connection = conn;
                        cmd.Transaction = trans;

                        // inserta los articulos
                        string sqlDet =
                            "INSERT INTO bitacora_det (id_bitacora, clave, prec_lista, prec_minimo, prec_mayoreo, prec_filial, prec_imss, medio_mayoreo) " +
                            "VALUES (@idBit, @clave, @precLista, @precMinimo, @precMayo, @precFilial, @precImss, @medioMayo)";

                        Modelos.Articulos ant;

                        foreach (Modelos.Articulos art in seleccionados)
                        {
                            ant = new Modelos.Articulos();
                            ant = anteriores.Where(w => w.clave.Equals(art.clave)).FirstOrDefault();

                            // define parametros
                            cmd.Parameters.AddWithValue("@idBit", resultado);
                            cmd.Parameters.AddWithValue("@clave", art.clave);

                            cmd.Parameters.AddWithValue("@precLista", ant.precLista != art.precLista ? ("A:" + ant.precLista + " => N:" + art.precLista) : "-");
                            cmd.Parameters.AddWithValue("@precMinimo", ant.precMinimo != art.precMinimo ? ("A:" + ant.precMinimo + " => N:" + art.precMinimo) : "-");
                            cmd.Parameters.AddWithValue("@precMayo", ant.precMayoreo != art.precMayoreo ? ("A:" + ant.precMayoreo + " => N:" + art.precMayoreo) : "-");
                            cmd.Parameters.AddWithValue("@precFilial", ant.precFilial != art.precFilial ? ("A:" + ant.precFilial + " => N:" + art.precFilial) : "-");
                            cmd.Parameters.AddWithValue("@precImss", ant.precIMSS != art.precIMSS ? ("A:" + ant.precIMSS + " => N:" + art.precIMSS) : "-");
                            cmd.Parameters.AddWithValue("@medioMayo", ant.medioMayoreo != art.medioMayoreo ? ("A:" + ant.medioMayoreo + " => N:" + art.medioMayoreo) : "-");

                            ManejoSql_My res = Utilerias.EjecutaSQL(sqlDet, ref rows, cmd);

                            if (!res.ok) throw new Exception(res.numErr + ": " + res.descErr);

                            cmd.Parameters.Clear();
                        }

                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                        throw new Exception(e.Message);
                    }
                }
            }
        }
    }
}
