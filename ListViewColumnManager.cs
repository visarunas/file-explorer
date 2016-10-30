using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileExplorer
{
	class ListViewColumnManager
	{
		private ListView listView;

		public ListViewColumnManager(ListView listView)
		{
			this.listView = listView;
		}

		public void addColumn(string name, int width)
		{
			var ch = new ColumnHeader();
			ch.Text = name;
			ch.Width = width;
			listView.Columns.Add(ch);
		}


	}
}
