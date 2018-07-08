using NewsFeeds.Data.Exceptions;
using System;

namespace NewsFeeds.Data.User
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException(string userId) : base(String.Format("User with ${0} id was not found", userId))
        {

        }
    }
}
