using System.Collections.Generic;

namespace Core.IRepository {

    public interface IAlternativaRepository {

        void Add(Projeto projeto, string nome);
        void Update(Alternativa alternativa);
        List<Alternativa> Select(int idProjeto);
        Alternativa SelectAlternativa(int idAlternativa);
        bool Exists(string nome, Projeto projeto);
        void Delete(Alternativa alternativa);
        int Quantidade(int id);
        void Excluir(Alternativa alternativa);
        List<Alternativa> Select(Projeto projeto);
    }

}