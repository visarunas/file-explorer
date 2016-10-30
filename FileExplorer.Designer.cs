namespace FileExplorer
{
	partial class FileExplorer
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
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.listView = new System.Windows.Forms.ListView();
			this.pathTextBox = new System.Windows.Forms.TextBox();
			this.buttonLeft = new System.Windows.Forms.Button();
			this.buttonRight = new System.Windows.Forms.Button();
			this.buttonBack = new System.Windows.Forms.Button();
			this.searchTextBox = new System.Windows.Forms.TextBox();
			this.viewListContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.viewListContext.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// listView
			// 
			this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView.ContextMenuStrip = this.viewListContext;
			this.listView.Location = new System.Drawing.Point(12, 57);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(960, 592);
			this.listView.TabIndex = 0;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
			this.listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseClick);
			// 
			// pathTextBox
			// 
			this.pathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pathTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.pathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pathTextBox.Location = new System.Drawing.Point(173, 21);
			this.pathTextBox.Name = "pathTextBox";
			this.pathTextBox.Size = new System.Drawing.Size(505, 29);
			this.pathTextBox.TabIndex = 1;
			this.pathTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pathTextBox_KeyDown);
			this.pathTextBox.Validated += new System.EventHandler(this.pathTextBox_Validated);
			// 
			// buttonLeft
			// 
			this.buttonLeft.Location = new System.Drawing.Point(12, 12);
			this.buttonLeft.Name = "buttonLeft";
			this.buttonLeft.Size = new System.Drawing.Size(47, 38);
			this.buttonLeft.TabIndex = 2;
			this.buttonLeft.Text = "button1";
			this.buttonLeft.UseVisualStyleBackColor = true;
			// 
			// buttonRight
			// 
			this.buttonRight.Location = new System.Drawing.Point(65, 12);
			this.buttonRight.Name = "buttonRight";
			this.buttonRight.Size = new System.Drawing.Size(47, 38);
			this.buttonRight.TabIndex = 2;
			this.buttonRight.Text = "button1";
			this.buttonRight.UseVisualStyleBackColor = true;
			// 
			// buttonBack
			// 
			this.buttonBack.Location = new System.Drawing.Point(120, 12);
			this.buttonBack.Name = "buttonBack";
			this.buttonBack.Size = new System.Drawing.Size(47, 38);
			this.buttonBack.TabIndex = 2;
			this.buttonBack.Text = "back";
			this.buttonBack.UseVisualStyleBackColor = true;
			this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
			// 
			// searchTextBox
			// 
			this.searchTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.searchTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.searchTextBox.ForeColor = System.Drawing.SystemColors.InactiveCaption;
			this.searchTextBox.Location = new System.Drawing.Point(715, 21);
			this.searchTextBox.Name = "searchTextBox";
			this.searchTextBox.Size = new System.Drawing.Size(257, 29);
			this.searchTextBox.TabIndex = 3;
			this.searchTextBox.Text = "Search";
			this.searchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// viewListContext
			// 
			this.viewListContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem});
			this.viewListContext.Name = "viewListContext";
			this.viewListContext.Size = new System.Drawing.Size(103, 70);
			// 
			// copyToolStripMenuItem
			// 
			this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
			this.copyToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this.copyToolStripMenuItem.Text = "Copy";
			// 
			// pasteToolStripMenuItem
			// 
			this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
			this.pasteToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this.pasteToolStripMenuItem.Text = "Paste";
			// 
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this.cutToolStripMenuItem.Text = "Cut";
			// 
			// FileExplorer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(984, 661);
			this.Controls.Add(this.searchTextBox);
			this.Controls.Add(this.buttonBack);
			this.Controls.Add(this.buttonRight);
			this.Controls.Add(this.buttonLeft);
			this.Controls.Add(this.pathTextBox);
			this.Controls.Add(this.listView);
			this.DoubleBuffered = true;
			this.MinimumSize = new System.Drawing.Size(700, 250);
			this.Name = "FileExplorer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "File Explorer";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FileExplorer_FormClosed);
			this.viewListContext.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.ListView listView;
		private System.Windows.Forms.TextBox pathTextBox;
		private System.Windows.Forms.Button buttonLeft;
		private System.Windows.Forms.Button buttonRight;
		private System.Windows.Forms.Button buttonBack;
		private System.Windows.Forms.TextBox searchTextBox;
		private System.Windows.Forms.ContextMenuStrip viewListContext;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
	}
}

