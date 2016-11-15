using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;
using static System.Windows.Forms.ListView;

namespace FileExplorer
{
	public class FileOperator
	{
		private ListViewItem[] selectedItems;

		public FileOperator()
		{

		}

		public void SelectFiles(ListViewItem[] selectedItems)
		{
			this.selectedItems = selectedItems;
		}

		public void PasteFile(string pathToPaste)
		{
			Task copyTask = new Task( () => Paste(pathToPaste) );
			copyTask.Start();

		}

		private void Paste(string pathToPaste)
		{
			if (selectedItems != null)
			{
				foreach (ListViewFileItem file in selectedItems)
				{
					if (file.Attributes.HasFlag(FileAttributes.Directory))
					{
						FileSystem.CopyDirectory(file.Name, Path.Combine(pathToPaste, file.Text), UIOption.AllDialogs, UICancelOption.DoNothing);
					}
					else
					{
						FileSystem.CopyFile(file.Name, Path.Combine(pathToPaste, file.Text), UIOption.AllDialogs, UICancelOption.DoNothing);
					}
				}
			}
		}

		public void DeleteFile(ListViewFileItem[] files)
		{
			Task deleteTask = new Task( () => Delete(files) );
			deleteTask.Start();
		}

		private void Delete(ListViewFileItem[] files)
		{
			foreach(ListViewFileItem file in files)
			{
				if (file.Attributes.HasFlag(FileAttributes.Directory))
				{
					FileSystem.DeleteDirectory(file.Name, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
				}
				else
				{
					FileSystem.DeleteFile(file.Name, UIOption.AllDialogs, RecycleOption.SendToRecycleBin);
				}
				
			}	
		}

		public void OpenFile(string filePath)
		{
			Process.Start(filePath);
		}
	}
}
