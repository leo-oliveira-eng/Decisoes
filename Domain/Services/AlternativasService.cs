using System;
using System.Collections.Generic;
using Core;
using Core.Enuns;
using Core.IServices;
using Core.IRepository;

namespace Services
{
    public class AlternativasService : IAlternativaService
    {
        private IAlternativaRepository _alternativaRepository;
        private IProjetoRepository _projetoRepository;

        public AlternativasService(IAlternativaRepository alternativaRepository, IProjetoRepository projetoRepository)
        {
            _alternativaRepository = alternativaRepository;
            _projetoRepository = projetoRepository;
        }

        public Resposta Add(int idProjeto, string nome) {
            var projeto = _projetoRepository.Select(idProjeto);
            if(projeto == null)
                return Resposta.NaoExiste;
            var alternativa = _alternativaRepository.Exists(nome, projeto);
            if(alternativa)
                return Resposta.Existe;
            _alternativaRepository.Add(projeto, nome);
            return Resposta.Ok;
        }

        public Resposta AtualizarAlternativa(int id, string nome) {
            var alternativa = _alternativaRepository.SelectAlternativa(id);
            if(alternativa == null)
                return Resposta.NaoExiste;
            alternativa.Nome = nome;
            _alternativaRepository.Update(alternativa);
            return Resposta.Ok;
        }

        public List<Alternativa> SelecionarAlternativas(int idProjeto)
            => _alternativaRepository.Select(idProjeto);

        public Alternativa SelecionarAlternativa(int id)
            => _alternativaRepository.SelectAlternativa(id);

        public Resposta Salvar(int idProjeto, List<string> alternativas)
        {
            Excluir(idProjeto);
            alternativas.ForEach(x => Add(idProjeto, x));
            return Resposta.Ok;
        }

        public void Atualizar(Alternativa alternativa)
            => _alternativaRepository.Update(alternativa);

        public void Excluir(int idAlternativa)
        {
            var alternativa = _alternativaRepository.SelectAlternativa(idAlternativa);
            _alternativaRepository.Excluir(alternativa);
        }

        public List<Alternativa> SelecionarAlternativas(Projeto projeto)
            => _alternativaRepository.Select(projeto);
    }
}