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
			this.SuspendLayout();
			// 
			// imageList
			// 
			this.imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList.ImageSize = new System.Drawing.Size(70, 70);
			this.imageList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// listView
			// 
			this.listView.Location = new System.Drawing.Point(12, 57);
			this.listView.Name = "listView";
			this.listView.Size = new System.Drawing.Size(960, 592);
			this.listView.TabIndex = 0;
			this.listView.UseCompatibleStateImageBehavior = false;
			this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
			// 
			// pathTextBox
			// 
			this.pathTextBox.BackColor = System.Drawing.SystemColors.ButtonHighlight;
			this.pathTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.pathTextBox.Location = new System.Drawing.Point(173, 21);
			this.pathTextBox.Name = "pathTextBox";
			this.pathTextBox.Size = new System.Drawing.Size(757, 29);
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
			// FileExplorer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(984, 661);
			this.Controls.Add(this.buttonBack);
			this.Controls.Add(this.buttonRight);
			this.Controls.Add(this.buttonLeft);
			this.Controls.Add(this.pathTextBox);
			this.Controls.Add(this.listView);
			this.DoubleBuffered = true;
			this.MinimumSize = new System.Drawing.Size(500, 250);
			this.Name = "FileExplorer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "File Explorer";
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
	}
}

