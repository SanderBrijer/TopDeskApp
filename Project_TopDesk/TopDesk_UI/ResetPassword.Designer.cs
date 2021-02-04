namespace TopDesk_UI
{
    partial class ResetPassword
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
            this.label9 = new System.Windows.Forms.Label();
            this.txtAntwoordCheck = new System.Windows.Forms.TextBox();
            this.BtnCheckAntwoord = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCheckGebruiker = new System.Windows.Forms.TextBox();
            this.pnlResetPassword = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.btnWijzigWW = new System.Windows.Forms.Button();
            this.txtHerhaaldWW = new System.Windows.Forms.TextBox();
            this.txtNieuwWW = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlResetPassword.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(46, 64);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(533, 23);
            this.label9.TabIndex = 16;
            this.label9.Text = "Security question: Wat was de naam van uw eerste school?";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // txtAntwoordCheck
            // 
            this.txtAntwoordCheck.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAntwoordCheck.Location = new System.Drawing.Point(605, 67);
            this.txtAntwoordCheck.Name = "txtAntwoordCheck";
            this.txtAntwoordCheck.Size = new System.Drawing.Size(183, 30);
            this.txtAntwoordCheck.TabIndex = 17;
            this.txtAntwoordCheck.TextChanged += new System.EventHandler(this.txtAntwoordCheck_TextChanged);
            // 
            // BtnCheckAntwoord
            // 
            this.BtnCheckAntwoord.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCheckAntwoord.Location = new System.Drawing.Point(605, 103);
            this.BtnCheckAntwoord.Name = "BtnCheckAntwoord";
            this.BtnCheckAntwoord.Size = new System.Drawing.Size(183, 50);
            this.BtnCheckAntwoord.TabIndex = 18;
            this.BtnCheckAntwoord.Text = "Check antwoord";
            this.BtnCheckAntwoord.UseVisualStyleBackColor = true;
            this.BtnCheckAntwoord.Click += new System.EventHandler(this.BtnCheckAntwoord_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(46, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 23);
            this.label1.TabIndex = 19;
            this.label1.Text = "Gebruikersnaam";
            // 
            // txtCheckGebruiker
            // 
            this.txtCheckGebruiker.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCheckGebruiker.Location = new System.Drawing.Point(605, 24);
            this.txtCheckGebruiker.Name = "txtCheckGebruiker";
            this.txtCheckGebruiker.Size = new System.Drawing.Size(183, 30);
            this.txtCheckGebruiker.TabIndex = 20;
            this.txtCheckGebruiker.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // pnlResetPassword
            // 
            this.pnlResetPassword.Controls.Add(this.label11);
            this.pnlResetPassword.Controls.Add(this.btnWijzigWW);
            this.pnlResetPassword.Controls.Add(this.txtHerhaaldWW);
            this.pnlResetPassword.Controls.Add(this.txtNieuwWW);
            this.pnlResetPassword.Controls.Add(this.label3);
            this.pnlResetPassword.Controls.Add(this.label2);
            this.pnlResetPassword.Location = new System.Drawing.Point(49, 187);
            this.pnlResetPassword.Name = "pnlResetPassword";
            this.pnlResetPassword.Size = new System.Drawing.Size(751, 230);
            this.pnlResetPassword.TabIndex = 21;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(20, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(313, 23);
            this.label11.TabIndex = 24;
            this.label11.Text = "(Incl hoofdletter, cijfer en symbool)";
            // 
            // btnWijzigWW
            // 
            this.btnWijzigWW.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnWijzigWW.Location = new System.Drawing.Point(556, 134);
            this.btnWijzigWW.Name = "btnWijzigWW";
            this.btnWijzigWW.Size = new System.Drawing.Size(183, 50);
            this.btnWijzigWW.TabIndex = 19;
            this.btnWijzigWW.Text = "Reset wachtwoord";
            this.btnWijzigWW.UseVisualStyleBackColor = true;
            this.btnWijzigWW.Click += new System.EventHandler(this.btnWijzigWW_Click);
            // 
            // txtHerhaaldWW
            // 
            this.txtHerhaaldWW.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtHerhaaldWW.Location = new System.Drawing.Point(556, 88);
            this.txtHerhaaldWW.Name = "txtHerhaaldWW";
            this.txtHerhaaldWW.Size = new System.Drawing.Size(183, 30);
            this.txtHerhaaldWW.TabIndex = 3;
            // 
            // txtNieuwWW
            // 
            this.txtNieuwWW.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNieuwWW.Location = new System.Drawing.Point(556, 34);
            this.txtNieuwWW.Name = "txtNieuwWW";
            this.txtNieuwWW.Size = new System.Drawing.Size(183, 30);
            this.txtNieuwWW.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(223, 23);
            this.label3.TabIndex = 1;
            this.label3.Text = "Herhaal het wachtwoord";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(261, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "Geef een nieuw wachtwoord";
            // 
            // ResetPassword
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlResetPassword);
            this.Controls.Add(this.txtCheckGebruiker);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnCheckAntwoord);
            this.Controls.Add(this.txtAntwoordCheck);
            this.Controls.Add(this.label9);
            this.Name = "ResetPassword";
            this.Text = "ResetPassword";
            this.pnlResetPassword.ResumeLayout(false);
            this.pnlResetPassword.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAntwoordCheck;
        private System.Windows.Forms.Button BtnCheckAntwoord;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCheckGebruiker;
        private System.Windows.Forms.Panel pnlResetPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnWijzigWW;
        private System.Windows.Forms.TextBox txtHerhaaldWW;
        private System.Windows.Forms.TextBox txtNieuwWW;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label11;
    }
}