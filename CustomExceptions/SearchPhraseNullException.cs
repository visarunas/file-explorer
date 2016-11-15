using System;
using System.Runtime.Serialization;

namespace FileExplorer
{
	[Serializable]
	internal class SearchPhraseNullException : Exception
	{
		public SearchPhraseNullException()
		{
		}

		public SearchPhraseNullException(string message) : base(message)
		{
		}

		public SearchPhraseNullException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected SearchPhraseNullException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}