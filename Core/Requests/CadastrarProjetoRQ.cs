using System;

namespace Core.ViewModels
{
    public class CadastrarProjetoRQ {

        private string _Nome;
        private string _Descricao;
        
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

        public CadastrarProjetoRQ()
        {
            
        }


    }
}