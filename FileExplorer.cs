using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Etier.IconHelper;
using System.Diagnostics;

namespace FileExplorer
{
	public partial class FileExplorer : Form
	{

		private System.IO.DirectoryInfo dir;
		public System.IO.DirectoryInfo Dir {
			get {
				return dir;
			}
			set {
				if (value.ToString().Last() == '\\')
				{
					dir = value;
				}
				else
				{
					dir = new System.IO.DirectoryInfo(value.ToString() + @"\");
				}
			}
		}

		private PathList<string> pathList;
		private FileOperator fileOperator;

		public FileExplorer()
		{
			InitializeComponent();

			pathList = new PathList<string>();

			Dir = new System.IO.DirectoryInfo(@"c:\users\Sarunas\Desktop");
			ChangeDirectory(Dir.ToString());

			listView.LargeImageList = imageList;
			listView.SmallImageList = imageList;

			bool largeList = false;
			if (largeList)
			{
				listView.View = View.LargeIcon;
			}
			else
			{
				
				listView.View = View.Details;
			}

			imageList.ImageSize = new Size(32, 32);

			fileOperator = new FileOperator();

			

		}

		private void pathTextBox_Validated(object sender, EventArgs e)
		{

			//Debug.WriteLine(imageList.Container.Components.Count.ToString());

			Stopwatch sw = new Stopwatch();
			sw.Start();

			listView.Clear();

			var columnManager = new ListViewColumnManager(listView);

			columnManager.addColumn("Name", 400);


			ListViewFileItem item;

			listView.BeginUpdate();
			

			foreach (System.IO.FileSystemInfo file in Dir.GetFileSystemInfos())
			{
				string imageKey = file.Name;
				item = new ListViewFileItem(file.Name);
				item.Attributes = file.Attributes;
				if (file.Attributes.HasFlag(System.IO.FileAttributes.Directory))
				{

					Icon iconForFile = IconReader.GetFolderIcon(file.FullName, IconReader.IconSize.Large, IconReader.FolderType.Open);
					imageList.Images.Add(imageKey, iconForFile);          //TODO if Extension unknown
				}
				else
				{
					if (!imageList.Images.ContainsKey(imageKey))
					{
						Icon iconForFile = IconReader.GetFileIcon(file.FullName, IconReader.IconSize.Large, false);
						imageList.Images.Add(imageKey, iconForFile);          //TODO if Extension unknown
					}
				}
				item.ImageKey = imageKey;

				listView.Items.Add(item);
			}

			listView.EndUpdate();

			sw.Stop();
			Debug.WriteLine("Elapsed={0}", sw.Elapsed);

		}

		private void pathTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				pathTextBox_Validated(this, null);	
			}
		}

		private void listView_DoubleClick(object sender, EventArgs e)
		{
			ListView.SelectedListViewItemCollection itemCollection = listView.SelectedItems;
			ListViewFileItem item = (ListViewFileItem)itemCollection[0];

			if (item.Attributes.HasFlag(System.IO.FileAttributes.Directory))
			{
				ChangeDirectory(Dir.ToString() + item.Text + '\\');
			}
			else
			{
				fileOperator.OpenFile(Dir + item.Text);
			}
		}

		public void ChangeDirectory(string path)
		{
			Dir = new System.IO.DirectoryInfo(path);
			Debug.WriteLine("Changing Directory to: " + Dir.ToString());
			pathList.AddNext(path);
			pathTextBox.Text = Dir.ToString();
			pathTextBox_Validated(this, null);
		}

		private void buttonBack_Click(object sender, EventArgs e)
		{
			ChangeDirectory(Dir.Parent.FullName);
		}

		private void FileExplorer_FormClosed(object sender, FormClosedEventArgs e)
		{
			imageList.Dispose();
			listView.Dispose();
		}

		private void listView_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				
			}
		}

		private void buttonUndo_Click(object sender, EventArgs e)
		{
			Dir = new System.IO.DirectoryInfo(pathList.Undo());
			pathTextBox.Text = Dir.ToString();
			pathTextBox_Validated(this, null);
		}

		private void buttonRedo_Click(object sender, EventArgs e)
		{
			Dir = new System.IO.DirectoryInfo(pathList.Redo());
			pathTextBox.Text = Dir.ToString();
			pathTextBox_Validated(this, null);
		}
	}
}
