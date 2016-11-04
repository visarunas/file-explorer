using Etier.IconHelper;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace FileExplorer
{
	public class FileListViewUpdater
	{
		public FileListViewUpdater()
		{

		}

		public void update(ListView listView, ImageList imageList, DirectoryInfo Dir)
		{
			listView.Clear();
			imageList.Images.Clear();

			var columnManager = new ListViewColumnManager(listView);
			columnManager.addColumn("Name", 400);

			ListViewFileItem item;
			listView.BeginUpdate();

			foreach (FileSystemInfo file in Dir.GetFileSystemInfos())
			{
				string imageKey = file.Name;
				item = new ListViewFileItem(file.Name);
				item.Attributes = file.Attributes;
				if (file.Attributes.HasFlag(FileAttributes.Directory))
				{

					Icon iconForFile = IconReader.GetFolderIcon(file.FullName, IconReader.IconSize.Large, IconReader.FolderType.Open);
					imageList.Images.Add(imageKey, iconForFile);          //TODO if Extension unknown
				}
				else
				{
					if (!imageList.Images.ContainsKey(imageKey))
					{
						Icon iconForFile = IconReader.GetFileIcon(file.FullName, IconReader.IconSize.Large, false);
						imageList.Images.Add(imageKey, iconForFile);          //TODO if Extension unknown
					}
				}
				item.ImageKey = imageKey;
				listView.Items.Add(item);
			}
			listView.EndUpdate();
		}

	}
}