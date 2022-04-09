namespace Majales
{
    partial class TaskWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskWindow));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.SekceView = new System.Windows.Forms.ListView();
            this.NazevHeader = new System.Windows.Forms.ColumnHeader();
            this.ZadavatelHeader = new System.Windows.Forms.ColumnHeader();
            this.UzaverkaHeader = new System.Windows.Forms.ColumnHeader();
            this.PrioritaHeader = new System.Windows.Forms.ColumnHeader();
            this.StavHeader = new System.Windows.Forms.ColumnHeader();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.AutorHeader = new System.Windows.Forms.ColumnHeader();
            this.DatumHeader = new System.Windows.Forms.ColumnHeader();
            this.NahledHeader = new System.Windows.Forms.ColumnHeader();
            this.UkolyMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NovyUkol = new System.Windows.Forms.ToolStripMenuItem();
            this.FiltrovatUkoly = new System.Windows.Forms.ToolStripMenuItem();
            this.SmazatUkol = new System.Windows.Forms.ToolStripMenuItem();
            this.SmazUkoly = new System.Windows.Forms.ToolStripMenuItem();
            this.KomentareMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NovyKomentar = new System.Windows.Forms.ToolStripMenuItem();
            this.FiltrovatKomentare = new System.Windows.Forms.ToolStripMenuItem();
            this.SmazatKomentar = new System.Windows.Forms.ToolStripMenuItem();
            this.SmazKomentare = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.UkolyMenu.SuspendLayout();
            this.KomentareMenu.SuspendLayout();
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
            this.splitContainer1.Size = new System.Drawing.Size(1182, 703);
            this.splitContainer1.SplitterDistance = 591;
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
            this.flowLayoutPanel1.Size = new System.Drawing.Size(591, 703);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(33, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Úkoly";
            // 
            // SekceView
            // 
            this.SekceView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SekceView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NazevHeader,
            this.ZadavatelHeader,
            this.UzaverkaHeader,
            this.PrioritaHeader,
            this.StavHeader});
            this.SekceView.ContextMenuStrip = this.UkolyMenu;
            this.SekceView.GridLines = true;
            this.SekceView.Location = new System.Drawing.Point(40, 78);
            this.SekceView.Margin = new System.Windows.Forms.Padding(10);
            this.SekceView.Name = "SekceView";
            this.SekceView.Size = new System.Drawing.Size(506, 580);
            this.SekceView.TabIndex = 1;
            this.SekceView.UseCompatibleStateImageBehavior = false;
            this.SekceView.View = System.Windows.Forms.View.Details;
            // 
            // NazevHeader
            // 
            this.NazevHeader.Text = "Název";
            this.NazevHeader.Width = 120;
            // 
            // ZadavatelHeader
            // 
            this.ZadavatelHeader.Text = "Zadavatel";
            this.ZadavatelHeader.Width = 140;
            // 
            // UzaverkaHeader
            // 
            this.UzaverkaHeader.Text = "Uzávěrka";
            this.UzaverkaHeader.Width = 80;
            // 
            // PrioritaHeader
            // 
            this.PrioritaHeader.Text = "Priorita";
            this.PrioritaHeader.Width = 80;
            // 
            // StavHeader
            // 
            this.StavHeader.Text = "Stav";
            this.StavHeader.Width = 80;
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
            this.flowLayoutPanel2.Size = new System.Drawing.Size(587, 703);
            this.flowLayoutPanel2.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(33, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 38);
            this.label2.TabIndex = 0;
            this.label2.Text = "Komentáře";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AutorHeader,
            this.DatumHeader,
            this.NahledHeader});
            this.listView1.ContextMenuStrip = this.KomentareMenu;
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(40, 78);
            this.listView1.Margin = new System.Windows.Forms.Padding(10);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(505, 580);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // AutorHeader
            // 
            this.AutorHeader.Text = "Autor";
            this.AutorHeader.Width = 120;
            // 
            // DatumHeader
            // 
            this.DatumHeader.Text = "Datum";
            this.DatumHeader.Width = 80;
            // 
            // NahledHeader
            // 
            this.NahledHeader.Text = "Náhled";
            this.NahledHeader.Width = 300;
            // 
            // UkolyMenu
            // 
            this.UkolyMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.UkolyMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NovyUkol,
            this.FiltrovatUkoly,
            this.SmazatUkol,
            this.SmazUkoly});
            this.UkolyMenu.Name = "contextMenuStrip1";
            this.UkolyMenu.Size = new System.Drawing.Size(140, 100);
            // 
            // NovyUkol
            // 
            this.NovyUkol.Name = "NovyUkol";
            this.NovyUkol.Size = new System.Drawing.Size(139, 24);
            this.NovyUkol.Text = "Nový";
            // 
            // FiltrovatUkoly
            // 
            this.FiltrovatUkoly.Name = "FiltrovatUkoly";
            this.FiltrovatUkoly.Size = new System.Drawing.Size(139, 24);
            this.FiltrovatUkoly.Text = "Filtrovat";
            // 
            // SmazatUkol
            // 
            this.SmazatUkol.Name = "SmazatUkol";
            this.SmazatUkol.Size = new System.Drawing.Size(139, 24);
            this.SmazatUkol.Text = "Smazat";
            // 
            // SmazUkoly
            // 
            this.SmazUkoly.Name = "SmazUkoly";
            this.SmazUkoly.Size = new System.Drawing.Size(139, 24);
            this.SmazUkoly.Text = "Smaž vše";
            // 
            // KomentareMenu
            // 
            this.KomentareMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.KomentareMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NovyKomentar,
            this.FiltrovatKomentare,
            this.SmazatKomentar,
            this.SmazKomentare});
            this.KomentareMenu.Name = "KomentareMenu";
            this.KomentareMenu.Size = new System.Drawing.Size(140, 100);
            // 
            // NovyKomentar
            // 
            this.NovyKomentar.Name = "NovyKomentar";
            this.NovyKomentar.Size = new System.Drawing.Size(139, 24);
            this.NovyKomentar.Text = "Nový";
            // 
            // FiltrovatKomentare
            // 
            this.FiltrovatKomentare.Name = "FiltrovatKomentare";
            this.FiltrovatKomentare.Size = new System.Drawing.Size(139, 24);
            this.FiltrovatKomentare.Text = "Filtrovat";
            // 
            // SmazatKomentar
            // 
            this.SmazatKomentar.Name = "SmazatKomentar";
            this.SmazatKomentar.Size = new System.Drawing.Size(139, 24);
            this.SmazatKomentar.Text = "Smazat";
            // 
            // SmazKomentare
            // 
            this.SmazKomentare.Name = "SmazKomentare";
            this.SmazKomentare.Size = new System.Drawing.Size(139, 24);
            this.SmazKomentare.Text = "Smaž vše";
            // 
            // TaskWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 703);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TaskWindow";
            this.Text = "Úkoly - Kolínský Majáles";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.UkolyMenu.ResumeLayout(false);
            this.KomentareMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private ListView SekceView;
        private ColumnHeader NazevHeader;
        private ColumnHeader ZadavatelHeader;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label label2;
        private ListView listView1;
        private ColumnHeader AutorHeader;
        private ColumnHeader DatumHeader;
        private ColumnHeader NahledHeader;
        private ColumnHeader UzaverkaHeader;
        private ColumnHeader PrioritaHeader;
        private ColumnHeader StavHeader;
        private ContextMenuStrip UkolyMenu;
        private ToolStripMenuItem NovyUkol;
        private ToolStripMenuItem SmazatUkol;
        private ToolStripMenuItem SmazUkoly;
        private ContextMenuStrip KomentareMenu;
        private ToolStripMenuItem NovyKomentar;
        private ToolStripMenuItem SmazatKomentar;
        private ToolStripMenuItem SmazKomentare;
        private ToolStripMenuItem FiltrovatUkoly;
        private ToolStripMenuItem FiltrovatKomentare;
    }
}