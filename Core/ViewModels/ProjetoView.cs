using System;

namespace Core.ViewModels
{
    public class ExibicaoProjeto {

        private Projeto _Projeto;
        private int _QuantidadeCriterios;
        private int _QuantidadeAlternativas;
        
        public Projeto Projeto
        {
            get { return _Projeto;}
            set { _Projeto = value;}
        }

        public int QuantidadeAlternativas
        {
            get { return _QuantidadeAlternativas;}
            set { _QuantidadeAlternativas = value;}
        }
        
        public int QuantidadeCriterios
        {
            get { return _QuantidadeCriterios;}
            set { _QuantidadeCriterios = value;}
        }
        
    }
}