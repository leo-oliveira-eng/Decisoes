using System;
using System.IO;

namespace Helpers
{
    public class Log
    {
        private string _file;  

        public Log(string file)
        {
            _file = string.Format("{0}{1}", file, NomeLog());
        }

        private string NomeLog()
            => string.Format("{0}{1}{2}.txt",
                DateTime.Now.Year.ToString(),
                DateTime.Now.Month.ToString(),
                DateTime.Now.Day.ToString());

        public void ADD(string texto)
        {
            if (!(File.Exists(_file)))
            {
                var arquivo = new FileStream(_file, FileMode.Create);
                arquivo.Close();
            }
            var registro = new StreamWriter(_file, true);
            registro.WriteLine(DateTime.Now.ToString() + ": " + texto);
            registro.Close();
        }
    }
}
