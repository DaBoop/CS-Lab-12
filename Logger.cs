using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Lab12
{
    [Serializable]
    class Logger
    {
        List<Exception> list = new List<Exception>();
        List<string> TimeStamps = new List<string>();
        int Count { get => list.Count; }
        public Exception this[int index]
        {
            get => list[index];
        }

        public void Add(Exception ex)
        {
            list.Add(ex);
            TimeStamps.Add(System.DateTime.Now.ToString("dd.MM.yyyy hh:mm"));
        }

        public override string ToString()
        {
            string s = "";
            Exception ex;
            for (int i = 0; i < Count-1; i++)
            {
                ex = list[i];
                s += TimeStamps[i] + "  INFO: " + ex.GetType() + "\t" + ex.Message + "\t\t" + ex.TargetSite + "\n";  
            }
           /* foreach (Exception ex in list)
            {
                s+= ex.GetType() + "\t" + ex.Message + "\t\t" + ex.TargetSite + "\n";
            }*/

            return s;
        }

        public void ToFile(string fileName)
        {
            string s = "";
            Exception ex;
            for (int i = 0; i < Count - 1; i++)
            {
                ex = list[i];
                s += TimeStamps[i] + "  INFO: " + ex.GetType() + "\t" + ex.Message + "\t\t" + ex.TargetSite + "\n";
            }
            File.WriteAllText(fileName, s);
        }
    }
}
