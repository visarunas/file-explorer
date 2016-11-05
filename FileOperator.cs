using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			try
			{
				Process.Start(filePath);
			}
			catch(System.ComponentModel.Win32Exception e)
			{
				Debug.WriteLine(e.Message);
			}
			catch(Exception e)
			{
				Debug.WriteLine(e.Message);
			}
			
		}

	}
}
