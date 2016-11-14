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
	public class ListViewFiller
	{
		protected ListViewManager listViewManager;
		public event EventHandler LoadingFinished, LoadingStarted;
		protected IColumnManager columns;

		public ListViewFiller(ListViewManager listViewManager, IColumnManager columns)
		{
			this.listViewManager = listViewManager;
			this.columns = columns;
		}

		protected void OnLoadingFinished()
		{
			LoadingFinished?.Invoke(this, new EventArgs());
		}

		protected void OnLoadingStarted()
		{
			LoadingStarted?.Invoke(this, new EventArgs());
		}

		public virtual ListViewFileItem ConvertToListViewFileItem(FileSystemInfo file)
		{
			ListViewFileItem item = new ListViewFileItem(file.Name);
			item.Name = file.FullName;
			item.ImageKey = item.Name;
			item.Attributes = file.Attributes;

			return item;
		}

		public virtual Icon GetFileSystemIcon(FileSystemInfo file)
		{
			if (file.Attributes.HasFlag(FileAttributes.Directory))
			{
				return GetDirectoryIcon(new DirectoryInfo(file.FullName));
			}
			else
			{
				return GetFileIcon(new FileInfo(file.FullName));
			}
		}

		public virtual Icon GetFileIcon(FileInfo file)
		{
			Icon icon = IconReader.GetFileIcon(file.FullName, IconReader.IconSize.Large, false);
			return icon;
		}

		public virtual Icon GetDirectoryIcon(DirectoryInfo dir)
		{
			Icon icon = IconReader.GetFolderIcon(dir.FullName, IconReader.IconSize.Large, IconReader.FolderType.Open);
			return icon;
		}
	}
}
