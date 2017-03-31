namespace ActPreciosCarn.GUIs
{
    partial class frmVerPrecios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVerPrecios));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPrecioLista = new System.Windows.Forms.TextBox();
            this.tbPrecioMinimo = new System.Windows.Forms.TextBox();
            this.tbPrecioMayoreo = new System.Windows.Forms.TextBox();
            this.tbPrecioFilial = new System.Windows.Forms.TextBox();
            this.tbPrecioIMSS = new System.Windows.Forms.TextBox();
            this.tbMedioMayoreo = new System.Windows.Forms.TextBox();
            this.btnCerrar = new System.Windows.Forms.Button();
            this.lbArticulo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 99);
            this.label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Precio de Lista";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(188, 99);
            this.label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(126, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Precio Mínimo";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(362, 99);
            this.label3.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "Precio Mayoreo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 162);
            this.label4.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 23);
            this.label4.TabIndex = 3;
            this.label4.Text = "Precio Filial";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(188, 162);
            this.label5.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 23);
            this.label5.TabIndex = 4;
            this.label5.Text = "Precio IMSS";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(362, 162);
            this.label6.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(137, 23);
            this.label6.TabIndex = 5;
            this.label6.Text = "Medio Mayoreo";
            // 
            // tbPrecioLista
            // 
            this.tbPrecioLista.Location = new System.Drawing.Point(18, 127);
            this.tbPrecioLista.Margin = new System.Windows.Forms.Padding(5);
            this.tbPrecioLista.Name = "tbPrecioLista";
            this.tbPrecioLista.ReadOnly = true;
            this.tbPrecioLista.Size = new System.Drawing.Size(164, 30);
            this.tbPrecioLista.TabIndex = 6;
            this.tbPrecioLista.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbPrecioMinimo
            // 
            this.tbPrecioMinimo.Location = new System.Drawing.Point(192, 127);
            this.tbPrecioMinimo.Margin = new System.Windows.Forms.Padding(5);
            this.tbPrecioMinimo.Name = "tbPrecioMinimo";
            this.tbPrecioMinimo.ReadOnly = true;
            this.tbPrecioMinimo.Size = new System.Drawing.Size(164, 30);
            this.tbPrecioMinimo.TabIndex = 7;
            this.tbPrecioMinimo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbPrecioMayoreo
            // 
            this.tbPrecioMayoreo.Location = new System.Drawing.Point(366, 127);
            this.tbPrecioMayoreo.Margin = new System.Windows.Forms.Padding(5);
            this.tbPrecioMayoreo.Name = "tbPrecioMayoreo";
            this.tbPrecioMayoreo.ReadOnly = true;
            this.tbPrecioMayoreo.Size = new System.Drawing.Size(164, 30);
            this.tbPrecioMayoreo.TabIndex = 8;
            this.tbPrecioMayoreo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbPrecioFilial
            // 
            this.tbPrecioFilial.Location = new System.Drawing.Point(18, 190);
            this.tbPrecioFilial.Margin = new System.Windows.Forms.Padding(5);
            this.tbPrecioFilial.Name = "tbPrecioFilial";
            this.tbPrecioFilial.ReadOnly = true;
            this.tbPrecioFilial.Size = new System.Drawing.Size(164, 30);
            this.tbPrecioFilial.TabIndex = 9;
            this.tbPrecioFilial.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbPrecioIMSS
            // 
            this.tbPrecioIMSS.Location = new System.Drawing.Point(192, 190);
            this.tbPrecioIMSS.Margin = new System.Windows.Forms.Padding(5);
            this.tbPrecioIMSS.Name = "tbPrecioIMSS";
            this.tbPrecioIMSS.ReadOnly = true;
            this.tbPrecioIMSS.Size = new System.Drawing.Size(164, 30);
            this.tbPrecioIMSS.TabIndex = 10;
            this.tbPrecioIMSS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbMedioMayoreo
            // 
            this.tbMedioMayoreo.Location = new System.Drawing.Point(366, 190);
            this.tbMedioMayoreo.Margin = new System.Windows.Forms.Padding(5);
            this.tbMedioMayoreo.Name = "tbMedioMayoreo";
            this.tbMedioMayoreo.ReadOnly = true;
            this.tbMedioMayoreo.Size = new System.Drawing.Size(164, 30);
            this.tbMedioMayoreo.TabIndex = 11;
            this.tbMedioMayoreo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnCerrar
            // 
            this.btnCerrar.Location = new System.Drawing.Point(374, 233);
            this.btnCerrar.Margin = new System.Windows.Forms.Padding(5);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(125, 41);
            this.btnCerrar.TabIndex = 12;
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.UseVisualStyleBackColor = true;
            this.btnCerrar.Click += new System.EventHandler(this.btnCerrar_Click);
            // 
            // lbArticulo
            // 
            this.lbArticulo.Font = new System.Drawing.Font("Tahoma", 22F);
            this.lbArticulo.Location = new System.Drawing.Point(12, 9);
            this.lbArticulo.Name = "lbArticulo";
            this.lbArticulo.Size = new System.Drawing.Size(518, 76);
            this.lbArticulo.TabIndex = 13;
            this.lbArticulo.Text = "ARTICULO_NOMBRE";
            this.lbArticulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmVerPrecios
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 294);
            this.Controls.Add(this.lbArticulo);
            this.Controls.Add(this.btnCerrar);
            this.Controls.Add(this.tbMedioMayoreo);
            this.Controls.Add(this.tbPrecioIMSS);
            this.Controls.Add(this.tbPrecioFilial);
            this.Controls.Add(this.tbPrecioMayoreo);
            this.Controls.Add(this.tbPrecioMinimo);
            this.Controls.Add(this.tbPrecioLista);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("Tahoma", 14F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "frmVerPrecios";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ver Precios";
            this.Load += new System.EventHandler(this.frmVerPrecios_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbPrecioLista;
        private System.Windows.Forms.TextBox tbPrecioMinimo;
        private System.Windows.Forms.TextBox tbPrecioMayoreo;
        private System.Windows.Forms.TextBox tbPrecioFilial;
        private System.Windows.Forms.TextBox tbPrecioIMSS;
        private System.Windows.Forms.TextBox tbMedioMayoreo;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Label lbArticulo;
    }
}