using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
			if (selectedItems != null)
			{
				foreach (ListViewFileItem item in selectedItems)
				{
					if (item.Attributes.HasFlag(FileAttributes.Directory))
					{
						DirectoryCopy(item.Name, Path.Combine(pathToPaste, item.Text), true);
					}
					else
					{
						File.Copy(item.Name, Path.Combine(pathToPaste, item.Text), true);
					}
				}
			}
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

		private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
		{
			// Get the subdirectories for the specified directory.
			DirectoryInfo dir = new DirectoryInfo(sourceDirName);

			if (!dir.Exists)
			{
				throw new DirectoryNotFoundException(
					"Source directory does not exist or could not be found: "
					+ sourceDirName);
			}

			DirectoryInfo[] dirs = dir.GetDirectories();
			// If the destination directory doesn't exist, create it.
			if (!Directory.Exists(destDirName))
			{
				//Debug.WriteLine("creating" + destDirName);
				Directory.CreateDirectory(destDirName);
			}

			// Get the files in the directory and copy them to the new location.
			FileInfo[] files = dir.GetFiles();
			foreach (FileInfo file in files)
			{
				string temppath = Path.Combine(destDirName, file.Name);
				file.CopyTo(temppath, false);
			}

			// If copying subdirectories, copy them and their contents to new location.
			if (copySubDirs)
			{
				foreach (DirectoryInfo subdir in dirs)
				{
					string temppath = Path.Combine(destDirName, subdir.Name);
					DirectoryCopy(subdir.FullName, temppath, copySubDirs);
				}
			}
		}
	}
}
