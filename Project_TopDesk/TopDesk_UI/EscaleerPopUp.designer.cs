namespace TopDesk_UI
{
    partial class EscaleerPopUp
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
            this.lblWarning = new System.Windows.Forms.Label();
            this.lblCategorie = new System.Windows.Forms.Label();
            this.lblWerknemer = new System.Windows.Forms.Label();
            this.cbCategorie = new System.Windows.Forms.ComboBox();
            this.cbWerknemer = new System.Windows.Forms.ComboBox();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txbWerknemer = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblWarning
            // 
            this.lblWarning.AutoSize = true;
            this.lblWarning.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWarning.Location = new System.Drawing.Point(12, 25);
            this.lblWarning.Name = "lblWarning";
            this.lblWarning.Size = new System.Drawing.Size(43, 23);
            this.lblWarning.TabIndex = 0;
            this.lblWarning.Text = "test";
            // 
            // lblCategorie
            // 
            this.lblCategorie.AutoSize = true;
            this.lblCategorie.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategorie.Location = new System.Drawing.Point(12, 78);
            this.lblCategorie.Name = "lblCategorie";
            this.lblCategorie.Size = new System.Drawing.Size(108, 23);
            this.lblCategorie.TabIndex = 1;
            this.lblCategorie.Text = "Categorie: ";
            // 
            // lblWerknemer
            // 
            this.lblWerknemer.AutoSize = true;
            this.lblWerknemer.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWerknemer.Location = new System.Drawing.Point(12, 131);
            this.lblWerknemer.Name = "lblWerknemer";
            this.lblWerknemer.Size = new System.Drawing.Size(124, 23);
            this.lblWerknemer.TabIndex = 2;
            this.lblWerknemer.Text = "Werknemer: ";
            // 
            // cbCategorie
            // 
            this.cbCategorie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCategorie.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCategorie.FormattingEnabled = true;
            this.cbCategorie.Location = new System.Drawing.Point(162, 78);
            this.cbCategorie.MaxDropDownItems = 12;
            this.cbCategorie.Name = "cbCategorie";
            this.cbCategorie.Size = new System.Drawing.Size(185, 31);
            this.cbCategorie.TabIndex = 3;
            this.cbCategorie.SelectedIndexChanged += new System.EventHandler(this.cbCategorie_SelectedIndexChanged);
            // 
            // cbWerknemer
            // 
            this.cbWerknemer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWerknemer.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbWerknemer.FormattingEnabled = true;
            this.cbWerknemer.Location = new System.Drawing.Point(162, 167);
            this.cbWerknemer.Name = "cbWerknemer";
            this.cbWerknemer.Size = new System.Drawing.Size(185, 31);
            this.cbWerknemer.TabIndex = 4;
            this.cbWerknemer.SelectedIndexChanged += new System.EventHandler(this.cbWerknemer_SelectedIndexChanged);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirm.Location = new System.Drawing.Point(71, 216);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(149, 39);
            this.btnConfirm.TabIndex = 5;
            this.btnConfirm.Text = "Confirm";
            this.btnConfirm.UseVisualStyleBackColor = true;
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(226, 216);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(121, 39);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txbWerknemer
            // 
            this.txbWerknemer.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txbWerknemer.Location = new System.Drawing.Point(162, 131);
            this.txbWerknemer.Name = "txbWerknemer";
            this.txbWerknemer.Size = new System.Drawing.Size(185, 30);
            this.txbWerknemer.TabIndex = 7;
            this.txbWerknemer.TextChanged += new System.EventHandler(this.txbWerknemer_TextChanged_1);
            // 
            // EscaleerPopUp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(419, 295);
            this.Controls.Add(this.txbWerknemer);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConfirm);
            this.Controls.Add(this.cbWerknemer);
            this.Controls.Add(this.cbCategorie);
            this.Controls.Add(this.lblWerknemer);
            this.Controls.Add(this.lblCategorie);
            this.Controls.Add(this.lblWarning);
            this.Name = "EscaleerPopUp";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.EscaleerPopUp_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWarning;
        private System.Windows.Forms.Label lblCategorie;
        private System.Windows.Forms.Label lblWerknemer;
        private System.Windows.Forms.ComboBox cbCategorie;
        private System.Windows.Forms.ComboBox cbWerknemer;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txbWerknemer;
    }
}