using Etier.IconHelper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using System.Windows.Forms;

namespace FileExplorer
{
	public class ListViewManager
	{
		private ListView listView;
		private ImageList imageList;

		public ListViewManager(ListView listView, ImageList imageList)
		{
			this.listView = listView;
			this.imageList = imageList;
		}

		public void SetColumns(IColumnManager columns)
		{
			if (listView.InvokeRequired)
			{
				MethodInvoker d = delegate { SetColumns(columns); };
				listView.Invoke(d, new object[] { columns } );
			}
			else
			{
				columns.SetColumns(listView);
			}
		}

		public void AddListViewItem(ListViewItem item, Icon icon)
		{
			// InvokeRequired required compares the thread ID of the
			// calling thread to the thread ID of the creating thread.
			// If these threads are different, it returns true.
			if (listView.InvokeRequired)
			{
				MethodInvoker d = delegate { AddListViewItem(item, icon); };
				listView.Invoke(d, new object[] { item, icon } );
			}
			else
			{
				imageList.Images.Add(item.ImageKey, icon);
				listView.Items.Add(item);
			}
		}

		public void ClearListView()
		{
			if (listView.InvokeRequired)
			{
				MethodInvoker d = delegate { ClearListView(); };
				listView.Invoke(d, new object[] { } );
			}
			else
			{
				imageList.Images.Clear();
				listView.Clear();

			}
		}

	}
}