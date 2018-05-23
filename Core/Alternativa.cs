using System;

namespace Core
{
    public class Alternativa
    {
        private int _Id;
        private Projeto _Projeto;
        private string _Nome;
        private double _Score;

        public int Id
        {
            get { return _Id;}
            set { _Id = value;}
        }

        public Projeto Projeto
        {
            get { return _Projeto; }
            set { _Projeto = value; }
        }

        public string Nome
        {
            get { return _Nome;}
            set { _Nome = value;}
        }

        public double Score
        {
            get { return _Score; }
            set { _Score = value; }
        }


        public bool AlternativaEhValida()
           => Id > 0 
           && !string.IsNullOrEmpty(Nome) && Nome.Length <= 50
           && Projeto.EhValido();
    }
}
