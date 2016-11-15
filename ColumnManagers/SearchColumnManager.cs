﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileExplorer.ColumnManagers
{
	public class SearchColumnManager : IColumnManager
	{
		public List<ColumnHeader> Columns { get; }

		public SearchColumnManager()
		{
			Columns = new List<ColumnHeader>();
			AddColumn(Properties.Settings.Default.DirectoryColumn1_name, Properties.Settings.Default.DirectoryColumn1_size);
			AddColumn(Properties.Settings.Default.SearchColumn2_name, Properties.Settings.Default.SearchColumn2_size);
		}

		public void AddColumn(string name, int width)
		{
			var ch = new ColumnHeader();
			ch.Text = name;
			ch.Width = width;

			Columns.Add(ch);
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
			item.SubItems.Add(file.FullName);
		}

		private void AddFileSubItems(FileInfo file, ListViewItem item)
		{
			item.SubItems.Add(file.FullName);
		}

		public void ClearColumns()
		{
			Columns.Clear();
		}

		public void SetColumns(ListView listView)
		{
			listView.Columns.Clear();
			listView.Columns.AddRange(Columns.ToArray());
		}

		public void UpdateColumnWidths()
		{
			Properties.Settings.Default.DirectoryColumn1_size = Columns[0].Width;
			Properties.Settings.Default.SearchColumn2_size = Columns[1].Width;
		}
	}
}
