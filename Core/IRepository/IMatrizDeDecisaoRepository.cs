using System.Collections.Generic;

namespace Core.IRepository {

    public interface IMatrizDeDecisaoRepository {

        dynamic Context { get; }

        void Add(ItemMatrizDeDecisao Item);
        void Update(ItemMatrizDeDecisao matrizDeDecisao);
        void Delete(ItemMatrizDeDecisao matrizDeDecisao);
        List<ItemMatrizDeDecisao> Select(int idProjeto);
        List<ItemMatrizDeDecisao> Select(int idProjeto, int idAlternativa);
        List<ItemMatrizDeDecisao> Select(Projeto projeto, List<Alternativa> Alternativas, List<Criterio> Criterios);
        ItemMatrizDeDecisao SelectItem(int Id);
        bool Exists(int idProjeto);
        List<ItemMatrizDeDecisao> Select(Alternativa alternativa);
        List<ItemMatrizDeDecisao> Select(Criterio criterio);
        int SelectItem(Projeto projeto, Alternativa alternativa, Criterio criterio);
        void Atualizar(int id, double valor);
    }

}