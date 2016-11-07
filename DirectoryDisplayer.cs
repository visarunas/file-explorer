using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileExplorer
{
	public class DirectoryDisplayer : ListViewFiller
	{
		public DirectoryDisplayer(ListViewManager listViewManager) : base(listViewManager)
		{

		}
		
		public void FillListView(ListView listView, DirectoryInfo dir)
		{
			listViewManager.ClearListView();
			try
			{
				var files = from fileSys in dir.EnumerateFileSystemInfos()
							select fileSys;

				foreach (var file in files)
				{
					listViewManager.AddFile(file);
				}
			}
			catch (UnauthorizedAccessException)
			{
				//Nothing to do if the file isn't accessible
			}
		}

	}
}
