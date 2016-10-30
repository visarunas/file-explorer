using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileExplorer
{
	class ListViewFileItem : ListViewItem
	{
		public System.IO.FileAttributes Attributes { get; set; }

		public ListViewFileItem(string text) : base(text) { }
	}
}
