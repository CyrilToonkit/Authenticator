using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Authenticator
{
    public class Machine
    {
        public Machine()
        {
            Name = Identifier.GetMachine();
            Mac = Identifier.GetMac();
            ProcID = Identifier.GetProcID();
        }

        static HashAlgorithm hashAlg = new SHA1Managed();

        string name = "";
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        string mac = "";
        public string Mac
        {
            get { return mac; }
            set { mac = value; }
        }

        string procID = "";
        public string ProcID
        {
            get { return procID; }
            set { procID = value; }
        }

        public static string getTokenID(string inName, string inMac, string inProcID, string version)
        {
            return string.Format("{0}_TOKENARG_{1}_TOKENARG_{2}_TOKENARG_{3}",
                    BitConverter.ToString(hashAlg.ComputeHash(Encoding.ASCII.GetBytes(inName))),
                    BitConverter.ToString(hashAlg.ComputeHash(Encoding.ASCII.GetBytes(inMac))),
                    BitConverter.ToString(hashAlg.ComputeHash(Encoding.ASCII.GetBytes(inProcID))),
                    BitConverter.ToString(hashAlg.ComputeHash(Encoding.ASCII.GetBytes(version))));
        }

        public string getTokenID(string version)
        {
            return Machine.getTokenID(name, mac, procID, version);
        }

        public string getRequestCode(string version)
        {
            return StringCipher.Encrypt(string.Format("{0}_TOKENARG_{1}_TOKENARG_{2}_TOKENARG_{3}", name, mac, procID, version), version);
        }

        public static string readRequestCode(string requestCode, string version)
        {
            return StringCipher.Decrypt(requestCode, version);
        }
    }
}
