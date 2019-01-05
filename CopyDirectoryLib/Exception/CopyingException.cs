namespace CopyDirectoryLib.Exception
{
    public class CopyingException: System.Exception
    {
        public CopyingException()
        {
        }

        public CopyingException(string message)
            : base(message)
        {
        }

        public CopyingException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
