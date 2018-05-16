using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;

namespace Authenticator
{
    public static class Identifier
    {
        public static string GetIP()
        {
            int tries = 2;

            string externalIP = "IP not found";

            for (int i = tries; i > 0; i--)
            {
                try
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://checkip.dyndns.org/");
                    request.Timeout = 20000;
                    request.ReadWriteTimeout = 20000;
                    HttpWebResponse wresp = (HttpWebResponse)request.GetResponse();

                    externalIP = new StreamReader(wresp.GetResponseStream()).ReadToEnd();

                    //externalIP = (new WebClient()).DownloadString();
                    externalIP = (new Regex(@"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}")).Matches(externalIP)[0].ToString();
                    break;
                }
                catch { }
            }

            return externalIP;
        }

        public static string GetProcID()
        {
            ManagementObjectSearcher searcher =
                 new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

            foreach (ManagementObject queryObj in searcher.Get())
            {
                if (queryObj["ProcessorId"] != null)
                {
                    return queryObj["ProcessorId"].ToString();
                }
            }

            return "ProcessorId not found";
        }

        public static string GetMac()
        {
            ManagementClass oMClass = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection colMObj = oMClass.GetInstances();
            foreach (ManagementObject objMO in colMObj)
            {
                if (objMO["MacAddress"] != null)
                {
                    return objMO["MacAddress"].ToString();
                }
            }

            return "MacAddress not found";
        }

        public static string GetUser()
        {
            string[] id = WindowsIdentity.GetCurrent().Name.Split("\\".ToCharArray());
            return id[1];
        }

        public static string GetMachine()
        {
            string[] id = WindowsIdentity.GetCurrent().Name.Split("\\".ToCharArray());
            return id[0];
        }
    }
}
