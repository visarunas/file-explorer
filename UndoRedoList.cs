using System.Collections.Generic;

namespace FileExplorer
{
	public class UndoRedoList<T>
	{
		Stack<T> undoStack = new Stack<T>();
		Stack<T> redoStack = new Stack<T>();

		public UndoRedoList()
		{
		}

		public void AddNext(T obj)
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

		public T Undo()
		{
			if (undoStack.Count == 1)
			{
				return undoStack.Peek();
			}
			else
			{
				T obj = undoStack.Pop();
				redoStack.Push(obj);

				return undoStack.Peek();
			}
		}

		public T Redo()
		{
			if (redoStack.Count != 0)
			{
				T obj = redoStack.Pop();
				undoStack.Push(obj);

				return obj;
			}
			else
				return undoStack.Peek();
		}
	}
}