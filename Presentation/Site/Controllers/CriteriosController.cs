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

namespace Site.Controllers
{
 
    public class CriteriosController : Controller
    {
        private ICriterioService _criterioService;
        private ILogServices _logService;
        private IMatrizDeDecisaoService _matrizDeDecisaoService;

        public CriteriosController(ICriterioService criterioService, ILogServices logServices, IMatrizDeDecisaoService matrizDeDecisaoService)
        {
            _criterioService = criterioService;
            _logService = logServices;
            _matrizDeDecisaoService = matrizDeDecisaoService;
        }

        [HttpGet]
        public string ConsultarCriterios(int id) {
            try {
                return JsonConvert.SerializeObject(
                    _criterioService.SelecionarCriterios(id));
            } catch(Exception ex) {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpGet]
        public string ConsultarCriterio(int id) {
            try {
                return JsonConvert.SerializeObject(
                    _criterioService.SelecionarCriterio(id));
            } catch(Exception ex) {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpPost]
        public string Cadastrar([FromBody] SalvarCriterioRQ RQ) {
            try {
                return _criterioService
                    .Cadastrar(
                        RQ.IdProjeto,
                        0, 
                        (TipoDeCriterio)RQ.TipoDeCriterio,
                        RQ.Peso, 
                        RQ.Nome)
                    .ToString();
            } catch(Exception ex) {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpPut]
        public string Atualizar(int id, int tipoDadosId, int tipoDeCriterioId, decimal peso, string nome) {
            try {
                return _criterioService.Atualizar(
                    id, 
                    (TipoDeDados)tipoDadosId, 
                    (TipoDeCriterio)tipoDeCriterioId, 
                    peso,
                    nome).ToString();
            } catch(Exception ex) {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpDelete]
        public string Deletar(int id)
        {
            try
            {
                _matrizDeDecisaoService.DeletarCriterios(id);
                _criterioService.Deletar(id);
                return "Ok";
            }
            catch(Exception Ex)
            {
                _logService.Add(Ex.ToString());
                return "Erro";
            }
        }

    }

}