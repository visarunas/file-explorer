using FileExplorer.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer
{
	public class SearchDisplayer : ListViewFiller
	{
		private bool SearchStopped { get; set; } = false;

		public SearchDisplayer(ListViewManager listViewManager, IColumnManager columns) : base(listViewManager, columns)
		{

		}

		public void Stop()
		{
			SearchStopped = true;
		}

		public void FillListView(string searchName, DirectoryInfo dir)
		{
			SearchStopped = false;
			OnLoadingStarted();
			listViewManager.ClearListView();
			listViewManager.SetColumns(columns);
			SearchAll(searchName, dir);
			OnLoadingFinished();
		}

		private void SearchAll(string searchName, DirectoryInfo dir)
		{
			try
			{
				foreach (FileSystemInfo file in dir.GetFileSystemInfos())
				{
					if (!SearchStopped)
					{
						if (file.Name.Contains(searchName, StringComparison.OrdinalIgnoreCase))
						{
							ListViewFileItem item = ConvertToListViewFileItem(file);
							
							if (file.Attributes.HasFlag(FileAttributes.Directory))	//If Directory
							{
								DirectoryInfo dirFile = new DirectoryInfo(file.FullName);
								columns.AddSubItem(dirFile, item);
							}
							else  //If File
							{
								columns.AddSubItem((FileInfo)file, item);
							}
							listViewManager.AddListViewItem(item, GetFileSystemIcon(file));
						}
						if (file.Attributes.HasFlag(FileAttributes.Directory))
						{
							SearchAll(searchName, new DirectoryInfo(file.FullName));
						}
					}
					else
					{
						break;
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				//Nothing to do, user can't access this particular folder
			}
		}
	}
}
