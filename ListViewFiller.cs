using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer
{
	public class ListViewFiller
	{
		protected ListViewManager listViewManager;
		public event EventHandler LoadingFinished, LoadingStarted;

		public ListViewFiller(ListViewManager listViewManager)
		{
			this.listViewManager = listViewManager;
		}

		protected void OnLoadingFinished()
		{
			LoadingFinished?.Invoke(this, new EventArgs());
		}

		protected void OnLoadingStarted()
		{
			LoadingStarted?.Invoke(this, new EventArgs());
		}
	}
}
