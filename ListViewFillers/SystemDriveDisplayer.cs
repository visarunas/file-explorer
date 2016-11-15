using Etier.IconHelper;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer
{
	public class SystemDriveDisplayer : ListViewFiller
	{
		public SystemDriveDisplayer(ListViewManager listViewManager, IColumnManager columns) : base(listViewManager, columns)
		{

		}

		override
		public void FillListView()
		{
			listViewManager.ClearListView();
			listViewManager.SetColumns(columns);

			DriveInfo[] allDrives = DriveInfo.GetDrives();
			foreach (DriveInfo drive in allDrives)
			{
				if (!Stopped)
				{
					ListViewFileItem item = new ListViewFileItem(drive.Name);
					item.Name = drive.Name;
					item.Attributes = FileAttributes.Directory;
					item.ImageKey = item.Name;
					Icon icon = IconReader.GetFolderIcon(null, IconReader.IconSize.Large, IconReader.FolderType.Open);
					listViewManager.AddListViewItem(item, icon);
				}
			}
		}
	}
}
