using System;
using System.Text;
using System.IO;

namespace ParameterReferenceBook
{
    class Logging : ILogging
    {
        private static Logging _instance;

        private Logging()
        { }

        public static Logging GetInstance()
        {
            if (_instance == null)
                _instance = new Logging();
            return _instance;
        }

        public void WriteInLog(String eventInfo)
        {
           using (StreamWriter sw = new StreamWriter("ParameterReferenceBook.log", true, Encoding.UTF8))
           {
                sw.WriteLine(DateTime.Now.ToString() + " " + eventInfo);
           }
        }
    }
}
