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
		public DirectoryDisplayer(ListViewManager listViewManager, IColumnManager columns) : base(listViewManager, columns)
		{

		}
		
		public void FillListView(DirectoryInfo dir)
		{
			listViewManager.ClearListView();
			listViewManager.SetColumns(columns);
			try
			{
				var files = from fileSys in dir.EnumerateFiles()
							select fileSys;

				foreach (var file in files)
				{
					ListViewItem item = ConvertToListViewItem(file);
					item.SubItems.Add(file.Length.ToString());
					item.SubItems.Add(file.CreationTime.ToLongDateString());
					listViewManager.AddListViewItem(item, getFileIcon(file));
				}

				var dirs = from fileSys in dir.EnumerateDirectories()
							select fileSys;

				foreach (var directory in dirs)
				{
					ListViewItem item = ConvertToListViewItem(directory);
					item.SubItems.Add("");
					item.SubItems.Add(directory.CreationTime.ToLongDateString());
					listViewManager.AddListViewItem(item, getDirectoryIcon(directory));
				}
			}
			catch (UnauthorizedAccessException)
			{
				//Nothing to do if the file isn't accessible
			}
		}

	}
}
