using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using static System.Windows.Forms.ListView;
using System.Security;
using System.ComponentModel;
using System.Collections.Generic;
using FileExplorer.ColumnManagers;

/*
 * 
 * Lazy - SearchDisplayer
 * Delegatai - ListViewManager
 * Events - SearchDisplayer
 * Exceptions - UndoRedoStack ir ChangeDirectory()
 * Anoniminiai metodai - FileExplorer constructorius ir ListViewManager
 * Lambda - ChangeDirectory(), SearchForFile()
 * Threading ir async await - StopProcesses()
 * Config - bet kuris ColumnManager
 * Dependency Injection - FileExplorer constructorius
 * 
 */

	
namespace FileExplorer
{
	public partial class FileExplorer : Form
	{
		private UndoRedoStack UndoRedoStack;
		private FileOperator fileOperator;
		private ListViewManager listViewManager;
		private Task loadTask;
		private DirectoryDisplayer directoryDisplayer;
		private SystemDriveDisplayer systemDriveDisplayer;
		private Lazy<SearchDisplayer> searchDisplayer;
		private IColumnManager dirColumns, searchColumns;

		public FileExplorer()
		{
			InitializeComponent();

			dirColumns = new DirectoryColumnManager();
			searchColumns = new SearchColumnManager();
			
			listViewManager = new ListViewManager(listView, imageList);
			directoryDisplayer = new DirectoryDisplayer(listViewManager, dirColumns);
			systemDriveDisplayer = new SystemDriveDisplayer(listViewManager, dirColumns);

			searchDisplayer = new Lazy<SearchDisplayer>( () => new SearchDisplayer(            
				listViewManager, searchColumns,
				delegate
				{ indicatorPictureBox.Image = Properties.Resources.loadingImage; }, 
				delegate
				{ indicatorPictureBox.Image = null; } ) );

			UndoRedoStack = new UndoRedoStack();
			

			listView.LargeImageList = imageList;
			listView.SmallImageList = imageList;
			listView.View = View.Details;

			pathTextBox.GotFocus += OnPathTextBoxFocus;

			imageList.ImageSize = new Size(Properties.Settings.Default.Icon_size, Properties.Settings.Default.Icon_size);
			fileOperator = new FileOperator();

			ChangeDirectory(Environment.GetFolderPath(Environment.SpecialFolder.Desktop));

		}

		private async void SearchForFile(string searchName, bool addToPathList = true)
		{
			setPathTextBoxText("Search results: " + searchName);
			setSearchTextBoxText(searchName);
			await StopProcesses();
			if (addToPathList)
			{
				UndoRedoStack.AddNext(() => SearchForFile(searchName, false));
			}
			searchDisplayer.Value.searchPhrase = searchName;
			searchDisplayer.Value.Dir = directoryDisplayer.Dir;
			loadTask = new Task( () => searchDisplayer.Value.FillListView() );
			loadTask.Start();
		}

		public async void ChangeDirectory(string path, bool addToPathList = true)
		{
			setSearchTextBoxText(string.Empty);
			await StopProcesses();
			if (path != string.Empty && path != null)
			{
				setPathTextBoxText(path);
				try
				{
					directoryDisplayer.Dir = new DirectoryInfo(path);
					loadTask = new Task( () => directoryDisplayer.FillListView() );
					loadTask.Start();

					if (addToPathList)
					{
						UndoRedoStack.AddNext(() => ChangeDirectory(path, false));
					}
				}
				catch (ArgumentNullException)
				{
					MessageBox.Show("Path not specified");
				}
				catch (SecurityException)
				{
					MessageBox.Show("Access denied");
				}
				catch (ArgumentException)
				{
					MessageBox.Show("Path contains invalid characters");
				}
				catch (PathTooLongException)
				{
					MessageBox.Show("Specified path is too long");
				}
				catch (DirectoryNotFoundException)
				{
					MessageBox.Show("Specified directory not found");
				}
				finally
				{
					setPathTextBoxText(directoryDisplayer.Dir.ToString());
				}
			}
			else
			{
				setPathTextBoxText(Properties.Settings.Default.DriveDirectory_name);
				if (addToPathList)
				{
					UndoRedoStack.AddNext(() => ChangeDirectory(null, false));
				}
				loadTask = new Task( () => systemDriveDisplayer.FillListView() );
				loadTask.Start();
			}
		}

		private void OnPathTextBoxFocus(object sender, EventArgs e)
		{
			setPathTextBoxText(directoryDisplayer.Dir.ToString());
		}

		private void setPathTextBoxText(string text)
		{
			pathTextBox.Text = text;
			pathTextBox.Refresh();
		}

		private void setSearchTextBoxText(string text)
		{
			searchTextBox.Text = text;
			searchTextBox.Refresh();
		}

		private void pathTextBox_Validated(object sender, EventArgs e)
		{
			ChangeDirectory(pathTextBox.Text);
		}

		private void searchTextBox_TextChanged(object sender, EventArgs e)
		{
			if (searchTextBox.Focused)
			{
				if (searchTextBox.Text != string.Empty)
				{
					SearchForFile(searchTextBox.Text, false);
				}
				else
				{
					ChangeDirectory(directoryDisplayer.Dir.ToString());
				}
			}
		}

		private void searchTextBox_Validated(object sender, EventArgs e)
		{
			if (searchTextBox.Text != string.Empty)
			{
				string searchText = searchTextBox.Text;
				UndoRedoStack.AddNext(() => SearchForFile(searchText, false));
			}
		}

		private void pathTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.Enter)
			{
				pathTextBox_Validated(this, null);
				listView.Focus();
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
				try
				{
					fileOperator.OpenFile(item.Name);
				}
				catch (FileNotFoundException)
				{
					MessageBox.Show("File not found");
				}
				catch(ObjectDisposedException)
				{
					MessageBox.Show("Failed to open file");
				}
				catch (Win32Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void buttonUndo_Click(object sender, EventArgs e)
		{
			UndoRedoStack.Undo().Invoke();
		}

		private void buttonRedo_Click(object sender, EventArgs e)
		{
			UndoRedoStack.Redo().Invoke();
		}

		private void buttonBack_Click(object sender, EventArgs e)
		{
			if (searchTextBox.Text == string.Empty)
			{
				if (directoryDisplayer.Dir.Parent != null)
				{
					ChangeDirectory(directoryDisplayer.Dir.Parent.FullName);
				}
				else
				{
					ChangeDirectory("");
				}
			}
			else
			{
				ChangeDirectory(directoryDisplayer.Dir.ToString());
			}
		}

		private void RefreshView()
		{
			try
			{
				UndoRedoStack.GetCurrent().Invoke();
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
				fileOperator.PasteFile(directoryDisplayer.Dir.ToString());
			}
			else if (e.ClickedItem == deleteToolStripMenuItem)
			{
				var arr = new ListViewFileItem[getSelectedItems().Count];
				getSelectedItems().CopyTo(arr, 0);
				fileOperator.DeleteFile(arr);
			}
		}

		private void listView_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
		{
			if (dirColumns != null)
				dirColumns.UpdateColumnWidths();
			if (searchColumns != null)
				searchColumns.UpdateColumnWidths();
		}

		private SelectedListViewItemCollection getSelectedItems()
		{
			return listView.SelectedItems;
		}

		private async Task StopProcesses()
		{
			directoryDisplayer.Stop();
			searchDisplayer.Value.Stop();
			if (loadTask != null)
				await Task.WhenAll(loadTask);
		}

		private void FileExplorer_FormClosed(object sender, FormClosedEventArgs e)
		{
			Properties.Settings.Default.Save();
		}

	}
}
