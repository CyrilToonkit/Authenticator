using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Authenticator
{
    public class Token
    {
        public Token(string inValue)
        {
            Value = inValue;
        }

        string _value = "";
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public bool Save(string inPath)
        {
            try
            {
                File.WriteAllText(inPath, _value);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool Load(string inPath)
        {
            try
            {
                _value = File.ReadAllText(inPath);
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        public bool Matches(Machine inMachine, string version)
        {
            return inMachine.getTokenID(version) == _value;
        }
    }
}
