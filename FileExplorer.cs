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
using System.IO;
using System.Threading;

namespace FileExplorer
{
	public partial class FileExplorer : Form
	{
		public event EventHandler OnDirectoryChanged;

		private DirectoryInfo dir;
		public DirectoryInfo Dir {
			get {
				return dir;
			}
			set {
				if (!value.Exists)
				{
					throw new DirectoryNotFoundException();
				}
				else if (value.ToString().Last() == '\\')
				{
					dir = value;
				}
				else
				{
					dir = new DirectoryInfo(value.ToString() + @"\");
				}
				OnDirectoryChanged?.Invoke(this, new EventArgs());
				pathTextBox.Text = Dir.ToString();
			}
		}

		private UndoRedoList<string> pathList;
		private FileOperator fileOperator;
		private FileListViewUpdater fileListViewUpdater;

		public FileExplorer()
		{
			InitializeComponent();

			//OnDirectoryChanged += pathTextBox_Validated;

			fileListViewUpdater = new FileListViewUpdater();
			pathList = new UndoRedoList<string>();
			Dir = new DirectoryInfo(@"c:\users\Sarunas\Desktop");
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

			searchTextBox.GotFocus += searchTextBox_GotFocus;
			searchTextBox.LostFocus += searchTextBox_LostFocus;

			

		}


		private void searchTextBox_GotFocus(object sender, EventArgs e)
		{
			if (searchTextBox.Text == "Search")
			{
				searchTextBox.ForeColor = Color.Black;
				searchTextBox.Text = "";
			}
		}

		private void searchTextBox_LostFocus(object sender, EventArgs e)
		{
			if (searchTextBox.Text == "")
			{
				searchTextBox.ForeColor = Color.DarkGray;
				searchTextBox.Text = "Search";
			}
		}

		private void pathTextBox_Validated(object sender, EventArgs e)
		{
			//Stopwatch sw = new Stopwatch();
			//sw.Start();

			ChangeDirectory(pathTextBox.Text);

			//sw.Stop();
			//Debug.WriteLine("Elapsed={0}", sw.Elapsed);

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

			if (item.Attributes.HasFlag(FileAttributes.Directory))
			{
				ChangeDirectory(item.Name);
			}
			else
			{
				fileOperator.OpenFile(item.Name);
			}
		}

		public void ChangeDirectory(string path)
		{
			Dir = new DirectoryInfo(path);
			updateView();
			Debug.WriteLine("Changed Directory to: " + Dir.ToString());
			pathList.AddNext(path);
		}

		private void updateView()
		{
			fileListViewUpdater.update(listView, imageList, Dir);
		}

		private void buttonBack_Click(object sender, EventArgs e)
		{
			ChangeDirectory(Dir.Parent.FullName);
		}

		private void FileExplorer_FormClosed(object sender, FormClosedEventArgs e)
		{
			//imageList.Dispose();
			//listView.Dispose();
		}

		private void listView_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				
			}
		}

		private void buttonUndo_Click(object sender, EventArgs e)
		{
			Dir = new DirectoryInfo(pathList.Undo());
			updateView();
			Debug.WriteLine("Undo Directory to: " + Dir.ToString());
		}

		private void buttonRedo_Click(object sender, EventArgs e)
		{
			Dir = new DirectoryInfo(pathList.Redo());
			updateView();
			Debug.WriteLine("Redo Directory to: " + Dir.ToString());
		}

		public void SearchFile(string searchName, DirectoryInfo dir)
		{
			
			foreach (FileSystemInfo file in dir.GetFileSystemInfos("*", SearchOption.AllDirectories))
			{
				if(file.Name.Contains(searchName))
				{
					string imageKey = file.Name;
					ListViewFileItem item = new ListViewFileItem(file.Name);
					item.Attributes = file.Attributes;
					item.Name = file.FullName;
					Icon icon;
					if (file.Attributes.HasFlag(FileAttributes.Directory))
					{
						icon = IconReader.GetFolderIcon(file.FullName, IconReader.IconSize.Large, IconReader.FolderType.Open);
					}
					else
					{
						icon = IconReader.GetFileIcon(file.FullName, IconReader.IconSize.Large, false);
					}
					item.ImageKey = imageKey;

					//imageList.Images.Add(imageKey, icon);
					AddListViewItem(item, icon);
					//listView.EndUpdate();
					//listView.Refresh();
					//listView.BeginUpdate();
				}
			}
		}

		delegate void SetTextCallback(ListViewItem item, Icon icon);

		private void AddListViewItem(ListViewItem item, Icon icon)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (InvokeRequired)
			{
				SetTextCallback d = new SetTextCallback(AddListViewItem);
				this.Invoke(d, new object[] { item, icon });
			}
			else
			{
				imageList.Images.Add(item.ImageKey, icon);
				listView.Items.Add(item);
				listView.Refresh();
			}
		}

		private void searchTextBox_Validated(object sender, EventArgs e)
		{
			if (searchTextBox.Text == "Search")
			{
				ChangeDirectory(Dir.ToString());
			}
			else
			{
				listView.Clear();
				imageList.Images.Clear();
				var columnManager = new ListViewColumnManager(listView);
				columnManager.addColumn("Name", 400);
				string searchName = searchTextBox.Text;

				//listView.BeginUpdate();

				Thread sThread = new Thread( () => SearchFile(searchName, Dir));

				sThread.Start();
				//SearchFile(searchName, Dir);


				//listView.EndUpdate();
			}

		}
	}
}
