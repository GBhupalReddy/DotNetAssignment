namespace BookMyShow.Core.Exceptions
{
    [Serializable]
    public class AppException : Exception
    {
        public AppException() : base() { }
        protected AppException(System.Runtime.Serialization.SerializationInfo info,System.Runtime.Serialization.StreamingContext context) : base(info,context)
        { 

        }

        public AppException(string message) : base(message) 
        {
        }
    }
}
