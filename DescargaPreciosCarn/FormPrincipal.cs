using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DescargaPreciosCarn.Negocio;

namespace DescargaPreciosCarn
{
    public partial class FormPrincipal : Form
    {
        private IConsultasFBNegocio _consultasFBNegocio;
        private IConsultasMySQLNegocio _consultasMySQLNegocio;

        private List<Modelos.Articulos> _articulos;

        private bool _actualizacion = false;
        private string _sucursal;

        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                this._sucursal = " " + Modelos.Login.sucursal;

                this.lbLeyenda.Text += this._sucursal;

                this._consultasFBNegocio = new ConsultasFBNegocio();
                this._consultasMySQLNegocio = new ConsultasMySQLNegocio();

                this.verificarInformacionPendiente();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void verificarInformacionPendiente()
        {
            // verifica si se tienen descargas pendientes segun la sucursal definida
            bool pendientes = this._consultasMySQLNegocio.verifDescargas(this._sucursal.ToLower());

            if (pendientes)
            {
                this.lbPendientes.Text = "ACTUALIZACIÓN PENDIENTE";
                this.lbPendientes.ForeColor = System.Drawing.Color.Red;
                this._actualizacion = true;
            }
            else
            {
                this.lbPendientes.Text = "ACTUALIZADO";
                this.lbPendientes.ForeColor = System.Drawing.Color.Green;
                this._actualizacion = false;
            }
        }

        private void btnVerifPendiente_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._actualizacion)
                {
                    // cargar articulos a actualizar del primer bloque que se tenga
                    this._articulos = this._consultasMySQLNegocio.getArticulosActualizar(this._sucursal.ToLower());

                    if (this._articulos.Count == 0)
                        throw new Exception("Sin resultados");

                    this.gcArticulos.DataSource = null;
                    this.gcArticulos.DataSource = this._articulos;

                    this.gridView1.BestFitColumns();

                    // bitacora
                    this._consultasMySQLNegocio.generaBitacora(
                        "Información pendiente consultada en '" + Modelos.Login.sucursal + "'");

                }
                else
                    throw new Exception("No hay actualizaciones pendientes");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDescargarInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (this._articulos == null || this._articulos.Count == 0)
                    throw new Exception("No se ha cargado la información");

                // obtiene precios_empresas
                Dictionary<string, long> precEmpr = this._consultasFBNegocio.getPreciosEmpresas();

