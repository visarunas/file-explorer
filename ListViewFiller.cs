using Etier.IconHelper;
using System.Drawing;
using System.IO;


namespace FileExplorer
{
	public abstract class ListViewFiller
	{
		protected ListViewManager listViewManager;

		protected IColumnManager columns;

		public ListViewFiller(ListViewManager listViewManager, IColumnManager columns)
		{
			this.listViewManager = listViewManager;
			this.columns = columns;
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
