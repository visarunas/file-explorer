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
			this.viewListContext = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pathTextBox = new System.Windows.Forms.TextBox();
			this.buttonUndo = new System.Windows.Forms.Button();
			this.buttonRedo = new System.Windows.Forms.Button();
			this.buttonBack = new System.Windows.Forms.Button();
			this.searchTextBox = new System.Windows.Forms.TextBox();
			this.listView = new ListViewNF();
			this.viewListContext.SuspendLayout();
			this.SuspendLayout();
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(32, 32);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
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
			// cutToolStripMenuItem
			// 
			this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
			this.cutToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
			this.cutToolStripMenuItem.Text = "Cut";
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
			this.pathTextBox.TabIndex = 4;
			this.pathTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.pathTextBox_KeyDown);
			this.pathTextBox.Validated += new System.EventHandler(this.pathTextBox_Validated);
			// 
			// buttonUndo
			// 
			this.buttonUndo.Location = new System.Drawing.Point(12, 12);
			this.buttonUndo.Name = "buttonUndo";
			this.buttonUndo.Size = new System.Drawing.Size(47, 38);
			this.buttonUndo.TabIndex = 2;
			this.buttonUndo.Text = "<<";
			this.buttonUndo.UseVisualStyleBackColor = true;
			this.buttonUndo.Click += new System.EventHandler(this.buttonUndo_Click);
			// 
			// buttonRedo
			// 
			this.buttonRedo.Location = new System.Drawing.Point(65, 12);
			this.buttonRedo.Name = "buttonRedo";
			this.buttonRedo.Size = new System.Drawing.Size(47, 38);
			this.buttonRedo.TabIndex = 2;
			this.buttonRedo.Text = ">>";
			this.buttonRedo.UseVisualStyleBackColor = true;
			this.buttonRedo.Click += new System.EventHandler(this.buttonRedo_Click);
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
			this.searchTextBox.ForeColor = System.Drawing.Color.DarkGray;
			this.searchTextBox.Location = new System.Drawing.Point(715, 21);
			this.searchTextBox.Name = "searchTextBox";
			this.searchTextBox.Size = new System.Drawing.Size(257, 29);
			this.searchTextBox.TabIndex = 3;
			this.searchTextBox.Text = "Search";
			this.searchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.searchTextBox.Validated += new System.EventHandler(this.searchTextBox_Validated);
			// 
			// listView
			// 
			this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView.Location = new System.Drawing.Point(12, 56);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(960, 593);
			this.listView.TabIndex = 1;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
			// 
			// FileExplorer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(984, 661);
			this.Controls.Add(this.listView);
			this.Controls.Add(this.searchTextBox);
			this.Controls.Add(this.buttonBack);
			this.Controls.Add(this.buttonRedo);
			this.Controls.Add(this.buttonUndo);
			this.Controls.Add(this.pathTextBox);
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
		private System.Windows.Forms.TextBox pathTextBox;
		private System.Windows.Forms.Button buttonUndo;
		private System.Windows.Forms.Button buttonRedo;
		private System.Windows.Forms.Button buttonBack;
		private System.Windows.Forms.TextBox searchTextBox;
		private System.Windows.Forms.ContextMenuStrip viewListContext;
		private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
		private ListViewNF listView;
	}
}

