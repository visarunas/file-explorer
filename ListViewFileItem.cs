using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileExplorer
{
	public class ListViewFileItem : ListViewItem
	{
		public System.IO.FileAttributes Attributes { get; set; }
		public long Size { get; set; } = 0;
		public DateTime CreationDate { get; set; }

		public ListViewFileItem(string text) : base(text) { }
	}
}
