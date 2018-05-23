using System.Collections.Generic;
using Core.Enuns;

namespace Core.IRepository {

    public interface ICriterioRepository {

        void Add(Projeto projeto, TipoDeDados tipoDeDados, TipoDeCriterio tipoDeCriterio, decimal Peso, string nome);
        void Update(Criterio criterio);
        List<Criterio> Select(int idProjeto);
        Criterio SelectItem(int id);
        void Delete(Criterio criterio);
        int Quantidade(int id);
        List<Criterio> Select(Projeto projeto);
        bool Existe(string nome, Projeto projeto);
    }

}