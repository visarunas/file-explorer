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
				setPathTextBoxText(dir.ToString());
				pathTextBox.Refresh();
			}
		}

		private UndoRedoStack UndoRedoList;
		private FileOperator fileOperator;
		private ListViewManager listViewManager;
		private Task searchThread, loadThread;
		private DirectoryDisplayer directoryDisplayer;
		private SystemDriveDisplayer systemDriveDisplayer;
		private Lazy<SearchDisplayer> searchDisplayer;			//Lazy
		private IColumnManager dirColumns;

		public FileExplorer()
		{
			InitializeComponent();

			dirColumns = new DirectoryColumnManager();
			
			listViewManager = new ListViewManager(listView, imageList);
			directoryDisplayer = new DirectoryDisplayer(listViewManager, dirColumns);
			systemDriveDisplayer = new SystemDriveDisplayer(listViewManager, dirColumns);

			searchDisplayer = new Lazy<SearchDisplayer>( () => new SearchDisplayer(             //Lazy 
				listViewManager, dirColumns,
				delegate
				{ indicatorPictureBox.Image = Properties.Resources.loadingImage; }, 
				delegate
				{ indicatorPictureBox.Image = null; } ) );

			UndoRedoList = new UndoRedoStack();
			Dir = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));
			

			listView.LargeImageList = imageList;
			listView.SmallImageList = imageList;
			listView.View = View.Details;

			imageList.ImageSize = new Size(Properties.Settings.Default.Icon_size, Properties.Settings.Default.Icon_size);
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
			ChangeDirectory(pathTextBox.Text);
		}

		private void pathTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				pathTextBox_Validated(this, null);	
			}
		}

		private async void listView_DoubleClick(object sender, EventArgs e)
		{
			await StopProcesses();
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

		public async void ChangeDirectory(string path, bool addToPathList = true)
		{
			await StopProcesses();
			if (path != string.Empty && path != null)
			{
				Dir = new DirectoryInfo(path);
				loadThread = new Task( () => directoryDisplayer.FillListView(Dir) );
				loadThread.Start();

				if (addToPathList)
				{
					string currentPath = Dir.ToString();
					UndoRedoList.AddNext( () => ChangeDirectory(currentPath, false) );
				}
			}
			else
			{
				setPathTextBoxText("This PC");
				if (addToPathList)
				{
					UndoRedoList.AddNext( () => ChangeDirectory(null, false) );
				}
				systemDriveDisplayer.FillListView();
			}
		}

		private void buttonBack_Click(object sender, EventArgs e)
		{
			if (Dir.Parent != null)
			{
				ChangeDirectory(Dir.Parent.FullName);
			}
			else
			{
				ChangeDirectory("");
			}
			
		}

		private async Task StopProcesses()
		{
			directoryDisplayer.Stop();
			searchDisplayer.Value.Stop();
			if (searchThread != null)
				await Task.WhenAll(searchThread);
			if (loadThread != null)
				await Task.WhenAll(loadThread);
		}

		private void buttonUndo_Click(object sender, EventArgs e)
		{
			UndoRedoList.Undo().Invoke();
		}

		private void buttonRedo_Click(object sender, EventArgs e)
		{
			UndoRedoList.Redo().Invoke();
		}

		private void searchTextBox_Validated(object sender, EventArgs e)
		{
			SearchForFile(searchTextBox.Text);
		}

		private async void SearchForFile(string searchName, bool addToPathList = true)
		{
			if (searchTextBox.Text == "Search")
			{
				ChangeDirectory(Dir.ToString());
			}
			else
			{
				await StopProcesses();
				if (addToPathList)
				{
					UndoRedoList.AddNext( () => SearchForFile(searchName, false) );
				}
				searchThread = new Task( () => searchDisplayer.Value.FillListView(searchName, Dir) );
				searchThread.Start();

			}
		}

		private void RefreshView()
		{
			try
			{
				UndoRedoList.GetCurrent().Invoke();
			}
			catch (EmptyStackException e)
			{
				Debug.WriteLine(e.Message);
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
				RefreshView();
			}
			else if (e.ClickedItem == deleteToolStripMenuItem)
			{
				var arr = new ListViewFileItem[getSelectedItems().Count];
				getSelectedItems().CopyTo(arr, 0);
				fileOperator.DeleteFile(arr);
				RefreshView();
			}
		}

		private void listView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
		{
			dirColumns.UpdateColumnWidths();
		}

		private void FileExplorer_FormClosed(object sender, FormClosedEventArgs e)
		{
			Properties.Settings.Default.Save();
		}

		private SelectedListViewItemCollection getSelectedItems()
		{
			return listView.SelectedItems;
		}

	}
}
