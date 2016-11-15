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
		private DirectoryInfo dir;
		public DirectoryInfo Dir
		{
			get
			{
				return dir;
			}
			set
			{
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
			}
		}

		public DirectoryDisplayer(ListViewManager listViewManager, IColumnManager columns) : base(listViewManager, columns)
		{
			
		}
		
		override
		public void FillListView()
		{
			Stopped = false;
			listViewManager.ClearListView();
			listViewManager.SetColumns(columns);
			try
			{
				var files = from fileSys in Dir.EnumerateFiles()
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

				var dirs = from fileSys in Dir.EnumerateDirectories()
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

	}
}
