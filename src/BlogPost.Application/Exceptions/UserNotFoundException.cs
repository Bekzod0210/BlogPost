namespace BlogPost.Application.Exceptions
{
    public class UserNotFoundException : Exception
    {
        private const string _message = "Please sign up for use this action!";

        public UserNotFoundException() : base(_message) { }
    }
}
