namespace BlogPost.Application.Exceptions
{
    public class NameException : Exception
    {
        public NameException(string name)
            : base($"Please enter your {name}'s name!") { }
    }
}
