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
    public class AlternativasController : Controller
    {
        private ILogServices _logService;
        private IAlternativaService _alternativaService;
        private IMatrizDeDecisaoService _matrizDeDecisaoService;

        public AlternativasController(IAlternativaService alternativaService, ILogServices logService, IMatrizDeDecisaoService matrizDeDecisaoService)
        {
            _logService = logService;
            _alternativaService = alternativaService;
            _matrizDeDecisaoService = matrizDeDecisaoService;
        }

        [HttpGet]
        public string SelecionarAlternativas(int idProjeto) {
            try {
                return JsonConvert.SerializeObject(
                    _alternativaService.SelecionarAlternativas(idProjeto));
            } catch(Exception ex) {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpGet]
        public string SelecionarAlternativa(int idAlternativa) {
            try {
                return JsonConvert.SerializeObject(
                    _alternativaService.SelecionarAlternativa(idAlternativa));
            } catch(Exception ex) {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpPost]
        public string CadastrarAlternativa([FromBody] SalvarAlternativaRQ salvarAlternativaRQ) {
            try {
                return _alternativaService.Add(
                    salvarAlternativaRQ.Id, 
                    salvarAlternativaRQ.Alternativa).ToString();
            } catch(Exception ex) {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpPost]
        public string SalvarAlternativas([FromBody] SalvarAlternativasRQ Rq) {
            try {
                return _alternativaService.Salvar(Rq.Id, Rq.Alternativas.ToList()).ToString();
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
                _matrizDeDecisaoService.DeletarAlternativas(id);
                _alternativaService.Excluir(id);
                return Resposta.Ok.ToString();
            }
            catch (Exception ex)
            {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

    }
}