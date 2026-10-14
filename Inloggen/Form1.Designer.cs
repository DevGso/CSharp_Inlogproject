namespace Inloggen
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.lblInloggen = new System.Windows.Forms.Label();
            this.lblEmailInvullen = new System.Windows.Forms.Label();
            this.lblWachtwoordInvullen = new System.Windows.Forms.Label();
            this.lblInfoGeenAccount = new System.Windows.Forms.Label();
            this.btnInloggen = new System.Windows.Forms.Button();
            this.btnRegistreren = new System.Windows.Forms.Button();
            this.txtWachtwoordInvoer = new System.Windows.Forms.TextBox();
            this.txtEmailInvoer = new System.Windows.Forms.TextBox();
            this.errorProviderDataBase = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderDataBase)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInloggen
            // 
            this.lblInloggen.AutoSize = true;
            this.lblInloggen.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInloggen.Location = new System.Drawing.Point(334, 38);
            this.lblInloggen.Name = "lblInloggen";
            this.lblInloggen.Size = new System.Drawing.Size(140, 38);
            this.lblInloggen.TabIndex = 0;
            this.lblInloggen.Text = "Inloggen";
            // 
            // lblEmailInvullen
            // 
            this.lblEmailInvullen.AutoSize = true;
            this.lblEmailInvullen.Location = new System.Drawing.Point(340, 99);
            this.lblEmailInvullen.Name = "lblEmailInvullen";
            this.lblEmailInvullen.Size = new System.Drawing.Size(121, 25);
            this.lblEmailInvullen.TabIndex = 1;
            this.lblEmailInvullen.Text = "E-mail adres";
            // 
            // lblWachtwoordInvullen
            // 
            this.lblWachtwoordInvullen.AutoSize = true;
            this.lblWachtwoordInvullen.Location = new System.Drawing.Point(340, 189);
            this.lblWachtwoordInvullen.Name = "lblWachtwoordInvullen";
            this.lblWachtwoordInvullen.Size = new System.Drawing.Size(122, 25);
            this.lblWachtwoordInvullen.TabIndex = 2;
            this.lblWachtwoordInvullen.Text = "Wachtwoord";
            // 
            // lblInfoGeenAccount
            // 
            this.lblInfoGeenAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblInfoGeenAccount.AutoSize = true;
            this.lblInfoGeenAccount.Location = new System.Drawing.Point(12, 426);
            this.lblInfoGeenAccount.Name = "lblInfoGeenAccount";
            this.lblInfoGeenAccount.Size = new System.Drawing.Size(209, 25);
            this.lblInfoGeenAccount.TabIndex = 3;
            this.lblInfoGeenAccount.Text = "Nog geen accout? dan";
            // 
            // btnInloggen
            // 
            this.btnInloggen.Location = new System.Drawing.Point(329, 281);
            this.btnInloggen.Name = "btnInloggen";
            this.btnInloggen.Size = new System.Drawing.Size(137, 49);
            this.btnInloggen.TabIndex = 4;
            this.btnInloggen.Text = "Inloggen";
            this.btnInloggen.UseVisualStyleBackColor = true;
            this.btnInloggen.Click += new System.EventHandler(this.btnInloggen_Click);
            // 
            // btnRegistreren
            // 
            this.btnRegistreren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRegistreren.Location = new System.Drawing.Point(221, 423);
            this.btnRegistreren.Name = "btnRegistreren";
            this.btnRegistreren.Size = new System.Drawing.Size(132, 34);
            this.btnRegistreren.TabIndex = 5;
            this.btnRegistreren.Text = "Registreren";
            this.btnRegistreren.UseVisualStyleBackColor = true;
            // 
            // txtWachtwoordInvoer
            // 
            this.txtWachtwoordInvoer.Location = new System.Drawing.Point(316, 219);
            this.txtWachtwoordInvoer.Name = "txtWachtwoordInvoer";
            this.txtWachtwoordInvoer.Size = new System.Drawing.Size(176, 29);
            this.txtWachtwoordInvoer.TabIndex = 7;
            // 
            // txtEmailInvoer
            // 
            this.txtEmailInvoer.Location = new System.Drawing.Point(316, 129);
            this.txtEmailInvoer.Name = "txtEmailInvoer";
            this.txtEmailInvoer.Size = new System.Drawing.Size(176, 29);
            this.txtEmailInvoer.TabIndex = 8;
            // 
            // errorProviderDataBase
            // 
            this.errorProviderDataBase.ContainerControl = this;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 472);
            this.Controls.Add(this.txtEmailInvoer);
            this.Controls.Add(this.txtWachtwoordInvoer);
            this.Controls.Add(this.btnRegistreren);
            this.Controls.Add(this.btnInloggen);
            this.Controls.Add(this.lblInfoGeenAccount);
            this.Controls.Add(this.lblWachtwoordInvullen);
            this.Controls.Add(this.lblEmailInvullen);
            this.Controls.Add(this.lblInloggen);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.errorProviderDataBase)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblInloggen;
        private System.Windows.Forms.Label lblEmailInvullen;
        private System.Windows.Forms.Label lblWachtwoordInvullen;
        private System.Windows.Forms.Label lblInfoGeenAccount;
        private System.Windows.Forms.Button btnInloggen;
        private System.Windows.Forms.Button btnRegistreren;
        private System.Windows.Forms.TextBox txtWachtwoordInvoer;
        private System.Windows.Forms.TextBox txtEmailInvoer;
        private System.Windows.Forms.ErrorProvider errorProviderDataBase;
    }
}

