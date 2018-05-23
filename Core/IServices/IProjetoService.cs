using System.Collections.Generic;
using Core.Enuns;
using Core.ViewModels;

namespace Core.IServices {

    public interface IProjetoService {

        Resposta CriarProjeto(string nome, string descricao);
        Resposta AtualizarProjeto(int id, string nome);
        List<Projeto> SelecionarProjetos();
        Projeto SelecionarProjeto(int Id);
        List<ExibicaoProjeto> CarregarProjetos(int? ultimoId);
        List<ExibicaoProjeto> CarregarProjetos(string texto);

    }

}