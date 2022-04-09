namespace Majales
{
    partial class LoginWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginWindow));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.jmenoBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.hesloBox = new System.Windows.Forms.TextBox();
            this.lblMsg = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnPrihlasit = new System.Windows.Forms.Button();
            this.BtnZapomenute = new System.Windows.Forms.Button();
            this.chckName = new System.Windows.Forms.CheckBox();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.tableLayoutPanel1);
            this.flowLayoutPanel1.Controls.Add(this.lblMsg);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(382, 655);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::Majales.Properties.Resources.logo_web;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(3, 33);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(100, 0, 0, 0);
            this.pictureBox1.Size = new System.Drawing.Size(370, 170);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 25.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.DarkOrange;
            this.label1.Location = new System.Drawing.Point(3, 206);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 30, 0, 60);
            this.label1.Size = new System.Drawing.Size(370, 149);
            this.label1.TabIndex = 5;
            this.label1.Text = "Kolínský Majáles";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 45.67567F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 54.32433F));
            this.tableLayoutPanel1.Controls.Add(this.jmenoBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.hesloBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.chckName, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 360);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(366, 141);
            this.tableLayoutPanel1.TabIndex = 5;
            // 
            // jmenoBox
            // 
            this.jmenoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.jmenoBox.Location = new System.Drawing.Point(170, 3);
            this.jmenoBox.Name = "jmenoBox";
            this.jmenoBox.Size = new System.Drawing.Size(193, 27);
            this.jmenoBox.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 47);
            this.label2.TabIndex = 0;
            this.label2.Text = "Uživatelské jméno:";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(3, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(161, 47);
            this.label3.TabIndex = 1;
            this.label3.Text = "Heslo:";
            // 
            // hesloBox
            // 
            this.hesloBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hesloBox.Location = new System.Drawing.Point(170, 50);
            this.hesloBox.Name = "hesloBox";
            this.hesloBox.PasswordChar = '*';
            this.hesloBox.Size = new System.Drawing.Size(193, 27);
            this.hesloBox.TabIndex = 1;
            // 
            // lblMsg
            // 
            this.lblMsg.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblMsg.AutoSize = true;
            this.lblMsg.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblMsg.ForeColor = System.Drawing.Color.Firebrick;
            this.lblMsg.Location = new System.Drawing.Point(188, 506);
            this.lblMsg.Margin = new System.Windows.Forms.Padding(3, 0, 3, 30);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(0, 23);
            this.lblMsg.TabIndex = 8;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this.BtnPrihlasit);
            this.flowLayoutPanel2.Controls.Add(this.BtnZapomenute);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 562);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(370, 75);
            this.flowLayoutPanel2.TabIndex = 6;
            // 
            // BtnPrihlasit
            // 
            this.BtnPrihlasit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.BtnPrihlasit.AutoSize = true;
            this.BtnPrihlasit.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnPrihlasit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnPrihlasit.Location = new System.Drawing.Point(30, 10);
            this.BtnPrihlasit.Margin = new System.Windows.Forms.Padding(10);
            this.BtnPrihlasit.Name = "BtnPrihlasit";
            this.BtnPrihlasit.Padding = new System.Windows.Forms.Padding(5);
            this.BtnPrihlasit.Size = new System.Drawing.Size(124, 48);
            this.BtnPrihlasit.TabIndex = 2;
            this.BtnPrihlasit.Text = "Přihlásit se";
            this.BtnPrihlasit.UseVisualStyleBackColor = true;
            this.BtnPrihlasit.Click += new System.EventHandler(this.BtnPrihlasit_Click);
            // 
            // BtnZapomenute
            // 
            this.BtnZapomenute.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.BtnZapomenute.AutoSize = true;
            this.BtnZapomenute.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnZapomenute.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnZapomenute.Location = new System.Drawing.Point(174, 10);
            this.BtnZapomenute.Margin = new System.Windows.Forms.Padding(10);
            this.BtnZapomenute.Name = "BtnZapomenute";
            this.BtnZapomenute.Padding = new System.Windows.Forms.Padding(5);
            this.BtnZapomenute.Size = new System.Drawing.Size(180, 48);
            this.BtnZapomenute.TabIndex = 3;
            this.BtnZapomenute.Text = "Zapomenuté heslo";
            this.BtnZapomenute.UseVisualStyleBackColor = true;
            this.BtnZapomenute.Click += new System.EventHandler(this.BtnZapomenute_Click);
            // 
            // chckName
            // 
            this.chckName.AutoSize = true;
            this.chckName.Location = new System.Drawing.Point(170, 97);
            this.chckName.Name = "chckName";
            this.chckName.Size = new System.Drawing.Size(181, 24);
            this.chckName.TabIndex = 4;
            this.chckName.Text = "zapamatovat uživatele";
            this.chckName.UseVisualStyleBackColor = true;
            // 
            // LoginWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 655);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoginWindow";
            this.Text = "Přihlášení";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private PictureBox pictureBox1;
        private Label label1;
        private Button BtnPrihlasit;
        private TableLayoutPanel tableLayoutPanel1;
        private TextBox jmenoBox;
        private Label label2;
        private Label label3;
        private TextBox hesloBox;
        private FlowLayoutPanel flowLayoutPanel2;
        private Button BtnZapomenute;
        private Label lblMsg;
        private CheckBox chckName;
    }
}