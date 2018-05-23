using System;
using Microsoft.AspNetCore.Builder;

namespace Site
{
    public class RoutesConfig {
        public static Action<Microsoft.AspNetCore.Routing.IRouteBuilder> GetRoutes()
        {
            return routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "ProjetosCarregar",
                    template: "Projetos/Carregar/{ultimoId?}",
                    defaults: new { controller = "Projetos", action = "SelecionarProjetos" }
                );

                routes.MapRoute(
                    name: "ProjetosCarregarPorNome",
                    template: "Projetos/{nome}",
                    defaults: new { controller = "Projetos", action = "SelecionarProjetoPorNome" }
                );

                routes.MapRoute(
                    name: "ProjetosSelecionar",
                    template: "Projetos/Selecionar",
                    defaults: new { controller = "Projetos", action = "Selecionar" }
                );

                routes.MapRoute(
                    name: "ProjetoSelecionar",
                    template: "Projeto/{id}",
                    defaults: new { controller = "Projetos", action = "SelecionarProjeto" }
                );

                routes.MapRoute(
                    name: "ProjetoCadastrar",
                    template: "Projeto/Cadastrar",
                    defaults: new { controller = "Projetos", action = "Adicionar" }
                );

                routes.MapRoute(
                    name: "ProjetoAtualizar",
                    template: "Projeto/Atualizar",
                    defaults: new { controller = "Projetos", action = "Atualizar" }
                );

                routes.MapRoute(
                    name: "ProjetoCarregar",
                    template: "Projeto/Carregar/{id}",
                    defaults: new { controller = "Projeto", action = "Index" }
                );

                routes.MapRoute(
                    name: "AlternativasSelecionar",
                    template: "Alternativas/{idProjeto}",
                    defaults: new { controller = "Alternativas", action = "SelecionarAlternativas" }
                );

                routes.MapRoute(
                    name: "AlternativaSelecionar",
                    template: "Alternativa/{id}",
                    defaults: new { controller = "Alternativas", action = "SelecionarAlternativa" }
                );

                routes.MapRoute(
                    name: "AlternativaCadastrar",
                    template: "Alternativa/Cadastrar",
                    defaults: new { controller = "Alternativas", action = "CadastrarAlternativa" }
                );

                routes.MapRoute(
                    name: "AlternativasCadastrar",
                    template: "Alternativas/Cadastrar",
                    defaults: new { controller = "Alternativas", action = "SalvarAlternativas" }
                );

                routes.MapRoute(
                    name: "AlternativaAtualizar",
                    template: "Alternativa/Atualizar",
                    defaults: new { controller = "Alternativas", action = "AtualizarAlternativa" }
                );

                routes.MapRoute(
                    name: "AlternativasExcluir",
                    template: "Alternativa/{id}/Excluir",
                    defaults: new { controller = "Alternativas", action = "Deletar" }
                );

                routes.MapRoute(
                    name: "CriteriosSelecionar",
                    template: "Criterios/{id}",
                    defaults: new { controller = "Criterios", action = "ConsultarCriterios" }
                );

                routes.MapRoute(
                    name: "CriterioSelecionar",
                    template: "Criterio/{id}",
                    defaults: new { controller = "Criterios", action = "ConsultarCriterio" }
                );

                routes.MapRoute(
                    name: "CriteriosCadastrar",
                    template: "Criterio/Cadastrar",
                    defaults: new { controller = "Criterios", action = "Cadastrar" }
                );

                routes.MapRoute(
                    name: "CriteriosAtualizar",
                    template: "Criterio/Atualizar",
                    defaults: new { controller = "Criterios", action = "Atualizar" }
                );

                routes.MapRoute(
                    name: "CriteriosDeletar",
                    template: "Criterio/Deletar/{id}",
                    defaults: new { controller = "Criterios", action = "Deletar" }
                );

                routes.MapRoute(
                    name: "MatrizSelecionarTudo",
                    template: "Matriz/{idProjeto}",
                    defaults: new { controller = "MatrizDeDecisao", action = "Selecionar" }
                );

                routes.MapRoute(
                    name: "MatrizExiste",
                    template: "Matriz/{id}/Existe",
                    defaults: new { controller = "MatrizDeDecisao", action = "ExisteMatriz" }
                );

                routes.MapRoute(
                    name: "MatrizSelecionarItem",
                    template: "Matriz/Item/{id}",
                    defaults: new { controller = "MatrizDeDecisao", action = "SelecionarItem" }
                );

                routes.MapRoute(
                    name: "MatrizGerar",
                    template: "Matriz/{id}/Gerar",
                    defaults: new { controller = "MatrizDeDecisao", action = "GerarMatriz" }
                );

                routes.MapRoute(
                    name: "MatrizSalvar",
                    template: "Matriz/Salvar",
                    defaults: new { controller = "MatrizDeDecisao", action = "SalvarMatriz" }
                );

                routes.MapRoute(
                    name: "MatrizGerarResultado",
                    template: "Matriz/{id}/GerarResultado",
                    defaults: new { controller = "MatrizDeDecisao", action = "GerarResultado" }
                );

            };
        }
    }
}