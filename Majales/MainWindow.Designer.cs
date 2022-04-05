namespace Majales
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnTym = new System.Windows.Forms.Button();
            this.BtnProgram = new System.Windows.Forms.Button();
            this.BtnUkoly = new System.Windows.Forms.Button();
            this.BtnZmenitHeslo = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.BtnTym);
            this.flowLayoutPanel1.Controls.Add(this.BtnProgram);
            this.flowLayoutPanel1.Controls.Add(this.BtnUkoly);
            this.flowLayoutPanel1.Controls.Add(this.BtnZmenitHeslo);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(0, 30, 0, 0);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(382, 664);
            this.flowLayoutPanel1.TabIndex = 0;
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
            this.label1.TabIndex = 1;
            this.label1.Text = "Kolínský Majáles";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnTym
            // 
            this.BtnTym.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnTym.AutoSize = true;
            this.BtnTym.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnTym.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnTym.Location = new System.Drawing.Point(150, 365);
            this.BtnTym.Margin = new System.Windows.Forms.Padding(10);
            this.BtnTym.Name = "BtnTym";
            this.BtnTym.Padding = new System.Windows.Forms.Padding(5);
            this.BtnTym.Size = new System.Drawing.Size(76, 51);
            this.BtnTym.TabIndex = 2;
            this.BtnTym.Text = "Tým";
            this.BtnTym.UseVisualStyleBackColor = true;
            this.BtnTym.Click += new System.EventHandler(this.BtnTym_Click);
            // 
            // BtnProgram
            // 
            this.BtnProgram.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnProgram.AutoSize = true;
            this.BtnProgram.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnProgram.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnProgram.Location = new System.Drawing.Point(127, 436);
            this.BtnProgram.Margin = new System.Windows.Forms.Padding(10);
            this.BtnProgram.Name = "BtnProgram";
            this.BtnProgram.Padding = new System.Windows.Forms.Padding(5);
            this.BtnProgram.Size = new System.Drawing.Size(122, 51);
            this.BtnProgram.TabIndex = 3;
            this.BtnProgram.Text = "Program";
            this.BtnProgram.UseVisualStyleBackColor = true;
            this.BtnProgram.Click += new System.EventHandler(this.BtnProgram_Click);
            // 
            // BtnUkoly
            // 
            this.BtnUkoly.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnUkoly.AutoSize = true;
            this.BtnUkoly.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnUkoly.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnUkoly.Location = new System.Drawing.Point(142, 507);
            this.BtnUkoly.Margin = new System.Windows.Forms.Padding(10);
            this.BtnUkoly.Name = "BtnUkoly";
            this.BtnUkoly.Padding = new System.Windows.Forms.Padding(5);
            this.BtnUkoly.Size = new System.Drawing.Size(91, 51);
            this.BtnUkoly.TabIndex = 4;
            this.BtnUkoly.Text = "Úkoly";
            this.BtnUkoly.UseVisualStyleBackColor = true;
            this.BtnUkoly.Click += new System.EventHandler(this.BtnUkoly_Click);
            // 
            // BtnZmenitHeslo
            // 
            this.BtnZmenitHeslo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.BtnZmenitHeslo.AutoSize = true;
            this.BtnZmenitHeslo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnZmenitHeslo.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnZmenitHeslo.Location = new System.Drawing.Point(123, 598);
            this.BtnZmenitHeslo.Margin = new System.Windows.Forms.Padding(3, 30, 3, 3);
            this.BtnZmenitHeslo.Name = "BtnZmenitHeslo";
            this.BtnZmenitHeslo.Padding = new System.Windows.Forms.Padding(3);
            this.BtnZmenitHeslo.Size = new System.Drawing.Size(130, 41);
            this.BtnZmenitHeslo.TabIndex = 5;
            this.BtnZmenitHeslo.Text = "Změnit heslo";
            this.BtnZmenitHeslo.UseVisualStyleBackColor = true;
            this.BtnZmenitHeslo.Click += new System.EventHandler(this.BtnZmenitHeslo_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 664);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Hlavní menu - Kolínský Majáles";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FlowLayoutPanel flowLayoutPanel1;
        private PictureBox pictureBox1;
        private Label label1;
        private Button BtnTym;
        private Button BtnProgram;
        private Button BtnUkoly;
        private Button BtnZmenitHeslo;
    }
}