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

			pathTextBox.Text = Dir.ToString();

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

			// For each file in the c:\ directory, create a ListViewItem
			// and set the icon to the icon extracted from the file.
			foreach (System.IO.FileInfo file in Dir.GetFiles())
			{
				// Set a default icon for the file.
				Icon iconForFile = SystemIcons.WinLogo;

				item = new ListViewItem(file.Name, 1);
				iconForFile = Icon.ExtractAssociatedIcon(file.FullName);

				// Check to see if the image collection contains an image
				// for this extension, using the extension as a key.
				if (!imageList.Images.ContainsKey(file.Extension))
				{
					// If not, add the image to the image list.
					iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
					imageList.Images.Add(file.Extension, iconForFile);
				}
				item.ImageKey = file.Extension;
				listView.Items.Add(item);
			}

			foreach (System.IO.FileInfo file in Dir.GetFiles())
			{
				// Set a default icon for the file.
				Icon iconForFile = SystemIcons.WinLogo;

				item = new ListViewItem(file.Name, 1);
				iconForFile = Icon.ExtractAssociatedIcon(file.FullName);

				// Check to see if the image collection contains an image
				// for this extension, using the extension as a key.
				if (!imageList.Images.ContainsKey(file.Extension))
				{
					// If not, add the image to the image list.
					iconForFile = System.Drawing.Icon.ExtractAssociatedIcon(file.FullName);
					imageList.Images.Add(file.Extension, iconForFile);
				}
				item.ImageKey = file.Extension;
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
			//Console.WriteLine(Dir + item.Text);
			fileOperator.OpenFile(Dir + item.Text);

		}


	}
}
