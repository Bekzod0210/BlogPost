using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Application.Exceptions
{
    public class LoginException : Exception
    {
        private const string _message = "UserName or Password is wrong";

        public LoginException()
            :base( _message) { }

        public LoginException(Exception innerException)
            :base(_message, innerException) { }

    }
}
