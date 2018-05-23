using System;
using System.Collections.Generic;
using Core;
using Core.Enuns;
using Core.IServices;
using Core.IRepository;
using Core.ViewModels;

namespace Services
{
    public class ProjetoService : IProjetoService
    {
        private IProjetoRepository _projetoRepository;
        private IAlternativaRepository _alternativaRepositorio;
        private ICriterioRepository _criterioRepositorio;

        public ProjetoService(IProjetoRepository projetoRepository, IAlternativaRepository alternativaRepository, ICriterioRepository criterioRepository)
        {
            _projetoRepository = projetoRepository;
            _alternativaRepositorio = alternativaRepository;
            _criterioRepositorio = criterioRepository;
        }

        public Resposta CriarProjeto(string nome, string descricao) {
            if(string.IsNullOrEmpty(nome))
                return Resposta.DadosEmBranco;
            if(!(_projetoRepository.Exists(nome))) {
                _projetoRepository.Add(nome, descricao);
                return Resposta.Ok;
            }
            return Resposta.Existe;
        }
        
        public Resposta AtualizarProjeto(int id, string nome) {
            var projeto = _projetoRepository.Select(id);
            if(projeto != null) {
                projeto.Nome = nome;
                _projetoRepository.Update(projeto);
                return Resposta.Ok;
            }
            return Resposta.NaoExiste;
        }

        public List<ExibicaoProjeto> CarregarProjetos(int? ultimoId) {
            var lista = new List<ExibicaoProjeto>();
            var projetos = _projetoRepository.Select();
            foreach (var projeto in projetos)
            {
                lista.Add(new ExibicaoProjeto() {
                    Projeto = projeto,
                    QuantidadeAlternativas = _alternativaRepositorio.Quantidade(projeto.Id),
                    QuantidadeCriterios = _criterioRepositorio.Quantidade(projeto.Id)
                });
            }
            return lista;
        }

        public List<ExibicaoProjeto> CarregarProjetos(string texto) {
            var lista = new List<ExibicaoProjeto>();
            var projetos = _projetoRepository.Select(texto);
            foreach (var projeto in projetos)
            {
                lista.Add(new ExibicaoProjeto() {
                    Projeto = projeto,
                    QuantidadeAlternativas = _alternativaRepositorio.Quantidade(projeto.Id),
                    QuantidadeCriterios = _criterioRepositorio.Quantidade(projeto.Id)
                });
            }
            return lista;
        }

        public List<Projeto> SelecionarProjetos()
            => _projetoRepository.Select();

        public Projeto SelecionarProjeto(int Id)
            => _projetoRepository.Select(Id);

    }
}
