using System.Collections;
using System.Collections.Generic;

namespace Core.IServices
{
    public interface IMatrizDeDecisaoService
    {
        void Adicionar(List<ItemMatrizDeDecisao> Matriz);
        void Atualizar(List<ItemMatrizDeDecisao> Matriz);
        void Atualizar(Hashtable Matriz);
        List<ItemMatrizDeDecisao> Selecionar(int idProjeto);
        ItemMatrizDeDecisao SelecionarItem(int id);
        void GerarMatriz(int idProjeto);
        List<ItemMatrizDeDecisao> MatrizNormalizadaPonderada(List<ItemMatrizDeDecisao> matrizNormalizada, List<Criterio> criterios);
        bool ExisteMatriz(int idProjeto);
        void DeletarAlternativas(int idAlternativa);
        void GerarResultado(List<Alternativa> alternativas, List<double> DistanciaEuclidianaDoVetorPositivo, List<double> DistanciaEuclidianaDoVetorNegativo);
        void DeletarCriterios(int id);
        int Selecionar(Projeto projeto, Alternativa alternativa, Criterio criterio);
        List<Alternativa> ProcessarMatriz(int idProjeto);
    }
}