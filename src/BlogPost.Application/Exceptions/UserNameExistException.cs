namespace BlogPost.Application.Exceptions
{
    public class UserNameExistException : Exception
    {
        private const string _message = "This userName is already exist";

        public UserNameExistException()
            : base(_message) { }
    }
}
