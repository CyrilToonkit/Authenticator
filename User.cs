using System;
using System.Collections.Generic;
using System.Text;

namespace Authenticator
{
    public enum Privileges
    {
        Admin, PowerUser, Operator, None
    }

    public class User
    {
        public User()
        {
            Name = Identifier.GetUser();
        }

        string name = "";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        Privileges privilege = Privileges.None;
        public Privileges Privilege
        {
            get { return privilege; }
            set { privilege = value; }
        }
    }
}
