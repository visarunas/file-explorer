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
					value = dir;
				}
				else
				{
					dir = new System.IO.DirectoryInfo(value.ToString() + @"\");
				}
			}
		}
				
		private FileOperator fileOperator;

		public FileExplorer()
		{
			InitializeComponent();

			Dir = new System.IO.DirectoryInfo(@"c:\users");
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

			
			//pathTextBox_Validated(this, null);
			//pathTextBox.Validated += new EventHandler(pathTextBox_Validated);

			fileOperator = new FileOperator();



		}

		private void pathTextBox_Validated(object sender, EventArgs e)
		{
			Stopwatch sw = new Stopwatch();
			sw.Start();

			Dir = new System.IO.DirectoryInfo(pathTextBox.Text);
			Console.WriteLine(Dir.ToString());
			listView.Clear();

			var cm = new ColumnHeader();
			cm.Text = "Name";
			cm.Width = 300;
			imageList.ImageSize = new Size(20, 20);
			listView.Columns.Add(cm);

			ListViewFileItem item;

			listView.BeginUpdate();

			foreach (System.IO.FileSystemInfo file in Dir.GetFileSystemInfos())
			{
				item = new ListViewFileItem(file.Name);
				item.Attributes = file.Attributes;
				if (file.Attributes.HasFlag(System.IO.FileAttributes.Directory))
				{

					Icon iconForFile = IconReader.GetFolderIcon(file.FullName, IconReader.IconSize.Large, IconReader.FolderType.Open);
					imageList.Images.Add(file.Name, iconForFile);          //TODO if Extension unknown
				}
				else
				{
					if (!imageList.Images.ContainsKey(file.Name))
					{
						Icon iconForFile = IconReader.GetFileIcon(file.FullName, IconReader.IconSize.Large, false);
						imageList.Images.Add(file.Name, iconForFile);          //TODO if Extension unknown
					}
				}
				item.ImageKey = file.Name;

				listView.Items.Add(item);
			}

			listView.EndUpdate();

			sw.Stop();
			Console.WriteLine("Elapsed={0}", sw.Elapsed);
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
				ChangeDirectory(Dir.ToString() + item.Text);
			}
			else
			{
				fileOperator.OpenFile(Dir + item.Text);
			}
		}

		public void ChangeDirectory(string path)
		{
			pathTextBox.Text = path;
			pathTextBox_Validated(this, null);
		}

		private void buttonBack_Click(object sender, EventArgs e)
		{
			string path = Dir.ToString();
			path = path.Remove(path.LastIndexOf('\\'));
			path = path.Remove(path.LastIndexOf('\\'));
			ChangeDirectory(path);
		}

		private void FileExplorer_FormClosed(object sender, FormClosedEventArgs e)
		{
			imageList.Dispose();
			listView.Dispose();
		}
	}
}
