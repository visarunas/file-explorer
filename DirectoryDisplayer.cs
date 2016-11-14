using Etier.IconHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileExplorer
{
	public class DirectoryDisplayer : ListViewFiller
	{
		private bool Stopped { get; set; } = false;

		public DirectoryDisplayer(ListViewManager listViewManager, IColumnManager columns) : base(listViewManager, columns)
		{
			
		}
		
		public void FillListView(DirectoryInfo dir)
		{
			Stopped = false;
			listViewManager.ClearListView();
			listViewManager.SetColumns(columns);
			try
			{
				var files = from fileSys in dir.EnumerateFiles()
							select fileSys;

				foreach (var file in files)
				{
					if (!Stopped)
					{
						ListViewFileItem item = ConvertToListViewFileItem(file);
						columns.AddSubItem(file, item);
						listViewManager.AddListViewItem(item, GetFileIcon(file));
					}
					else
					{
						break;
					}
				}

				var dirs = from fileSys in dir.EnumerateDirectories()
							select fileSys;

				foreach (var directory in dirs)
				{
					if (!Stopped)
					{
						ListViewFileItem item = ConvertToListViewFileItem(directory);
						columns.AddSubItem(directory, item);
						listViewManager.AddListViewItem(item, GetDirectoryIcon(directory));
					}
					else
					{
						break;
					}
				}
			}
			catch (UnauthorizedAccessException)
			{
				//Nothing to do if the file isn't accessible
			}
		}

		public void Stop()
		{
			Stopped = true;
		}
	}
}
