using System;
using System.Collections.Generic;
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

		public void FillListView()
		{
			listViewManager.ClearListView();

			DriveInfo[] allDrives = DriveInfo.GetDrives();
			foreach (DriveInfo drive in allDrives)
			{
				listViewManager.AddDrive(drive);
			}
		}
	}
}
