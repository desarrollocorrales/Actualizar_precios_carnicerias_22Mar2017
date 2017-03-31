using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ActPreciosCarn.GUIs
{
    public partial class frmVerPrecios : Form
    {
        private decimal? _precLista;
        private decimal? _precMinimo;
        private decimal? _precMayoreo;
        private decimal? _precFilial;
        private decimal? _precIMSS;
        private decimal? _medioMayoreo;

        private string _nombreArticulo;

        public frmVerPrecios(string nombreArticulo,
            decimal? precLista, decimal? precMinimo, decimal? precMayoreo,
            decimal? precFilial, decimal? precIMSS, decimal? medioMayoreo)
        {
            InitializeComponent();

            this._precLista = precLista;
            this._precMinimo = precMinimo;
            this._precMayoreo = precMayoreo;
            this._precFilial = precFilial;
            this._precIMSS = precIMSS;
            this._medioMayoreo = medioMayoreo;

            this._nombreArticulo = nombreArticulo;
        }

        private void frmVerPrecios_Load(object sender, EventArgs e)
        {
            this.tbPrecioLista.Text = Convert.ToString(this._precLista);
            this.tbPrecioMinimo.Text = Convert.ToString(this._precMinimo);
            this.tbPrecioMayoreo.Text = Convert.ToString(this._precMayoreo);
            this.tbPrecioFilial.Text = Convert.ToString(this._precFilial);
            this.tbPrecioIMSS.Text = Convert.ToString(this._precIMSS);
            this.tbMedioMayoreo.Text = Convert.ToString(this._medioMayoreo);

            this.lbArticulo.Text = this._nombreArticulo;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
