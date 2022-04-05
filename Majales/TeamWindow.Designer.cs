namespace Majales
{
    partial class TeamWindow
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
            this.JmenoHeader = new System.Windows.Forms.ColumnHeader();
            this.EmailHeader = new System.Windows.Forms.ColumnHeader();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.KrestniHeader = new System.Windows.Forms.ColumnHeader();
            this.PrijmeniHeader = new System.Windows.Forms.ColumnHeader();
            this.FunkceHeader = new System.Windows.Forms.ColumnHeader();
            this.NovaSekce = new System.Windows.Forms.ToolStripMenuItem();
            this.SmazatSekci = new System.Windows.Forms.ToolStripMenuItem();
            this.SmazSekce = new System.Windows.Forms.ToolStripMenuItem();
            this.SekceMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ClenoveMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.NovyClen = new System.Windows.Forms.ToolStripMenuItem();
            this.SmazatClena = new System.Windows.Forms.ToolStripMenuItem();
            this.SmazCleny = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.SekceMenu.SuspendLayout();
            this.ClenoveMenu.SuspendLayout();
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
            this.splitContainer1.TabIndex = 0;
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
            this.label1.Size = new System.Drawing.Size(90, 38);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sekce";
            // 
            // SekceView
            // 
            this.SekceView.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SekceView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.JmenoHeader,
            this.EmailHeader});
            this.SekceView.GridLines = true;
            this.SekceView.Location = new System.Drawing.Point(40, 78);
            this.SekceView.Margin = new System.Windows.Forms.Padding(10);
            this.SekceView.Name = "SekceView";
            this.SekceView.Size = new System.Drawing.Size(405, 580);
            this.SekceView.TabIndex = 1;
            this.SekceView.UseCompatibleStateImageBehavior = false;
            this.SekceView.View = System.Windows.Forms.View.Details;
            // 
            // JmenoHeader
            // 
            this.JmenoHeader.Text = "Jméno";
            this.JmenoHeader.Width = 150;
            // 
            // EmailHeader
            // 
            this.EmailHeader.Text = "E-mail";
            this.EmailHeader.Width = 250;
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
            this.label2.Size = new System.Drawing.Size(118, 38);
            this.label2.TabIndex = 0;
            this.label2.Text = "Členové";
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.KrestniHeader,
            this.PrijmeniHeader,
            this.FunkceHeader});
            this.listView1.GridLines = true;
            this.listView1.Location = new System.Drawing.Point(40, 78);
            this.listView1.Margin = new System.Windows.Forms.Padding(10);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(405, 580);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // KrestniHeader
            // 
            this.KrestniHeader.Text = "Křestní";
            this.KrestniHeader.Width = 120;
            // 
            // PrijmeniHeader
            // 
            this.PrijmeniHeader.Text = "Příjmení";
            this.PrijmeniHeader.Width = 150;
            // 
            // FunkceHeader
            // 
            this.FunkceHeader.Text = "Funkce";
            this.FunkceHeader.Width = 130;
            // 
            // NovaSekce
            // 
            this.NovaSekce.Name = "NovaSekce";
            this.NovaSekce.Size = new System.Drawing.Size(139, 24);
            this.NovaSekce.Text = "Nová";
            // 
            // SmazatSekci
            // 
            this.SmazatSekci.Name = "SmazatSekci";
            this.SmazatSekci.Size = new System.Drawing.Size(139, 24);
            this.SmazatSekci.Text = "Smazat";
            // 
            // SmazSekce
            // 
            this.SmazSekce.Name = "SmazSekce";
            this.SmazSekce.Size = new System.Drawing.Size(139, 24);
            this.SmazSekce.Text = "Smaž vše";
            // 
            // SekceMenu
            // 
            this.SekceMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.SekceMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NovaSekce,
            this.SmazatSekci,
            this.SmazSekce});
            this.SekceMenu.Name = "contextMenuStrip1";
            this.SekceMenu.Size = new System.Drawing.Size(140, 76);
            // 
            // ClenoveMenu
            // 
            this.ClenoveMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ClenoveMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NovyClen,
            this.SmazatClena,
            this.SmazCleny});
            this.ClenoveMenu.Name = "ClenoveMenu";
            this.ClenoveMenu.Size = new System.Drawing.Size(140, 76);
            // 
            // NovyClen
            // 
            this.NovyClen.Name = "NovyClen";
            this.NovyClen.Size = new System.Drawing.Size(139, 24);
            this.NovyClen.Text = "Nový";
            // 
            // SmazatClena
            // 
            this.SmazatClena.Name = "SmazatClena";
            this.SmazatClena.Size = new System.Drawing.Size(210, 24);
            this.SmazatClena.Text = "Smazat";
            // 
            // SmazCleny
            // 
            this.SmazCleny.Name = "SmazCleny";
            this.SmazCleny.Size = new System.Drawing.Size(210, 24);
            this.SmazCleny.Text = "Smaž vše";
            // 
            // TeamWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(982, 703);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Name = "TeamWindow";
            this.Text = "Tým - Kolínský Majáles";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.SekceMenu.ResumeLayout(false);
            this.ClenoveMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private SplitContainer splitContainer1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private ListView SekceView;
        private ColumnHeader JmenoHeader;
        private ColumnHeader EmailHeader;
        private FlowLayoutPanel flowLayoutPanel2;
        private Label label2;
        private ListView listView1;
        private ColumnHeader KrestniHeader;
        private ColumnHeader PrijmeniHeader;
        private ColumnHeader FunkceHeader;
        private ToolStripMenuItem NovaSekce;
        private ToolStripMenuItem SmazatSekci;
        private ToolStripMenuItem SmazSekce;
        private ContextMenuStrip SekceMenu;
        private ContextMenuStrip ClenoveMenu;
        private ToolStripMenuItem NovyClen;
        private ToolStripMenuItem SmazatClena;
        private ToolStripMenuItem SmazCleny;
    }
}