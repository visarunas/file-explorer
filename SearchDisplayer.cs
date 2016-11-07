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
		public SearchDisplayer(ListViewManager listViewManager) : base(listViewManager)
		{

		}

		public void FillListView(string searchName, DirectoryInfo dir)
		{
			listViewManager.ClearListView();
			SearchAll(searchName, dir);
		}

		private void SearchAll(string searchName, DirectoryInfo dir)
		{
			try
			{
				foreach (FileSystemInfo file in dir.GetFileSystemInfos())
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
			}
			catch (UnauthorizedAccessException)
			{
				//Nothing to do, user can't access this particular folder
			}
		}
	}
}
