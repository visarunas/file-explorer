using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace FileExplorer
{
	public interface IColumnManager 
	{
		List<ColumnHeader> Columns { get; }

		void SetColumns(ListView listView);

		void AddColumn(string name, int width);

		void ClearColumns();

		void AddSubItem(FileSystemInfo file, ListViewItem item);

		void UpdateColumnWidths();
	}
}