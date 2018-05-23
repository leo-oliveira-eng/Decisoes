using System;
using System.Collections.Generic;

namespace Core
{
    public class Projeto
    {
        private int _Id;
        private string _Nome;
        private string _Descricao;
        private DateTime _DataCadastro;
        
        public int Id
        {
            get { return _Id;}
            set { _Id = value;}
        }

        public string Nome
        {
            get { return _Nome;}
            set { _Nome = value;}
        }

        public string Descricao
        {
            get { return _Descricao;}
            set { _Descricao = value;}
        }

        public DateTime DataCadastro
        {
            get { return _DataCadastro;}
            set { _DataCadastro = value;}
        }

        public bool EhValido() => Id > 0 && !string.IsNullOrEmpty(Nome);
    }
}
