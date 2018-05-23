using System.Collections.Generic;

namespace Core.IRepository {

    public interface IProjetoRepository {

        void Add(string nome, string descricao);
        void Update(Projeto Projeto);
        List<Projeto> Select();
        Projeto Select(int Id);
        List<Projeto> Select(string texto);
        bool Exists(string nome);
        void Delete(Projeto projeto);

    }

}