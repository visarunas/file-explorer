using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FileExplorer
{
	public class DirectoryColumnManager : IColumnManager
	{
		private List<ColumnHeader> columns;

		public DirectoryColumnManager()
		{
			columns = new List<ColumnHeader>();
		}

		public void AddColumn(string name, int width)
		{
			var ch = new ColumnHeader();
			ch.Text = name;
			ch.Width = width;

			columns.Add(ch);
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