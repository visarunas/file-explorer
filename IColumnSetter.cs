﻿using System.Windows.Forms;

namespace FileExplorer
{
	public interface IColumnManager
	{
		void SetColumns(ListView listView);

		void AddColumn(string name, int width);

		void ClearColumns();

	}
}