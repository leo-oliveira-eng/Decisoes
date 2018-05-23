using System;
using System.Collections.Generic;
using Core.Enuns;

namespace Core
{
    public class Criterio
    {
        private int _Id;
        private string _Nome;
        private TipoDeDados _TipoDeDados;
        private TipoDeCriterio _TipoDeCriterio;
        private decimal _Peso;
        private Projeto _Projeto;
        
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

        public TipoDeDados TipoDeDados
        {
            get { return _TipoDeDados;}
            set { _TipoDeDados = value;}
        }

        public TipoDeCriterio TipoDeCriterio
        {
            get { return _TipoDeCriterio;}
            set { _TipoDeCriterio = value;}
        }

        public decimal Peso
        {
            get { return _Peso;}
            set { _Peso = value;}
        }

        public Projeto Projeto
        {
            get { return _Projeto;}
            set { _Projeto = value;}
        }
        
        public bool CriterioEhValido()
            => Id > 0
            && !string.IsNullOrEmpty(Nome) && Nome.Length <= 50
            && Peso > 0 && Peso <= 10
            && ((TipoDeCriterio == TipoDeCriterio.Beneficio) || (TipoDeCriterio == TipoDeCriterio.Custo))
            && Projeto.EhValido();
            //&& TipoDeDados 

    }
}
