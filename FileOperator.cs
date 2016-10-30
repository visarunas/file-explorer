using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileExplorer
{
	public class FileOperator
	{

		public FileOperator()
		{

		}

		public void OpenFile(string filePath)
		{
			System.Diagnostics.Process.Start(filePath);
		}

	}
}
