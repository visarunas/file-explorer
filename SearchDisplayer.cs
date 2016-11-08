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

		public SearchDisplayer(ListViewManager listViewManager) : base(listViewManager)
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
							listViewManager.AddFile(file);
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