                foreach (Modelos.Articulos art in this._articulos)
                {
                    // buscar si el articulo esta activo FB
                    Modelos.ArticulosFB artFB = this._consultasFBNegocio.buscaActivo(art.clave);

                    // a = activo
                    if (artFB.estatus.Trim().ToLower().Equals("a"))
                    {
                        // P R E C I O   L I S T A
                        // busca si el articulo tiene precio segun el precio de empresa FB
                        Modelos.PrecioArticulo precArtL = this._consultasFBNegocio.buscaPrecExistencia(art.clave, precEmpr["precio de lista"]);

                        // null = no tiene precio
                        if (precArtL != null)
                            // se actualiza el registro FB
                            this._consultasFBNegocio.actPrecio(precArtL.precioArticuloId, art.precLista);
                        else
                        {
                            if (art.precLista != null)
                                // se inserta si no existe FB
                                this._consultasFBNegocio.insertPrecio(artFB.articuloId, precEmpr["precio de lista"], art.precLista);
                        }


                        // P R E C I O   M I N I M O 
                        // busca si el articulo tiene precio segun el precio de empresa FB
                        Modelos.PrecioArticulo precArtMin = this._consultasFBNegocio.buscaPrecExistencia(art.clave, precEmpr["precio mínimo"]);

                        // null = no tiene precio
                        if (precArtMin != null)
                            // se actualiza el registro FB
                            this._consultasFBNegocio.actPrecio(precArtMin.precioArticuloId, art.precMinimo);
                        else
                        {
                            if (art.precMinimo != null)
                                // se inserta si no existe FB
                                this._consultasFBNegocio.insertPrecio(artFB.articuloId, precEmpr["precio mínimo"], art.precMinimo);
                        }


                        // P R E C I O   M A Y O R E O
                        // busca si el articulo tiene precio segun el precio de empresa FB
                        Modelos.PrecioArticulo precArtMayo = this._consultasFBNegocio.buscaPrecExistencia(art.clave, precEmpr["prec.mayoreo"]);

                        // null = no tiene precio
                        if (precArtMayo != null)
                            // se actualiza el registro FB
                            this._consultasFBNegocio.actPrecio(precArtMayo.precioArticuloId, art.precMayoreo);
                        else
                        {
                            if (art.precMayoreo != null)
                                // se inserta si no existe FB
                                this._consultasFBNegocio.insertPrecio(artFB.articuloId, precEmpr["prec.mayoreo"], art.precLista);
                        }


                        // P R E C I O   F I L I A L
                        // busca si el articulo tiene precio segun el precio de empresa FB
                        Modelos.PrecioArticulo precArtF = this._consultasFBNegocio.buscaPrecExistencia(art.clave, precEmpr["prec.filial"]);

                        // null = no tiene precio
                        if (precArtF != null)
                            // se actualiza el registro FB
                            this._consultasFBNegocio.actPrecio(precArtF.precioArticuloId, art.precFilial);
                        else
                        {
                            if (art.precFilial != null)
                                // se inserta si no existe FB
                                this._consultasFBNegocio.insertPrecio(artFB.articuloId, precEmpr["prec.filial"], art.precFilial);
                        }


                        // P R E C I O   I M S S 
                        // busca si el articulo tiene precio segun el precio de empresa FB
                        Modelos.PrecioArticulo precArtIMSS = this._consultasFBNegocio.buscaPrecExistencia(art.clave, precEmpr["prec.i.m.s.s."]);

                        // null = no tiene precio
                        if (precArtIMSS != null)
                            // se actualiza el registro FB
                            this._consultasFBNegocio.actPrecio(precArtIMSS.precioArticuloId, art.precIMSS);
                        else
                        {
                            if (art.precIMSS != null)
                                // se inserta si no existe FB
                                this._consultasFBNegocio.insertPrecio(artFB.articuloId, precEmpr["prec.i.m.s.s."], art.precIMSS);
                        }


                        // M E D I O   M A Y O R E O
                        // busca si el articulo tiene precio segun el precio de empresa FB
                        Modelos.PrecioArticulo precArtMM = this._consultasFBNegocio.buscaPrecExistencia(art.clave, precEmpr["medio mayoreo"]);

                        // null = no tiene precio
                        if (precArtMM != null)
                            // se actualiza el registro FB
                            this._consultasFBNegocio.actPrecio(precArtMM.precioArticuloId, art.medioMayoreo);
                        else
                        {
                            if (art.medioMayoreo != null)
                                // se inserta si no existe FB
                                this._consultasFBNegocio.insertPrecio(artFB.articuloId, precEmpr["medio mayoreo"], art.medioMayoreo);
                        }



                        // se libera la actualizacion del articulo MS
                        bool resultado = this._consultasMySQLNegocio.liberaArticulo(art.clave, art.bloque, this._sucursal.ToLower());

                        // bitacora
                        this._consultasMySQLNegocio.generaBitacora(
                        "Descarga completa del artículo con clave '" + art.clave +
                        "' en el bloque '" + art.bloque + "' y en la sucursal '" + Modelos.Login.sucursal + "'");
                    }
                    else
                    {
                        // no es un articulo activo
                        MessageBox.Show("El artículo '" + art.articulo +
                            "' no se encuentra activo.\nEl proceso de actualización continuará pero este artículo no se actualizará"
                            ,"Actualizar Precios Carnicerías"
                            , MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // mandar mensaje y generar bitacora
                        this._consultasMySQLNegocio.generaBitacora(
                        "El artículo '" + art.articulo + "' con clave '" + art.clave +
                        "' en el bloque '" + art.bloque + "' y en la sucursal '" + Modelos.Login.sucursal +
                        "' no se encuetra Activo en la sucursal");

                    }
                }

                // se libera el bloque de actualizacion MS
                bool resAct = this._consultasMySQLNegocio.liberaBloque(this._articulos[0].bloque, this._sucursal.ToLower());
                int bloque = this._articulos[0].bloque;
                
                if (resAct)
                {
                    // bitacora
                    this._consultasMySQLNegocio.generaBitacora(
                        "Bloque '" + this._articulos[0].bloque + "' descargado completamente en la sucursal '" + Modelos.Login.sucursal);

                    MessageBox.Show("Proceso Concluido", "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // borrar registros a actualizar
                    this.gcArticulos.DataSource = new List<Modelos.Articulos>();
                    this._articulos = new List<Modelos.Articulos>();

                    this.lbPendientes.Text = "VERIFIQUE ACTUALIZACIONES ->";
                    this.lbPendientes.ForeColor = System.Drawing.Color.Blue;
                }

                // verifica si ya se ha descargado la informacion en todas las sucursales
                // de ser correcto, se actualizara el estatus general del articulo
                bool actTodosBloque = this._consultasMySQLNegocio.liberarBloques(bloque);

                if (actTodosBloque)
                    // bitacora
                    this._consultasMySQLNegocio.generaBitacora(
                        "Bloque '" + bloque + "' descargado completamente en todas las sucursales");

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnActPendientes_Click(object sender, EventArgs e)
        {
            try
            {
                this.verificarInformacionPendiente();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Actualizar Precios Carnicerías", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
