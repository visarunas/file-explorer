using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FileExplorer
{
	public class DirectoryColumnManager : IColumnManager
	{
		private List<ColumnHeader> columns;

		public DirectoryColumnManager()
		{
			columns = new List<ColumnHeader>();
			AddColumn(Properties.Settings.Default.DirectoryColumn1_name, Properties.Settings.Default.DirectoryColumn1_size);
			AddColumn(Properties.Settings.Default.DirectoryColumn2_name, Properties.Settings.Default.DirectoryColumn2_size);
			AddColumn(Properties.Settings.Default.DirectoryColumn3_name, Properties.Settings.Default.DirectoryColumn3_size);

		}

		public void AddColumn(string name, int width)
		{
			var ch = new ColumnHeader();
			ch.Text = name;
			ch.Width = width;

			columns.Add(ch);
		}

		public void AddSubItem(FileSystemInfo file, ListViewItem item)
		{
			if (file is DirectoryInfo)
			{
				AddDirectorySubItems((DirectoryInfo)file, item);
			}
			else if (file is FileInfo)
			{
				AddFileSubItems((FileInfo)file, item);
			}
		}

		private void AddDirectorySubItems(DirectoryInfo file, ListViewItem item)
		{
			item.SubItems.Add("");
			item.SubItems.Add(file.CreationTime.ToLongDateString());
		}

		private void AddFileSubItems(FileInfo file, ListViewItem item)
		{
			item.SubItems.Add(file.Length.ToString());
			item.SubItems.Add(file.CreationTime.ToLongDateString());
		}

		public void ClearColumns()
		{
			columns.Clear();
		}

		public void SetColumns(ListView listView)
		{
			listView.Columns.Clear();
			listView.Columns.AddRange(columns.ToArray());
		}
	}
}