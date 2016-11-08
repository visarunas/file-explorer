using Etier.IconHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Windows.Forms;

namespace FileExplorer
{
	public class ListViewManager
	{
		private ListView listView;
		private ImageList imageList;
		private Form fileExplorer;
		private ListViewColumnManager columnManager;

		public ListViewManager(ListView listView, ImageList imageList, Form fileExplorer)
		{
			this.listView = listView;
			this.imageList = imageList;
			this.fileExplorer = fileExplorer;
			columnManager = new ListViewColumnManager(listView);
		}

		public void AddFile(FileSystemInfo file)
		{

			ListViewFileItem item = new ListViewFileItem(file.Name);
			item.Attributes = file.Attributes;
			item.Name = file.FullName;
			item.ImageKey = item.Name;
			item.CreationDate = file.CreationTime;
			Icon icon;
			if (file.Attributes.HasFlag(FileAttributes.Directory))
			{
				icon = IconReader.GetFolderIcon(file.FullName, IconReader.IconSize.Large, IconReader.FolderType.Open);
			}
			else
			{
				icon = IconReader.GetFileIcon(file.FullName, IconReader.IconSize.Large, false);
				item.Size = new FileInfo(file.FullName).Length;
			}
			AddListViewItem(item, icon);
		}

		public void AddDrive(DriveInfo drive)
		{
			ListViewFileItem item = new ListViewFileItem(drive.Name);
			item.Name = drive.Name;
			item.Attributes = FileAttributes.Directory;
			item.ImageKey = item.Name;
			Icon icon = IconReader.GetFolderIcon(null, IconReader.IconSize.Large, IconReader.FolderType.Open);
			AddListViewItem(item, icon);
		}

		public void DisplaySystemDrives()
		{
			ClearListView();

			DriveInfo[] allDrives = DriveInfo.GetDrives();
			foreach (DriveInfo drive in allDrives)
			{
				AddDrive(drive);
			}

		}

		delegate void SetListViewCallback(ListViewFileItem item, Icon icon);

		private void AddListViewItem(ListViewFileItem item, Icon icon)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (fileExplorer.InvokeRequired)
			{
				SetListViewCallback d = new SetListViewCallback(AddListViewItem);
				fileExplorer.Invoke(d, new object[] { item, icon });
			}
			else
			{
				if (item.Size != 0)
				{
					item.SubItems.Add(item.Size.ToString());
				}
				else
				{
					item.SubItems.Add("");
				}
				if (item.CreationDate == default(DateTime))
				{
					item.SubItems.Add("");
				}
				else
				{
					item.SubItems.Add(item.CreationDate.ToShortDateString() + "  " + item.CreationDate.ToShortTimeString());
				}
				imageList.Images.Add(item.ImageKey, icon);
				listView.Items.Add(item);
			}
		}

		public void ClearListView()
		{
			if (fileExplorer.InvokeRequired)
			{
				MethodInvoker d = delegate { ClearListView(); };
				fileExplorer.Invoke(d, new object[] { });
			}
			else
			{
				imageList.Images.Clear();
				listView.Clear();

				//TODO this is temp
				columnManager.addColumn("Name", 400);
				columnManager.addColumn("Size", 100);
				columnManager.addColumn("Creation date", 400);
			}
		}

		public void DisplayDirectory(DirectoryInfo Dir)
		{
			ClearListView();

			try
			{
				var files = from fileSys in Dir.EnumerateFileSystemInfos()
							select fileSys;

				foreach (var file in files)
				{
					AddFile(file);
				}
			}
			catch (UnauthorizedAccessException e)
			{
				Debug.WriteLine(e.Message);
			}
		}

	}
}