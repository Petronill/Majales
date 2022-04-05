namespace Majales
{
    partial class EventWindow
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.SekceView = new System.Windows.Forms.ListView();
            this.NazevHeader = new System.Windows.Forms.ColumnHeader();
            this.NarocnostHeader = new System.Windows.Forms.ColumnHeader();
            this.StavHeader = new System.Windows.Forms.ColumnHeader();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.JmenoHeader = new System.Windows.Forms.ColumnHeader();
            this.ZacatekHeader = new System.Windows.Forms.ColumnHeader();
            this.KonecHeader = new System.Windows.Forms.ColumnHeader();
            this.MistoHeader = new System.Windows.Forms.ColumnHeader();
            this.ProjektyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NovyProjekt = new System.Windows.Forms.ToolStripMenuItem();
            this.SmazatProjekt = new System.Windows.Forms.ToolStripMenuItem();
            this.SmazProjekty = new System.Windows.Forms.ToolStripMenuItem();
            this.AkceMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NovaAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.SmazAkce = new System.Windows.Forms.ToolStripMenuItem();
            this.SmazatAkci = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.ProjektyMenu.SuspendLayout();
            this.AkceMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.flowLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.flowLayoutPanel2);
            this.splitContainer1.Size = new System.Drawing.Size(982, 703);
            this.splitContainer1.SplitterDistance = 491;
            this.splitContainer1.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.SekceView);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Padding = new System.Windows.Forms.Padding(30);
            this.flowLayoutPanel1.Size = new System.Drawing.Size(491, 703);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(33, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Projekty";
            // 
            // SekceView
            // 
            this.SekceView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SekceView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NazevHeader,
            this.NarocnostHeader,
            this.StavHeader});
            this.SekceView.GridLines = true;
            this.SekceView.Location = new System.Drawing.Point(40, 78);
            this.SekceView.Margin = new System.Windows.Forms.Padding(10);
            this.SekceView.Name = "SekceView";
            this.SekceView.Size = new System.Drawing.Size(405, 580);
            this.SekceView.TabIndex = 1;
            this.SekceView.UseCompatibleStateImageBehavior = false;
            this.SekceView.View = System.Windows.Forms.View.Details;
            // 
            // NazevHeader
            // 
            this.NazevHeader.Text = "Název";
            this.NazevHeader.Width = 200;
            // 
            // NarocnostHeader
            // 
            this.NarocnostHeader.Text = "Náročnost";
            this.NarocnostHeader.Width = 100;
            // 
            // StavHeader
            // 
            this.StavHeader.Text = "Stav";
            this.StavHeader.Width = 100;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.listView1);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(30);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(487, 703);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(33, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 38);
            this.label2.TabIndex = 0;
            this.label2.Text = "Akce";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.JmenoHeader,
            this.ZacatekHeader,
            this.KonecHeader,
            this.MistoHeader});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(40, 78);
            this.listView1.Margin = new System.Windows.Forms.Padding(10);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(405, 580);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // JmenoHeader
            // 
            this.JmenoHeader.Text = "Jméno";
            this.JmenoHeader.Width = 120;
            // 
            // ZacatekHeader
            // 
            this.ZacatekHeader.Text = "Začátek";
            this.ZacatekHeader.Width = 80;
            // 
            // KonecHeader
            // 
            this.KonecHeader.Text = "Konec";
            this.KonecHeader.Width = 80;
            // 
            // MistoHeader
            // 
            this.MistoHeader.Text = "Místo";
            this.MistoHeader.Width = 120;
            // 
            // ProjektyMenu
            // 
            this.ProjektyMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ProjektyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NovyProjekt,
            this.SmazatProjekt,
            this.SmazProjekty});
            this.ProjektyMenu.Name = "contextMenuStrip1";
            this.ProjektyMenu.Size = new System.Drawing.Size(140, 76);
            // 
            // NovyProjekt
            // 
            this.NovyProjekt.Name = "NovyProjekt";
            this.NovyProjekt.Size = new System.Drawing.Size(139, 24);
            this.NovyProjekt.Text = "Nový";
            // 
            // SmazatProjekt
            // 
            this.SmazatProjekt.Name = "SmazatProjekt";
            this.SmazatProjekt.Size = new System.Drawing.Size(139, 24);
            this.SmazatProjekt.Text = "Smazat";
            // 
            // SmazProjekty
            // 
            this.SmazProjekty.Name = "SmazProjekty";
            this.SmazProjekty.Size = new System.Drawing.Size(139, 24);
            this.SmazProjekty.Text = "Smaž vše";
            // 
            // AkceMenu
            // 
            this.AkceMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.AkceMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NovaAkce,
            this.SmazatAkci,
            this.SmazAkce});
            this.AkceMenu.Name = "contextMenuStrip5";
            this.AkceMenu.Size = new System.Drawing.Size(211, 104);
            // 
            // NovaAkce
            // 
            this.NovaAkce.Name = "NovaAkce";
            this.NovaAkce.Size = new System.Drawing.Size(210, 24);
            this.NovaAkce.Text = "Nová";
            // 
            // SmazAkce
            // 
            this.SmazAkce.Name = "SmazAkce";
            this.SmazAkce.Size = new System.Drawing.Size(210, 24);
            this.SmazAkce.Text = "Smaž vše";
            // 
            // SmazatAkci
            // 
            this.SmazatAkci.Name = "SmazatAkci";
            this.SmazatAkci.Size = new System.Drawing.Size(210, 24);
            this.SmazatAkci.Text = "Smazat";
            // 
            // EventWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 703);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "EventWindow";
            this.Text = "Program - Kolínský Majáles";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.ProjektyMenu.ResumeLayout(false);
            this.AkceMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private ListView SekceView;
        private ColumnHeader NazevHeader;
        private ColumnHeader NarocnostHeader;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label label2;
        private ListView listView1;
        private ColumnHeader JmenoHeader;
        private ColumnHeader ZacatekHeader;
        private ColumnHeader KonecHeader;
        private ColumnHeader StavHeader;
        private ColumnHeader MistoHeader;
        private ContextMenuStrip ProjektyMenu;
        private ToolStripMenuItem NovyProjekt;
        private ToolStripMenuItem SmazatProjekt;
        private ToolStripMenuItem SmazProjekty;
        private ContextMenuStrip AkceMenu;
        private ToolStripMenuItem NovaAkce;
        private ToolStripMenuItem SmazAkce;
        private ToolStripMenuItem SmazatAkci;
    }
}