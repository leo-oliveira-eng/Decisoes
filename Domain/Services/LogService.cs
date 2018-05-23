using System;
using Helpers;
using Core.IServices;

namespace Services
{
    public class LogService : ILogServices
    {
        private string _file;
        
        public LogService(string file)
        {
            _file = file;
        }

        public void Add(string Texto)
            => new Log(_file).ADD(Texto);
    }
}