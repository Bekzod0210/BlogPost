using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPost.Application.Exceptions
{
    public class UserNameExistException : Exception
    {
        private const string _message = "This userName is already exist";

        public UserNameExistException() 
            :base(_message) { }
    }
}
