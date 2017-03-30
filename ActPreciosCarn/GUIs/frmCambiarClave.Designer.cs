namespace ActPreciosCarn.GUIs
{
    partial class frmCambiarClave
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCambiarClave));
            this.btnGuardar = new System.Windows.Forms.Button();
            this.tbClaveActual = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbConfirmClave = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tbClaveNueva = new System.Windows.Forms.TextBox();
            this.tbUsuario = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnGuardar
            // 
            this.btnGuardar.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.btnGuardar.Location = new System.Drawing.Point(135, 221);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(191, 49);
            this.btnGuardar.TabIndex = 50;
            this.btnGuardar.Text = "&Guardar";
            this.btnGuardar.UseVisualStyleBackColor = true;
            this.btnGuardar.Click += new System.EventHandler(this.btnGuardar_Click);
            // 
            // tbClaveActual
            // 
            this.tbClaveActual.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.tbClaveActual.Location = new System.Drawing.Point(135, 51);
            this.tbClaveActual.MaxLength = 20;
            this.tbClaveActual.Name = "tbClaveActual";
            this.tbClaveActual.Size = new System.Drawing.Size(292, 30);
            this.tbClaveActual.TabIndex = 47;
            this.tbClaveActual.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.label1.Location = new System.Drawing.Point(18, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 23);
            this.label1.TabIndex = 46;
            this.label1.Text = "Clave Actual";
            // 
            // tbConfirmClave
            // 
            this.tbConfirmClave.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.tbConfirmClave.Location = new System.Drawing.Point(135, 161);
            this.tbConfirmClave.MaxLength = 20;
            this.tbConfirmClave.Name = "tbConfirmClave";
            this.tbConfirmClave.Size = new System.Drawing.Size(292, 30);
            this.tbConfirmClave.TabIndex = 49;
            this.tbConfirmClave.UseSystemPasswordChar = true;
            this.tbConfirmClave.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbConfirmClave_KeyPress);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.label8.Location = new System.Drawing.Point(35, 149);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 50);
            this.label8.TabIndex = 45;
            this.label8.Text = "Confirmar Clave";
            this.label8.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // tbClaveNueva
            // 
            this.tbClaveNueva.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.tbClaveNueva.Location = new System.Drawing.Point(135, 113);
            this.tbClaveNueva.MaxLength = 20;
            this.tbClaveNueva.Name = "tbClaveNueva";
            this.tbClaveNueva.Size = new System.Drawing.Size(292, 30);
            this.tbClaveNueva.TabIndex = 48;
            this.tbClaveNueva.UseSystemPasswordChar = true;
            // 
            // tbUsuario
            // 
            this.tbUsuario.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.tbUsuario.Location = new System.Drawing.Point(135, 15);
            this.tbUsuario.MaxLength = 20;
            this.tbUsuario.Name = "tbUsuario";
            this.tbUsuario.ReadOnly = true;
            this.tbUsuario.Size = new System.Drawing.Size(292, 30);
            this.tbUsuario.TabIndex = 44;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.label6.Location = new System.Drawing.Point(16, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 23);
            this.label6.TabIndex = 43;
            this.label6.Text = "Clave Nueva";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 14.25F);
            this.label5.Location = new System.Drawing.Point(57, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 23);
            this.label5.TabIndex = 42;
            this.label5.Text = "Usuario";
            // 
            // frmCambiarClave
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 285);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.tbClaveActual);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbConfirmClave);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.tbClaveNueva);
            this.Controls.Add(this.tbUsuario);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCambiarClave";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cambiar Clave";
            this.Load += new System.EventHandler(this.frmCambiarClave_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.TextBox tbClaveActual;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbConfirmClave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbClaveNueva;
        private System.Windows.Forms.TextBox tbUsuario;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
    }
}