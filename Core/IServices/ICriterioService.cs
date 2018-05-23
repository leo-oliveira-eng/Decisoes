using System.Collections.Generic;
using Core.Enuns;

namespace Core.IServices
{

    public interface ICriterioService
    {
        
        Resposta Cadastrar(int idProjeto, TipoDeDados tipo, TipoDeCriterio tipoDeCriterio, decimal peso, string nome);
        Resposta Atualizar(int id, TipoDeDados tipo, TipoDeCriterio tipoDeCriterio, decimal peso, string nome);
        List<Criterio> SelecionarCriterios(int idProjeto);
        Criterio SelecionarCriterio(int id);
        List<double> ObterPesos(int idProjeto);
        void Deletar(int id);
        List<double> CriaVetorDePesosPonderados(int idProjeto);
        List<Criterio> SelecionarCriterios(Projeto projeto);
    }

}