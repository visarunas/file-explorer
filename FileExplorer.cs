using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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
			listView.View = View.LargeIcon;

			pathTextBox_Validated(this, null);
			//pathTextBox.Validated += new EventHandler(pathTextBox_Validated);

			fileOperator = new FileOperator();



		}

		private void pathTextBox_Validated(object sender, EventArgs e)
		{
			Dir = new System.IO.DirectoryInfo(pathTextBox.Text);

			listView.Clear();
			ListViewItem item;
			listView.BeginUpdate();

			foreach (System.IO.FileInfo file in Dir.GetFiles())
			{
				//Icon iconForFile = SystemIcons.WinLogo;

				item = new ListViewItem(file.Name);

				if (!imageList.Images.ContainsKey(file.Extension))
				{
					Icon iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
					imageList.Images.Add(file.Extension, iconForFile);			//TODO if Extension unknown
				}
				item.ImageKey = file.Extension;
				listView.Items.Add(item);
			}

			imageList.Images.Add("dir", SystemIcons.Hand);
			foreach (System.IO.DirectoryInfo dir in Dir.GetDirectories())
			{
				item = new ListViewItem(dir.Name);
				item.ImageKey = "dir";
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
