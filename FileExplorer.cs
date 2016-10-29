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


namespace FileExplorer
{
	public partial class FileExplorer : Form
	{

		private System.IO.DirectoryInfo _dir;
		public System.IO.DirectoryInfo Dir {
			get {
				return _dir;
			}
			set {
				if (value.ToString().Last() == '\\')
				{
					value = _dir;
				}
				else
				{
					_dir = new System.IO.DirectoryInfo(value.ToString() + @"\");
				}
			}
		}
				
		private FileOperator fileOperator;

		public FileExplorer()
		{
			InitializeComponent();

			Dir = new System.IO.DirectoryInfo(@"C:\Users\Sarunas\Downloads");
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

			
			pathTextBox_Validated(this, null);
			//pathTextBox.Validated += new EventHandler(pathTextBox_Validated);

			fileOperator = new FileOperator();



		}

		private void pathTextBox_Validated(object sender, EventArgs e)
		{
			Dir = new System.IO.DirectoryInfo(pathTextBox.Text);

			listView.Clear();

			//imageList.Images.Add("dir", SystemIcons.Hand);

			var cm = new ColumnHeader();
			cm.Text = "Name";
			cm.Width = 300;
			imageList.ImageSize = new Size(20, 20);
			listView.Columns.Add(cm);

			ListViewItem item;
			listView.BeginUpdate();

			foreach (System.IO.FileSystemInfo file in Dir.GetFileSystemInfos())
			{
				item = new ListViewItem(file.Name);
				if (file.Attributes.HasFlag(System.IO.FileAttributes.Directory))
				{
					Icon iconForFile = IconReader.GetFolderIcon(0, IconReader.FolderType.Closed);
				}
				else
				{
					if (!imageList.Images.ContainsKey(file.Name))
					{
						Console.WriteLine(file.Extension);

						Icon iconForFile = IconReader.GetFileIcon(file.FullName, 0, false);
						imageList.Images.Add(file.Name, iconForFile);          //TODO if Extension unknown
					}
					item.ImageKey = file.Name;
				}

				listView.Items.Add(item);
			}

			listView.EndUpdate();
			
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
			ListViewItem item = itemCollection[0];
			if (item.ImageKey == "dir")
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
	}
}
