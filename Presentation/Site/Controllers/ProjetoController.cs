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
    public class ProjetoController : Controller
    {
        private IProjetoService _projetosService;
        private ILogServices _logService;

        public ProjetoController(IProjetoService projetoService, ILogServices logService) {
            _projetosService = projetoService;
            _logService = logService;
        }

        public IActionResult Index(int id)
        {
            @ViewBag.Id = id;
            return View();
        }

    }
}