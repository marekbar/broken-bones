namespace BoneAnalyser
{
    partial class forma
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(forma));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.menu = new System.Windows.Forms.MenuStrip();
            this.programToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuOpenFromFile = new System.Windows.Forms.ToolStripMenuItem();
            this.menuCloseApp = new System.Windows.Forms.ToolStripMenuItem();
            this.menuInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.samplesParent = new System.Windows.Forms.ToolStripMenuItem();
            this.objaśnieniaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jakToDziałaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jakZacząćUżywaćProgramToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pobierzOpisProjektuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.status = new System.Windows.Forms.ToolStripStatusLabel();
            this.progress = new System.Windows.Forms.ToolStripProgressBar();
            this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmGrayscale = new System.Windows.Forms.ToolStripMenuItem();
            this.cmThreshold = new System.Windows.Forms.ToolStripMenuItem();
            this.cmContrastRepair = new System.Windows.Forms.ToolStripMenuItem();
            this.cmBones = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.imageLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.picture2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.picture = new System.Windows.Forms.PictureBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
            this.stronaProjektuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.contextMenu.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picture2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programToolStripMenuItem,
            this.samplesParent,
            this.objaśnieniaToolStripMenuItem});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(984, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menuStrip1";
            // 
            // programToolStripMenuItem
            // 
            this.programToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuOpenFromFile,
            this.menuCloseApp,
            this.menuInfo,
            this.stronaProjektuToolStripMenuItem});
            this.programToolStripMenuItem.Image = global::BoneAnalyser.Properties.Resources._2_Apps_icon;
            this.programToolStripMenuItem.Name = "programToolStripMenuItem";
            this.programToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.programToolStripMenuItem.Text = "Program";
            // 
            // menuOpenFromFile
            // 
            this.menuOpenFromFile.Image = global::BoneAnalyser.Properties.Resources.Document_Blank_icon;
            this.menuOpenFromFile.Name = "menuOpenFromFile";
            this.menuOpenFromFile.Size = new System.Drawing.Size(188, 22);
            this.menuOpenFromFile.Text = "Otwórz obraz z pliku";
            this.menuOpenFromFile.Click += new System.EventHandler(this.menuOpenFromFile_Click);
            // 
            // menuCloseApp
            // 
            this.menuCloseApp.Image = global::BoneAnalyser.Properties.Resources.logout_icon;
            this.menuCloseApp.Name = "menuCloseApp";
            this.menuCloseApp.Size = new System.Drawing.Size(188, 22);
            this.menuCloseApp.Text = "Zamknij";
            this.menuCloseApp.Click += new System.EventHandler(this.menuCloseApp_Click);
            // 
            // menuInfo
            // 
            this.menuInfo.Image = global::BoneAnalyser.Properties.Resources.Alarm_Info_icon;
            this.menuInfo.Name = "menuInfo";
            this.menuInfo.Size = new System.Drawing.Size(188, 22);
            this.menuInfo.Text = "Infomacje o projekcie";
            this.menuInfo.Click += new System.EventHandler(this.menuInfo_Click);
            // 
            // samplesParent
            // 
            this.samplesParent.Image = global::BoneAnalyser.Properties.Resources.bone_icon;
            this.samplesParent.Name = "samplesParent";
            this.samplesParent.Size = new System.Drawing.Size(249, 20);
            this.samplesParent.Text = "Przykładowe obrazy rentgenowskie kości";
            // 
            // objaśnieniaToolStripMenuItem
            // 
            this.objaśnieniaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.jakToDziałaToolStripMenuItem,
            this.jakZacząćUżywaćProgramToolStripMenuItem,
            this.pobierzOpisProjektuToolStripMenuItem});
            this.objaśnieniaToolStripMenuItem.Name = "objaśnieniaToolStripMenuItem";
            this.objaśnieniaToolStripMenuItem.Size = new System.Drawing.Size(81, 20);
            this.objaśnieniaToolStripMenuItem.Text = "Objaśnienia";
            // 
            // jakToDziałaToolStripMenuItem
            // 
            this.jakToDziałaToolStripMenuItem.Name = "jakToDziałaToolStripMenuItem";
            this.jakToDziałaToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.jakToDziałaToolStripMenuItem.Text = "Jak to działa?";
            this.jakToDziałaToolStripMenuItem.Click += new System.EventHandler(this.jakToDziałaToolStripMenuItem_Click);
            // 
            // jakZacząćUżywaćProgramToolStripMenuItem
            // 
            this.jakZacząćUżywaćProgramToolStripMenuItem.Name = "jakZacząćUżywaćProgramToolStripMenuItem";
            this.jakZacząćUżywaćProgramToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.jakZacząćUżywaćProgramToolStripMenuItem.Text = "Jak zacząć używać program?";
            this.jakZacząćUżywaćProgramToolStripMenuItem.Click += new System.EventHandler(this.jakZacząćUżywaćProgramToolStripMenuItem_Click);
            // 
            // pobierzOpisProjektuToolStripMenuItem
            // 
            this.pobierzOpisProjektuToolStripMenuItem.Name = "pobierzOpisProjektuToolStripMenuItem";
            this.pobierzOpisProjektuToolStripMenuItem.Size = new System.Drawing.Size(223, 22);
            this.pobierzOpisProjektuToolStripMenuItem.Text = "Pobierz opis projektu";
            this.pobierzOpisProjektuToolStripMenuItem.Click += new System.EventHandler(this.pobierzOpisProjektuToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status,
            this.progress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 439);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(984, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // status
            // 
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 17);
            // 
            // progress
            // 
            this.progress.BackColor = System.Drawing.Color.Black;
            this.progress.ForeColor = System.Drawing.Color.Gold;
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(100, 16);
            this.progress.Visible = false;
            // 
            // contextMenu
            // 
            this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmGrayscale,
            this.cmThreshold,
            this.cmContrastRepair,
            this.cmBones});
            this.contextMenu.Name = "contextMenu";
            this.contextMenu.Size = new System.Drawing.Size(221, 92);
            // 
            // cmGrayscale
            // 
            this.cmGrayscale.Name = "cmGrayscale";
            this.cmGrayscale.Size = new System.Drawing.Size(220, 22);
            this.cmGrayscale.Text = "Konwersja na  skalę szarości";
            this.cmGrayscale.Visible = false;
            this.cmGrayscale.Click += new System.EventHandler(this.cmGrayscale_Click);
            // 
            // cmThreshold
            // 
            this.cmThreshold.Name = "cmThreshold";
            this.cmThreshold.Size = new System.Drawing.Size(220, 22);
            this.cmThreshold.Text = "Progowanie";
            this.cmThreshold.Visible = false;
            this.cmThreshold.Click += new System.EventHandler(this.cmThreshold_Click);
            // 
            // cmContrastRepair
            // 
            this.cmContrastRepair.Name = "cmContrastRepair";
            this.cmContrastRepair.Size = new System.Drawing.Size(220, 22);
            this.cmContrastRepair.Text = "Popraw kontrast";
            this.cmContrastRepair.Visible = false;
            this.cmContrastRepair.Click += new System.EventHandler(this.cmContrastRepair_Click);
            // 
            // cmBones
            // 
            this.cmBones.Name = "cmBones";
            this.cmBones.Size = new System.Drawing.Size(220, 22);
            this.cmBones.Text = "Znajdź kości";
            this.cmBones.Click += new System.EventHandler(this.cmBones_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.DarkGray;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.imageLabel);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.picture2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.picture);
            this.splitContainer1.Size = new System.Drawing.Size(984, 415);
            this.splitContainer1.SplitterDistance = 495;
            this.splitContainer1.TabIndex = 2;
            // 
            // imageLabel
            // 
            this.imageLabel.AutoSize = true;
            this.imageLabel.Location = new System.Drawing.Point(3, 36);
            this.imageLabel.Name = "imageLabel";
            this.imageLabel.Size = new System.Drawing.Size(0, 13);
            this.imageLabel.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label1.Size = new System.Drawing.Size(73, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "ORYGINAŁ";
            // 
            // picture2
            // 
            this.picture2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picture2.Image = global::BoneAnalyser.Properties.Resources.Skeleton_icon;
            this.picture2.Location = new System.Drawing.Point(0, 0);
            this.picture2.Name = "picture2";
            this.picture2.Size = new System.Drawing.Size(495, 415);
            this.picture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picture2.TabIndex = 0;
            this.picture2.TabStop = false;
            this.picture2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picture2_MouseDown);
            this.picture2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picture2_MouseMove);
            this.picture2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picture2_MouseUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(10, 10, 0, 0);
            this.label2.Size = new System.Drawing.Size(110, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "PRZETWORZONE";
            // 
            // picture
            // 
            this.picture.ContextMenuStrip = this.contextMenu;
            this.picture.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picture.Image = global::BoneAnalyser.Properties.Resources.Skeleton_icon;
            this.picture.InitialImage = global::BoneAnalyser.Properties.Resources.Skeleton_icon;
            this.picture.Location = new System.Drawing.Point(0, 0);
            this.picture.Name = "picture";
            this.picture.Size = new System.Drawing.Size(485, 415);
            this.picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picture.TabIndex = 0;
            this.picture.TabStop = false;
            this.picture.DoubleClick += new System.EventHandler(this.picture_DoubleClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripButton5});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(984, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = global::BoneAnalyser.Properties.Resources.logout_icon;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(70, 22);
            this.toolStripButton1.Text = "Zamknij";
            this.toolStripButton1.Click += new System.EventHandler(this.menuCloseApp_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::BoneAnalyser.Properties.Resources.Document_Blank_icon;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(137, 22);
            this.toolStripButton2.Text = "Wczytaj obraz z pliku";
            this.toolStripButton2.Click += new System.EventHandler(this.menuOpenFromFile_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::BoneAnalyser.Properties.Resources.Alarm_Info_icon;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(84, 22);
            this.toolStripButton3.Text = "Informacje";
            this.toolStripButton3.Click += new System.EventHandler(this.menuInfo_Click);
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = global::BoneAnalyser.Properties.Resources.bone_icon;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(92, 22);
            this.toolStripButton4.Text = "Znajdź kości";
            this.toolStripButton4.Click += new System.EventHandler(this.cmBones_Click);
            // 
            // toolStripButton5
            // 
            this.toolStripButton5.Image = global::BoneAnalyser.Properties.Resources.Actions_document_save_icon;
            this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton5.Name = "toolStripButton5";
            this.toolStripButton5.Size = new System.Drawing.Size(60, 22);
            this.toolStripButton5.Text = "Zapisz";
            this.toolStripButton5.Click += new System.EventHandler(this.toolStripButton5_Click);
            // 
            // stronaProjektuToolStripMenuItem
            // 
            this.stronaProjektuToolStripMenuItem.Name = "stronaProjektuToolStripMenuItem";
            this.stronaProjektuToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.stronaProjektuToolStripMenuItem.Text = "Strona projektu";
            this.stronaProjektuToolStripMenuItem.Click += new System.EventHandler(this.stronaProjektuToolStripMenuItem_Click);
            // 
            // forma
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(984, 461);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menu);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menu;
            this.MinimumSize = new System.Drawing.Size(1000, 500);
            this.Name = "forma";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kość";
            this.Load += new System.EventHandler(this.forma_Load);
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.contextMenu.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picture2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem programToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuCloseApp;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel status;
        private System.Windows.Forms.ToolStripProgressBar progress;
        private System.Windows.Forms.ToolStripMenuItem menuOpenFromFile;
        private System.Windows.Forms.ContextMenuStrip contextMenu;
        private System.Windows.Forms.ToolStripMenuItem cmGrayscale;
        private System.Windows.Forms.ToolStripMenuItem cmThreshold;
        private System.Windows.Forms.ToolStripMenuItem cmBones;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.PictureBox picture2;
        private System.Windows.Forms.PictureBox picture;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem menuInfo;
        private System.Windows.Forms.ToolStripMenuItem cmContrastRepair;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripButton toolStripButton5;
        private System.Windows.Forms.ToolStripMenuItem samplesParent;
        private System.Windows.Forms.Label imageLabel;
        private System.Windows.Forms.ToolStripMenuItem objaśnieniaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jakToDziałaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jakZacząćUżywaćProgramToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pobierzOpisProjektuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stronaProjektuToolStripMenuItem;
    }
}

