namespace AnatomyLabeller
{
    partial class frmMain
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
            this.mnuToolBar = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createNewFlashCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshCardListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lstCardList = new System.Windows.Forms.ListBox();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.txtAnswer = new System.Windows.Forms.TextBox();
            this.lblLabelID = new System.Windows.Forms.Label();
            this.picResult = new System.Windows.Forms.PictureBox();
            this.btnResetAnswers = new System.Windows.Forms.Button();
            this.btnSubmitAnswer = new System.Windows.Forms.Button();
            this.lblCorrectAnswer = new System.Windows.Forms.Label();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).BeginInit();
            this.SuspendLayout();
            // 
            // mnuToolBar
            // 
            this.mnuToolBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.mnuToolBar.Location = new System.Drawing.Point(0, 0);
            this.mnuToolBar.Name = "mnuToolBar";
            this.mnuToolBar.Size = new System.Drawing.Size(1432, 28);
            this.mnuToolBar.TabIndex = 1;
            this.mnuToolBar.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewFlashCardToolStripMenuItem,
            this.refreshCardListToolStripMenuItem,
            this.saveListToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.loadListToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // createNewFlashCardToolStripMenuItem
            // 
            this.createNewFlashCardToolStripMenuItem.Name = "createNewFlashCardToolStripMenuItem";
            this.createNewFlashCardToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.createNewFlashCardToolStripMenuItem.Text = "Create New Flash Card";
            this.createNewFlashCardToolStripMenuItem.Click += new System.EventHandler(this.createNewFlashCardToolStripMenuItem_Click);
            // 
            // refreshCardListToolStripMenuItem
            // 
            this.refreshCardListToolStripMenuItem.Name = "refreshCardListToolStripMenuItem";
            this.refreshCardListToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.refreshCardListToolStripMenuItem.Text = "Refresh Card List";
            // 
            // saveListToolStripMenuItem
            // 
            this.saveListToolStripMenuItem.Name = "saveListToolStripMenuItem";
            this.saveListToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.saveListToolStripMenuItem.Text = "Save List";
            this.saveListToolStripMenuItem.Click += new System.EventHandler(this.saveListToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.saveAsToolStripMenuItem.Text = "Save As";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // loadListToolStripMenuItem
            // 
            this.loadListToolStripMenuItem.Name = "loadListToolStripMenuItem";
            this.loadListToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.loadListToolStripMenuItem.Text = "Load List";
            this.loadListToolStripMenuItem.Click += new System.EventHandler(this.loadListToolStripMenuItem_Click);
            // 
            // lstCardList
            // 
            this.lstCardList.FormattingEnabled = true;
            this.lstCardList.ItemHeight = 16;
            this.lstCardList.Location = new System.Drawing.Point(12, 56);
            this.lstCardList.Name = "lstCardList";
            this.lstCardList.Size = new System.Drawing.Size(359, 436);
            this.lstCardList.TabIndex = 2;
            this.lstCardList.SelectedIndexChanged += new System.EventHandler(this.lstCardList_SelectedIndexChanged);
            this.lstCardList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstCardList_KeyDown);
            this.lstCardList.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lstCardList_KeyPress);
            // 
            // txtAnswer
            // 
            this.txtAnswer.Location = new System.Drawing.Point(937, 98);
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.Size = new System.Drawing.Size(248, 22);
            this.txtAnswer.TabIndex = 3;
            // 
            // lblLabelID
            // 
            this.lblLabelID.AutoSize = true;
            this.lblLabelID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLabelID.Location = new System.Drawing.Point(945, 68);
            this.lblLabelID.Name = "lblLabelID";
            this.lblLabelID.Size = new System.Drawing.Size(67, 20);
            this.lblLabelID.TabIndex = 4;
            this.lblLabelID.Text = "Label: ";
            // 
            // picResult
            // 
            this.picResult.Image = global::AnatomyLabeller.Properties.Resources.Neutral;
            this.picResult.Location = new System.Drawing.Point(1038, 234);
            this.picResult.Name = "picResult";
            this.picResult.Size = new System.Drawing.Size(62, 61);
            this.picResult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picResult.TabIndex = 5;
            this.picResult.TabStop = false;
            // 
            // btnResetAnswers
            // 
            this.btnResetAnswers.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnResetAnswers.Location = new System.Drawing.Point(1028, 430);
            this.btnResetAnswers.Name = "btnResetAnswers";
            this.btnResetAnswers.Size = new System.Drawing.Size(105, 74);
            this.btnResetAnswers.TabIndex = 6;
            this.btnResetAnswers.Text = "Reset Answers";
            this.btnResetAnswers.UseVisualStyleBackColor = true;
            this.btnResetAnswers.Click += new System.EventHandler(this.btnResetAnswers_Click);
            // 
            // btnSubmitAnswer
            // 
            this.btnSubmitAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSubmitAnswer.Location = new System.Drawing.Point(1000, 138);
            this.btnSubmitAnswer.Name = "btnSubmitAnswer";
            this.btnSubmitAnswer.Size = new System.Drawing.Size(133, 60);
            this.btnSubmitAnswer.TabIndex = 7;
            this.btnSubmitAnswer.Text = "Submit Answer";
            this.btnSubmitAnswer.UseVisualStyleBackColor = true;
            this.btnSubmitAnswer.Click += new System.EventHandler(this.btnSubmitAnswer_Click);
            // 
            // lblCorrectAnswer
            // 
            this.lblCorrectAnswer.AutoSize = true;
            this.lblCorrectAnswer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCorrectAnswer.Location = new System.Drawing.Point(915, 343);
            this.lblCorrectAnswer.Name = "lblCorrectAnswer";
            this.lblCorrectAnswer.Size = new System.Drawing.Size(168, 25);
            this.lblCorrectAnswer.TabIndex = 8;
            this.lblCorrectAnswer.Text = "Correct Answer:";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(1432, 755);
            this.Controls.Add(this.lblCorrectAnswer);
            this.Controls.Add(this.btnSubmitAnswer);
            this.Controls.Add(this.btnResetAnswers);
            this.Controls.Add(this.picResult);
            this.Controls.Add(this.lblLabelID);
            this.Controls.Add(this.txtAnswer);
            this.Controls.Add(this.lstCardList);
            this.Controls.Add(this.mnuToolBar);
            this.MainMenuStrip = this.mnuToolBar;
            this.Name = "frmMain";
            this.Text = "Flash Card Maker";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMain_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseClick);
            this.mnuToolBar.ResumeLayout(false);
            this.mnuToolBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResult)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mnuToolBar;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem createNewFlashCardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshCardListToolStripMenuItem;
        private System.Windows.Forms.ListBox lstCardList;
        private System.Windows.Forms.ToolStripMenuItem saveListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadListToolStripMenuItem;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.TextBox txtAnswer;
        private System.Windows.Forms.Label lblLabelID;
        private System.Windows.Forms.PictureBox picResult;
        private System.Windows.Forms.Button btnResetAnswers;
        private System.Windows.Forms.Button btnSubmitAnswer;
        private System.Windows.Forms.Label lblCorrectAnswer;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
    }
}

