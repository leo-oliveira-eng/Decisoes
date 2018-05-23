using System.Collections.Generic;
using Core.Enuns;

namespace Core.IServices
{
    public interface IAlternativaService
    {
        Resposta Add(int idProjeto, string nome);
        Resposta AtualizarAlternativa(int id, string nome);
        List<Alternativa> SelecionarAlternativas(int idProjeto);
        Alternativa SelecionarAlternativa(int id);
        Resposta Salvar(int idProjeto, List<string> list);
        void Excluir(int idAlternativa);
        List<Alternativa> SelecionarAlternativas(Projeto projeto);
        void Atualizar(Alternativa alternativa);
    }
}