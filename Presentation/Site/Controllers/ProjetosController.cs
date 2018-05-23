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
using Core.ViewModels;

namespace Site.Controllers
{
    public class ProjetosController : Controller
    {
        private IProjetoService _projetosService;
        private ILogServices _logService;

        public ProjetosController(IProjetoService projetoService, ILogServices logService) {
            _projetosService = projetoService;
            _logService = logService;
        }

        [HttpGet]
        public string SelecionarProjetos(int? ultimoId) {
            try {
                return JsonConvert.SerializeObject(
                    _projetosService.CarregarProjetos(ultimoId));
            } catch(Exception ex) {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpGet]
        public string Selecionar() {
            try {
                return JsonConvert.SerializeObject(
                    _projetosService.SelecionarProjetos());
            } catch(Exception ex) {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpGet]
        public string SelecionarProjeto(int id) {
            try {
                return JsonConvert.SerializeObject(
                    _projetosService.SelecionarProjeto(id));
            } catch(Exception ex) {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpGet]
        public string SelecionarProjetoPorNome(string nome) {
            try {
                return JsonConvert.SerializeObject(
                    _projetosService.CarregarProjetos(nome));
            } catch(Exception ex) {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpPost]
        public string Adicionar(string Nome, string Descricao) {
            try {
                return _projetosService.CriarProjeto(Nome, Descricao).ToString();
            } catch (Exception ex) {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

        [HttpPut]
        public string Atualizar(int id, string nome) {
            try {
                return _projetosService.AtualizarProjeto(id, nome).ToString();
            } catch (Exception ex) {
                _logService.Add(ex.ToString());
                return Resposta.Erro.ToString();
            }
        }

    }
}