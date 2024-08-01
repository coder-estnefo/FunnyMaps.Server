namespace FunnyMaps.Server.Exceptions
{
    public abstract class ServerException : Exception
    {
        public ServerException(string message) : base(message) { }
    }

    public class UserExistsException : ServerException
    {
        public UserExistsException(string message) : base(message) { }
    }

    public class InvalidLoginDetailsException : ServerException
    {
        public InvalidLoginDetailsException(string message): base(message)
        {
            
        }
    }
}
