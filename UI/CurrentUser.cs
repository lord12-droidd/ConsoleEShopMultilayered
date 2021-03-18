using System;
using System.Collections.Generic;
using System.Text;

namespace UI
{
    public enum Rights
    {
        Guest,
        RegistredUser,
        Admin,
    }
    public class CurrentUser
    {
        public string Login { get; set; }
        public Rights CurrentRights { get; set; }
        public CurrentUser(Rights rights)
        {
            CurrentRights = rights;
        }
    }
}
