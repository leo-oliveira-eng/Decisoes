using System;
using System.Collections.Generic;
using Core;
using Core.Enuns;
using Core.IServices;
using Core.IRepository;
using System.Linq;

namespace Services
{
    public class CriteriosService : ICriterioService
    {
        private ICriterioRepository _criterioRepository;
        private IProjetoRepository _projetoRepository;

        public CriteriosService(ICriterioRepository  criterioRepository, IProjetoRepository projetoRepository)
        {
            _criterioRepository = criterioRepository;
            _projetoRepository = projetoRepository;
        }

        public Resposta Cadastrar(int idProjeto, TipoDeDados tipoDeDados, TipoDeCriterio tipoDeCriterio, decimal peso, string nome) {
            if(string.IsNullOrEmpty(nome))
                return Resposta.DadosEmBranco;
            var projeto = _projetoRepository.Select(idProjeto);
            if(projeto == null)
                return Resposta.ProjetoNaoExiste;
            if (_criterioRepository.Existe(nome, projeto))
                return Resposta.Existe;
            _criterioRepository.Add(projeto, tipoDeDados, tipoDeCriterio, peso, nome);
            return Resposta.Ok;
        }

        public Resposta Atualizar(int id, TipoDeDados tipoDeDados, TipoDeCriterio tipoDeCriterio, decimal peso, string nome) {
            if(string.IsNullOrEmpty(nome))
                return Resposta.DadosEmBranco;
            var criterio = _criterioRepository.SelectItem(id);
            if(criterio == null)
                return Resposta.CriterioInexistente;
            criterio.Nome = nome;
            criterio.TipoDeDados = tipoDeDados;
            criterio.TipoDeCriterio = tipoDeCriterio;
            criterio.Peso = peso;
            _criterioRepository.Update(criterio);
            return Resposta.Ok;
        }

        public List<double> ObterPesos(int idProjeto)
        {
            var lista = new List<double>();
            var criterios = _criterioRepository
                .Select(idProjeto);
            criterios.ForEach(item => lista.Add(Convert.ToDouble(item.Peso)));
            return lista;
        }

        public List<double> CriaVetorDePesosPonderados(int idProjeto)
        {
            var listaPesos = new List<double>();
            var pesos = ObterPesos(idProjeto);
            double soma = pesos.Sum();
            pesos.ForEach(item => listaPesos.Add((item / soma)));
            if (!VetorDePesosEhValido(listaPesos))
                throw new Exception(
                    string.Format("Vetor de pesos do projeto {0} inválido", 
                        idProjeto.ToString()));
            return listaPesos;
        }

        public bool VetorDePesosEhValido(List<double> pesos)
            => Math.Round(pesos.Sum(), 2) == 1;

        public List<Criterio> SelecionarCriterios(int idProjeto)
            => _criterioRepository.Select(idProjeto);

        public Criterio SelecionarCriterio(int id)
            => _criterioRepository.SelectItem(id);

        public void Deletar(int id)
        {
            var criterio = _criterioRepository.SelectItem(id);
            _criterioRepository.Delete(criterio);
        }

        public List<Criterio> SelecionarCriterios(Projeto projeto)
            => _criterioRepository.Select(projeto);

    }

}
