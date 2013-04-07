using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Emdoor.Barcode
{
    class SerialReader
    {
        private Dictionary<string, string> result;

        public SerialReader(string fileName)
        {
            if (File.Exists(fileName))
            {
                StreamReader sr = new StreamReader(fileName);
                result = new Dictionary<string, string>();

                  for(string line=sr.ReadLine();line!=null;line=sr.ReadLine()) {

                      string [] string_array= line.Split(':');
                      if (string_array != null && string_array.Length == 2)
                      {
                          result.Add(string_array[0], string_array[1]);
                      }
                }
                  sr.Close();

            }
            else
            {

            }
        }


        public string GetIMEI()
        {

            if (result != null&&result.ContainsKey("IMEI"))
            {
                return result["IMEI"];
            }
            return null;
        }

        public string GetSN()
        {
            if (result != null && result.ContainsKey("SN"))
            {
                return result["SN"];
            }
            return null;
        }

        public string GetWifiMAC()
        {
            if (result != null && result.ContainsKey("WIFI_MAC"))
            {
                return result["WIFI_MAC"];
            }
            return null;
        }

    }
}
