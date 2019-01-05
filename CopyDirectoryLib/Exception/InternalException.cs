namespace CopyDirectoryLib.Exception
{
    public class InternalException : System.Exception
    {
        public InternalException()
        {
        }

        public InternalException(string message)
            : base(message)
        {
        }

        public InternalException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }
}
