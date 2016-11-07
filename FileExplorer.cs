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
using System.Security.Permissions;
using FileExplorer.ExtensionMethods;
using static System.Windows.Forms.ListView;

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
				setPathTextBoxText(dir.ToString());
			}
		}

		private UndoRedoList pathList;
		private FileOperator fileOperator;
		private ListViewManager fileListViewUpdater;
		private Thread searchThread, loadThread;
		private DirectoryDisplayer directoryDisplayer;
		private SystemDriveDisplayer systemDriveDisplayer;
		private SearchDisplayer searchDisplayer;

		public FileExplorer()
		{
			InitializeComponent();
			//OnDirectoryChanged += pathTextBox_Validated;

			fileListViewUpdater = new ListViewManager(listView, imageList, this);
			directoryDisplayer = new DirectoryDisplayer(fileListViewUpdater);
			systemDriveDisplayer = new SystemDriveDisplayer(fileListViewUpdater);
			searchDisplayer = new SearchDisplayer(fileListViewUpdater);
			pathList = new UndoRedoList();
			Dir = new DirectoryInfo(@"c:\users\Sarunas\Desktop");
			

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

			ChangeDirectory(Dir.ToString());

		}

		private void searchTextBox_GotFocus(object sender, EventArgs e)
		{
			if (searchTextBox.Text == "Search")
			{
				searchTextBox.ForeColor = Color.Black;
				searchTextBox.Text = "";
			}
		}

		private void setPathTextBoxText(string text)
		{
			pathTextBox.Text = text;
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

		private SelectedListViewItemCollection getSelectedItems()
		{
			return listView.SelectedItems;
		}

		private void listView_DoubleClick(object sender, EventArgs e)
		{
			SelectedListViewItemCollection itemCollection = listView.SelectedItems;
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

		public void ChangeDirectory(string path, bool addToPathList = true)
		{
			if (path != string.Empty && path != null)
			{
				Dir = new DirectoryInfo(path);
				killThread(loadThread);
				loadThread = new Thread( () => directoryDisplayer.FillListView(listView, Dir) );
				loadThread.Start();
				if (addToPathList)
				{
					string currentPath = Dir.ToString();
					pathList.AddNext(() => ChangeDirectory(currentPath, false));
				}
			}
			else
			{
				setPathTextBoxText("This PC");
				Debug.WriteLine("Drive info");
				if (addToPathList)
				{
					pathList.AddNext( () => ChangeDirectory(null, false) );
				}
				//pathList.AddNext(path);
				systemDriveDisplayer.FillListView();

			}
			
		}

		private void buttonBack_Click(object sender, EventArgs e)
		{
			killThread(searchThread);
			if (Dir.Parent != null)
			{
				ChangeDirectory(Dir.Parent.FullName);
			}
			else
			{
				ChangeDirectory("");
			}
			
		}

		private void FileExplorer_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (searchThread != null)
			{
				searchThread.Abort();
			}
			if (loadThread != null)
			{
				loadThread.Abort();
			}
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
			killThread(searchThread);
			pathList.Undo().Invoke();
		}

		private void buttonRedo_Click(object sender, EventArgs e)
		{
			killThread(searchThread);
			pathList.Redo().Invoke();
		}

		private void searchTextBox_Validated(object sender, EventArgs e)
		{
			SearchForFile(searchTextBox.Text);
		}

		private void SearchForFile(string searchName, bool addToPathList = true)
		{
			if (searchTextBox.Text == "Search")
			{
				ChangeDirectory(Dir.ToString());
			}
			else
			{
				if (addToPathList)
				{
					pathList.AddNext(() => SearchForFile(searchName, false));
				}
				killThread(searchThread);
				searchThread = new Thread(() => searchDisplayer.FillListView(searchName, Dir));
				searchThread.Start();

			}
		}

		private void viewListContext_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
		{
			if (e.ClickedItem == copyToolStripMenuItem)
			{
				var arr = new ListViewFileItem[getSelectedItems().Count];
				getSelectedItems().CopyTo(arr, 0);
				fileOperator.SelectFiles(arr);
			}
			else if (e.ClickedItem == pasteToolStripMenuItem)
			{
				fileOperator.PasteFile(Dir.ToString());
			}
		}

		[SecurityPermission(SecurityAction.Demand, ControlThread = true)]
		private void killThread(Thread thread)
		{
			if (thread != null)
			{
				thread.Abort();	//TODO Make thread killing safe
			}
		}


	}
}
