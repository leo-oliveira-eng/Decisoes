using System;
using System.Collections.Generic;
using Core;
using Core.Enuns;
using Core.IServices;
using Core.IRepository;
using Core.Factory;
using Core.ValidacoesDeTipo;

namespace Core
{
    public class ItemMatrizDeDecisao
    {
        private int _Id;
        private Alternativa _Alternativa;
        private Criterio _Criterio;
        private Projeto _Projeto;
        private string _Valor;

        public int Id
        {
            get { return _Id;}
            set { _Id = value;}
        }

        public Alternativa Alternativa
        {
            get { return _Alternativa;}
            set { _Alternativa = value;}
        }
        
        public Criterio Criterio
        {
            get { return _Criterio;}
            set { _Criterio = value;}
        }

        public Projeto Projeto
        {
            get { return _Projeto;}
            set { _Projeto = value;}
        }

        public string Valor
        {
            get { return _Valor;}
            set { _Valor = value;}
        }

        public bool Validar(ref string erro, ref int cont)
            => IdEhValido(ref erro, ref cont)
                && ProJetoEhValido(ref erro)
                && AlternativaEhValida(ref erro)
                && CriterioValido(ref erro)
                && ValorEhValido(ref erro);

        private bool IdEhValido(ref string erro, ref int cont)
        {
            if (Id < 0)
            {
                erro = string.Format("Item {0}: Id inválido.", cont.ToString());
                return false;
            }
            return true;
        }

        private bool CriterioValido(ref string erro)
        {
            if (!Criterio.CriterioEhValido())
            {
                erro = string.Format("Item {0}: Critério Inválido.", Id.ToString());
                return false;
            }
            return true;
        }

        private bool AlternativaEhValida(ref string erro) {
            if(!Alternativa.AlternativaEhValida()) {
                erro = string.Format("Item {0}: Alternativa inválida.", Id.ToString());
                return false;
            }
            return true;
        }

        private bool ProJetoEhValido(ref string erro)
        {
            if (!Projeto.EhValido())
            {
                erro = string.Format("Item {0}: Projeto inválido.", Id.ToString());
                return false;
            }
            return true;
        }

        public bool ValorEhValido(ref string erro)
        {
            if (new ValidacaoTipoFactory()
              .CriarValidacoTipo<IBaseValidacao, TipoDeDados>
                  (Criterio.TipoDeDados)
              .Validar(Valor))
                return true;
            else
            {
               erro = string.Format("Item {0}: Valor é inválido.", Id.ToString());
               return false;
            }
        }

    }
}
