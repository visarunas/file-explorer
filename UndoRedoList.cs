using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace FileExplorer
{
	public class UndoRedoList
	{
		Stack<Action> undoStack = new Stack<Action>();
		Stack<Action> redoStack = new Stack<Action>();

		public UndoRedoList()
		{
		}

		public void AddNext(Action obj)
		{
			undoStack.Push(obj);

			if (redoStack.Count != 0)
			{
				if (redoStack.Peek().Equals(obj))
				{
					redoStack.Pop();
				}
				else
				{
					redoStack.Clear();
				}
			}
		}

		public Action Undo()
		{
			if (undoStack.Count == 1)
			{
				return undoStack.Peek();
			}
			else
			{
				Action obj = undoStack.Pop();
				redoStack.Push(obj);

				return undoStack.Peek();
			}
		}

		public Action Redo()
		{
			if (redoStack.Count != 0)
			{
				Action obj = redoStack.Pop();
				undoStack.Push(obj);

				return obj;
			}
			else
				return undoStack.Peek();
		}
	}
}