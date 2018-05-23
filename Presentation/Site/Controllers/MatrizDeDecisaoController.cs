using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Core.IServices;
using Core.Enuns;
using Site.Models;
using Core;
using System.Collections;

namespace Site.Controllers
{
    public class MatrizDeDecisaoController : Controller
    {
        private IMatrizDeDecisaoService _matrizDeDecisaoService;
        private ILogServices _logService;
        private IProjetoService _projetoService;
        private IAlternativaService _alternativaService;
        private ICriterioService _criterioService;

        public MatrizDeDecisaoController(IMatrizDeDecisaoService matrizDeDecisaoService, ILogServices logServices, IProjetoService projetoService, ICriterioService criterioService, IAlternativaService alternativaService)
        {
            _matrizDeDecisaoService = matrizDeDecisaoService;
            _logService = logServices;
            _projetoService = projetoService;
            _alternativaService = alternativaService;
            _criterioService = criterioService;
        }

        [HttpGet]
        public string Selecionar(int idProjeto) {
            try {
                return JsonConvert.SerializeObject(
                    _matrizDeDecisaoService.Selecionar(idProjeto)
                );
            } catch(Exception ex) {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpGet]
        public string SelecionarItem(int id) {
            try {
                return JsonConvert.SerializeObject(
                    _matrizDeDecisaoService.SelecionarItem(id)
                );
            } catch(Exception ex) {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpGet]
        public string ExisteMatriz(int id)
        {
            try
            {
                return _matrizDeDecisaoService.ExisteMatriz(id).ToString();
            }
            catch (Exception ex)
            {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpPost]
        public string GerarMatriz(int id)
        {
            try
            {
                _matrizDeDecisaoService.GerarMatriz(id);
                return Resposta.Ok.ToString();
            }
            catch(Exception Ex)
            {
                _logService.Add(Ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpPut]
        public string SalvarMatriz([FromBody] SalvarMatrizRQ salvarMatrizRQ)
        {
            try
            {
                Hashtable matriz = ObterMatriz(salvarMatrizRQ.Matriz);
                _matrizDeDecisaoService.Atualizar(matriz);
                return Resposta.Ok.ToString();
            }
            catch (Exception Ex)
            {
                _logService.Add(Ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        private Hashtable ObterMatriz(List<ItemSalvarMatrizRQ> matriz)
        {
            var lista = new Hashtable();
            foreach (var item in matriz)
            {
                var alternativa = _alternativaService.SelecionarAlternativa(item.IdAlternativa);
                var criterio = _criterioService.SelecionarCriterio(item.IdCriterio);
                var projeto = _projetoService.SelecionarProjeto(item.IdProjeto);
                var Id = _matrizDeDecisaoService.Selecionar(projeto, alternativa, criterio);
                lista.Add(Id, item.Valor);
            }
            return lista;
        }

        [HttpPost]
        public List<Alternativa> GerarResultado(int id)
            => _matrizDeDecisaoService.ProcessarMatriz(id);


    }

}