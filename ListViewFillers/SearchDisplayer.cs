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
		public event EventHandler LoadingFinished, LoadingStarted;      //Custom Events

		public DirectoryInfo Dir { get; set; }
		public string searchPhrase { get; set; }

		public SearchDisplayer(ListViewManager listViewManager, IColumnManager columns, EventHandler loadingStart, EventHandler loadingFinish) : base(listViewManager, columns)
		{
			LoadingFinished += loadingFinish;
			LoadingStarted += loadingStart;
		}

		public void OnLoadingFinished()
		{
			LoadingFinished?.Invoke(this, new EventArgs());
		}

		public void OnLoadingStarted()
		{
			LoadingStarted?.Invoke(this, new EventArgs());
		}

		override
		public void FillListView()
		{
			if (searchPhrase == null) throw new SearchPhraseNullException("Search phrase not specified");
			else
			{
				Stopped = false;
				OnLoadingStarted();
				listViewManager.ClearListView();
				listViewManager.SetColumns(columns);
				SearchAll(searchPhrase, Dir);
				OnLoadingFinished();
			}
		}

		private void SearchAll(string searchName, DirectoryInfo dir)
		{
			try
			{
				foreach (FileSystemInfo file in dir.GetFileSystemInfos())
				{
					if (!Stopped)
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
