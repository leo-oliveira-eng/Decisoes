using Microsoft.VisualStudio.TestTools.UnitTesting;
using Core;
using Core.Enuns;
using Core.IServices;
using Core.IRepository;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using Moq;
using System.Linq;

namespace Services.Test
{
    [TestClass]
    public class UnitTest1
    {

        public Projeto _Projeto;
        public List<Alternativa> _Alternativas;
        private List<Criterio> _Criterios;
        private List<ItemMatrizDeDecisao> _Matriz;

        [TestInitialize()]
        public void Initialize()
        {
            _Projeto = CriarProjetoFake();
            _Alternativas = CriarAlternativasFake();
            _Criterios = CriarCriteriosFake();
            _Matriz = CarregarMatrizFake();
        }

        private Projeto CriarProjetoFake()
            => new Projeto()
            {
                Id = 1,
                Nome = "Aquisição de Carro",
                DataCadastro = DateTime.Now,
                Descricao = "Aquisição de novos modelos de carro para a secretaria de seguran�a do munic�pio do Rio de Janeiro"
            };

        private List<Criterio> CriarCriteriosFake()
            => new List<Criterio>()
            {
                new Criterio() { Id = 1, Projeto = _Projeto, Nome = "Consumo", TipoDeDados = TipoDeDados.Inteiro, TipoDeCriterio = TipoDeCriterio.Beneficio, Peso = 7M },
                new Criterio() { Id = 2, Projeto = _Projeto, Nome = "Conforto", TipoDeDados = TipoDeDados.Inteiro, TipoDeCriterio = TipoDeCriterio.Beneficio, Peso = 3M },
                new Criterio() {Id = 3, Projeto = _Projeto, Nome = "Preço", TipoDeDados = TipoDeDados.Inteiro, TipoDeCriterio = TipoDeCriterio.Custo, Peso = 9M},
                new Criterio() {Id = 4, Projeto = _Projeto, Nome = "Reputacao da Marca", TipoDeDados = TipoDeDados.Inteiro, TipoDeCriterio = TipoDeCriterio.Beneficio, Peso = 2M}
            };

        private List<Alternativa> CriarAlternativasFake()
            => new List<Alternativa>()
            {
                new Alternativa() {Id = 1, Nome = "Palio", Projeto = _Projeto },
                new Alternativa() {Id = 2, Nome = "HB20", Projeto = _Projeto},
                new Alternativa() {Id = 3, Nome = "Corola", Projeto = _Projeto}
            };

        public List<ItemMatrizDeDecisao> CarregarMatrizFake()
            => new List<ItemMatrizDeDecisao>() {
                    new ItemMatrizDeDecisao() { Id = 1, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[0], Valor = "15" },
                    new ItemMatrizDeDecisao() { Id = 2, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[0], Valor = "12" },
                    new ItemMatrizDeDecisao() { Id = 3, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[0], Valor = "10" },
                    new ItemMatrizDeDecisao() { Id = 4, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[1], Valor = "6" },
                    new ItemMatrizDeDecisao() { Id = 5, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[1], Valor = "7" },
                    new ItemMatrizDeDecisao() { Id = 6, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[1], Valor = "9" },
                    new ItemMatrizDeDecisao() { Id = 7, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[2], Valor = "25000" },
                    new ItemMatrizDeDecisao() { Id = 8, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[2], Valor = "35000" },
                    new ItemMatrizDeDecisao() { Id = 9, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[2], Valor = "55000"},
                    new ItemMatrizDeDecisao() { Id = 10, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[3], Valor = "7"},
                    new ItemMatrizDeDecisao() { Id = 11, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[3], Valor = "8"},
                    new ItemMatrizDeDecisao() { Id = 12, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[3], Valor = "9"}
            };

        private List<ItemMatrizDeDecisao> MatrizComUmCriterioInvalidoFake()
            => new List<ItemMatrizDeDecisao>() {
                    new ItemMatrizDeDecisao() { Id = 1, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[0], Valor = "15" },
                    new ItemMatrizDeDecisao() { Id = 2, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[0], Valor = "12" },
                    new ItemMatrizDeDecisao() { Id = 3, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[0], Valor = "10" },
                    new ItemMatrizDeDecisao() { Id = 4, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[1], Valor = "6" },
                    new ItemMatrizDeDecisao() { Id = 5, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[1], Valor = "7" },
                    new ItemMatrizDeDecisao() { Id = 6, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[1], Valor = "9" },
                    new ItemMatrizDeDecisao() { Id = 7, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[2], Valor = "25000" },
                    new ItemMatrizDeDecisao() { Id = 8, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[2], Valor = "35000" },
                    new ItemMatrizDeDecisao() { Id = 9, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[2], Valor = "55000"},
                    new ItemMatrizDeDecisao() { Id = 10, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[3], Valor = "7"},
                    new ItemMatrizDeDecisao() { Id = 11, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[3], Valor = "8"},
                    new ItemMatrizDeDecisao() { Id = 12, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = new Criterio(), Valor = "9"}
            };

        private List<ItemMatrizDeDecisao> MatrizComUmaAlternativaInvalidaFake()
            => new List<ItemMatrizDeDecisao>() {
                    new ItemMatrizDeDecisao() { Id = 1, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[0], Valor = "15" },
                    new ItemMatrizDeDecisao() { Id = 2, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[0], Valor = "12" },
                    new ItemMatrizDeDecisao() { Id = 3, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[0], Valor = "10" },
                    new ItemMatrizDeDecisao() { Id = 4, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[1], Valor = "6" },
                    new ItemMatrizDeDecisao() { Id = 5, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[1], Valor = "7" },
                    new ItemMatrizDeDecisao() { Id = 6, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[1], Valor = "9" },
                    new ItemMatrizDeDecisao() { Id = 7, Projeto = _Projeto, Alternativa = new Alternativa(), Criterio = _Criterios[2], Valor = "25000" },
                    new ItemMatrizDeDecisao() { Id = 8, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[2], Valor = "35000" },
                    new ItemMatrizDeDecisao() { Id = 9, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[2], Valor = "55000"},
                    new ItemMatrizDeDecisao() { Id = 10, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[3], Valor = "7"},
                    new ItemMatrizDeDecisao() { Id = 11, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[3], Valor = "8"},
                    new ItemMatrizDeDecisao() { Id = 12, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[3], Valor = "9"}
            };

        private List<ItemMatrizDeDecisao> MatrizComUmProjetoInvalidoFake()
            => new List<ItemMatrizDeDecisao>() {
                    new ItemMatrizDeDecisao() { Id = 1, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[0], Valor = "15" },
                    new ItemMatrizDeDecisao() { Id = 2, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[0], Valor = "12" },
                    new ItemMatrizDeDecisao() { Id = 3, Projeto = new Projeto(), Alternativa = _Alternativas[2], Criterio = _Criterios[0], Valor = "10" },
                    new ItemMatrizDeDecisao() { Id = 4, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[1], Valor = "6" },
                    new ItemMatrizDeDecisao() { Id = 5, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[1], Valor = "7" },
                    new ItemMatrizDeDecisao() { Id = 6, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[1], Valor = "9" },
                    new ItemMatrizDeDecisao() { Id = 7, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[2], Valor = "25000" },
                    new ItemMatrizDeDecisao() { Id = 8, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[2], Valor = "35000" },
                    new ItemMatrizDeDecisao() { Id = 9, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[2], Valor = "55000"},
                    new ItemMatrizDeDecisao() { Id = 10, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[3], Valor = "7"},
                    new ItemMatrizDeDecisao() { Id = 11, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[3], Valor = "8"},
                    new ItemMatrizDeDecisao() { Id = 12, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[3], Valor = "9"}
            };

        private List<ItemMatrizDeDecisao> MatrizComUmIdInvalidoFake()
           => new List<ItemMatrizDeDecisao>() {
                    new ItemMatrizDeDecisao() { Id = 1, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[0], Valor = "15" },
                    new ItemMatrizDeDecisao() { Id = 2, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[0], Valor = "12" },
                    new ItemMatrizDeDecisao() { Id = 3, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[0], Valor = "10" },
                    new ItemMatrizDeDecisao() { Id = -13, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[1], Valor = "6" },
                    new ItemMatrizDeDecisao() { Id = 5, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[1], Valor = "7" },
                    new ItemMatrizDeDecisao() { Id = 6, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[1], Valor = "9" },
                    new ItemMatrizDeDecisao() { Id = 7, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[2], Valor = "25000" },
                    new ItemMatrizDeDecisao() { Id = 8, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[2], Valor = "35000" },
                    new ItemMatrizDeDecisao() { Id = 9, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[2], Valor = "55000"},
                    new ItemMatrizDeDecisao() { Id = 10, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[3], Valor = "7"},
                    new ItemMatrizDeDecisao() { Id = 11, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[3], Valor = "8"},
                    new ItemMatrizDeDecisao() { Id = 12, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[3], Valor = "9"}
           };

        private List<ItemMatrizDeDecisao> MatrizComUmValorInvalidoFake()
           => new List<ItemMatrizDeDecisao>() {
                    new ItemMatrizDeDecisao() { Id = 1, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[0], Valor = "15" },
                    new ItemMatrizDeDecisao() { Id = 2, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[0], Valor = "12" },
                    new ItemMatrizDeDecisao() { Id = 3, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[0], Valor = "10" },
                    new ItemMatrizDeDecisao() { Id = 4, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[1], Valor = "6" },
                    new ItemMatrizDeDecisao() { Id = 5, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[1], Valor = "teste" },
                    new ItemMatrizDeDecisao() { Id = 6, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[1], Valor = "9" },
                    new ItemMatrizDeDecisao() { Id = 7, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[2], Valor = "25000" },
                    new ItemMatrizDeDecisao() { Id = 8, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[2], Valor = "35000" },
                    new ItemMatrizDeDecisao() { Id = 9, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[2], Valor = "55000"},
                    new ItemMatrizDeDecisao() { Id = 10, Projeto = _Projeto, Alternativa = _Alternativas[0], Criterio = _Criterios[3], Valor = "7"},
                    new ItemMatrizDeDecisao() { Id = 11, Projeto = _Projeto, Alternativa = _Alternativas[1], Criterio = _Criterios[3], Valor = "8"},
                    new ItemMatrizDeDecisao() { Id = 12, Projeto = _Projeto, Alternativa = _Alternativas[2], Criterio = _Criterios[3], Valor = "9"}
           };

        private List<double> VetorDeDistanciasNegativasFake()
            => new List<double>() { 0.199579, 0.127232, 0.035965 };

        private List<double> VetorDeDistanciasPositivasFake()
            => new List<double>() { 0.035965, 0.080239, 0.199579 };

        [TestMethod]
        public void Projeto_Nao_Pode_Ter_Campo_Vazio()
        {
            var projeto = new Projeto()
            {
                Id = 1,
                Nome = null,
                Descricao = null,
                DataCadastro = DateTime.Now
            };
            Assert.IsFalse(projeto.EhValido());
        }

        [TestMethod]
        public void Projeto_Valido()
        {
            var projeto = new Projeto()
            {
                Id = 1,
                Nome = "Teste",
                Descricao = null,
                DataCadastro = DateTime.Now
            };
            Assert.IsTrue(projeto.EhValido());
        }

        [TestMethod]
        public void Criterio_Valido()
        {
            var criterio = new Criterio()
            {
                Id = 4,
                Projeto = _Projeto,
                Nome = "Qualquer",
                TipoDeCriterio = TipoDeCriterio.Beneficio,
                TipoDeDados = TipoDeDados.Inteiro,
                Peso = 10
            };
            Assert.IsTrue(criterio.CriterioEhValido());
        }

        [TestMethod]
        public void Alternativa_Valida()
        {
            var alternativa = new Alternativa()
            {
                Id = 1,
                Nome = "ssssssaef",
                Projeto = _Projeto
            };
            Assert.IsTrue(alternativa.AlternativaEhValida());
        }

        [TestMethod]
        public void Valor_Valido()
        {
            var item = new ItemMatrizDeDecisao()
            {
                Valor = "15",
                Criterio = new Criterio()
                {
                    TipoDeDados = TipoDeDados.Inteiro,
                    TipoDeCriterio = TipoDeCriterio.Custo,
                    Nome = "Teste",
                    Peso = 1M,
                    Projeto = new Projeto()
                    {
                        Nome = "Um",
                        Descricao = "",
                        DataCadastro = DateTime.Now
                    }
                }
            };
            string erro = "";
            Assert.IsTrue(item.ValorEhValido(ref erro));
        }

        [TestMethod]
        public void Valor_InValido()
        {
            var item = new ItemMatrizDeDecisao()
            {
                Valor = "as",
                Criterio = new Criterio()
                {
                    TipoDeDados = TipoDeDados.Inteiro,
                    TipoDeCriterio = TipoDeCriterio.Custo,
                    Nome = "Teste",
                    Peso = 1M,
                    Projeto = new Projeto()
                    {
                        Nome = "Um",
                        Descricao = "",
                        DataCadastro = DateTime.Now
                    }
                }
            };
            string erro = "";
            Assert.IsFalse(item.ValorEhValido(ref erro));
        }

        [TestMethod]
        public void Matriz_Valida()
        {
            var matrizDecisaoRepository = new Mock<IMatrizDeDecisaoRepository>();
            matrizDecisaoRepository.Setup(x => x.Add(It.IsAny<ItemMatrizDeDecisao>()));
            matrizDecisaoRepository.Setup(x => x.Update(It.IsAny<ItemMatrizDeDecisao>()));
            var matrizService = new MatrizDeDecisaoService(matrizDecisaoRepository.Object, null, null, null);
            matrizService.Adicionar(_Matriz);
            Assert.IsTrue(matrizService.ErroValidacaoMatriz.Equals(string.Empty));
        }

        [TestMethod]
        public void Matriz_Com_Um_Criterio_Invalido()
        {
            var matrizDecisaoRepository = new Mock<IMatrizDeDecisaoRepository>();
            matrizDecisaoRepository.Setup(x => x.Add(It.IsAny<ItemMatrizDeDecisao>()));
            matrizDecisaoRepository.Setup(x => x.Update(It.IsAny<ItemMatrizDeDecisao>()));
            var matrizService = new MatrizDeDecisaoService(matrizDecisaoRepository.Object, null, null, null);
            List<ItemMatrizDeDecisao> matriz = MatrizComUmCriterioInvalidoFake();
            matrizService.Adicionar(matriz);
            Assert.IsTrue(matrizService.ErroValidacaoMatriz.Equals("Item 12: Critério Inválido."));
        }

        [TestMethod]
        public void Matriz_Com_Uma_Alternativa_Invalida()
        {
            var matrizDecisaoRepository = new Mock<IMatrizDeDecisaoRepository>();
            matrizDecisaoRepository.Setup(x => x.Add(It.IsAny<ItemMatrizDeDecisao>()));
            matrizDecisaoRepository.Setup(x => x.Update(It.IsAny<ItemMatrizDeDecisao>()));
            var matrizService = new MatrizDeDecisaoService(matrizDecisaoRepository.Object, null, null, null);
            List<ItemMatrizDeDecisao> matriz = MatrizComUmaAlternativaInvalidaFake();
            matrizService.Adicionar(matriz);
            Assert.IsTrue(matrizService.ErroValidacaoMatriz.Equals("Item 7: Alternativa inválida."));
        }

        [TestMethod]
        public void Matriz_Com_Um_Projeto_Invalido()
        {
            var matrizDecisaoRepository = new Mock<IMatrizDeDecisaoRepository>();
            matrizDecisaoRepository.Setup(x => x.Add(It.IsAny<ItemMatrizDeDecisao>()));
            matrizDecisaoRepository.Setup(x => x.Update(It.IsAny<ItemMatrizDeDecisao>()));
            var matrizService = new MatrizDeDecisaoService(matrizDecisaoRepository.Object, null, null, null);
            List<ItemMatrizDeDecisao> matriz = MatrizComUmProjetoInvalidoFake();
            matrizService.Adicionar(matriz);
            Assert.IsTrue(matrizService.ErroValidacaoMatriz.Equals("Item 3: Projeto inválido."));
        }

        
        [TestMethod]
        public void Matriz_Com_Um_Id_Invalido()
        {
            var matrizDecisaoRepository = new Mock<IMatrizDeDecisaoRepository>();
            matrizDecisaoRepository.Setup(x => x.Add(It.IsAny<ItemMatrizDeDecisao>()));
            matrizDecisaoRepository.Setup(x => x.Update(It.IsAny<ItemMatrizDeDecisao>()));
            var matrizService = new MatrizDeDecisaoService(matrizDecisaoRepository.Object, null, null, null);
            List<ItemMatrizDeDecisao> matriz = MatrizComUmIdInvalidoFake();
            matrizService.Adicionar(matriz);
            Assert.IsTrue(matrizService.ErroValidacaoMatriz.Equals("Item 4: Id inválido."));
        }

        
        [TestMethod]
        public void Matriz_Com_Um_Item_Invalido()
        {
            var matrizDecisaoRepository = new Mock<IMatrizDeDecisaoRepository>();
            matrizDecisaoRepository.Setup(x => x.Add(It.IsAny<ItemMatrizDeDecisao>()));
            matrizDecisaoRepository.Setup(x => x.Update(It.IsAny<ItemMatrizDeDecisao>()));
            var matrizService = new MatrizDeDecisaoService(matrizDecisaoRepository.Object, null, null, null);
            List<ItemMatrizDeDecisao> matriz = MatrizComUmValorInvalidoFake();
            matrizService.Adicionar(matriz);
            Assert.IsTrue(matrizService.ErroValidacaoMatriz.Equals("Item 5: Valor é inválido."));
        }

        [TestMethod]
        public void Gerar_Pesos_Ponderados()
        {
            var criterioRepository = new Mock<ICriterioRepository>();
            criterioRepository.Setup(x => x.Select(It.IsAny<int>())).Returns(CriarCriteriosFake());
            var criterioService = new CriteriosService(criterioRepository.Object, null);
            var pesos = criterioService.CriaVetorDePesosPonderados(1);
            Assert.IsTrue(criterioService.VetorDePesosEhValido(pesos));
        }

        [TestMethod]
        public void Gerar_Matriz_Sem_Erros()
        {
            var criterioRepository = new Mock<ICriterioRepository>();
            criterioRepository.Setup(x => x.Select(It.IsAny<int>())).Returns(_Criterios);
            var projetoRepository = new Mock<IProjetoRepository>();
            projetoRepository.Setup(x => x.Select(It.IsAny<int>())).Returns(_Projeto);
            var alternativaRepository = new Mock<IAlternativaRepository>();
            alternativaRepository.Setup(x => x.Select(It.IsAny<int>())).Returns(_Alternativas);
            var matrizDeDecisaoRepository = new Mock<IMatrizDeDecisaoRepository>();
            matrizDeDecisaoRepository.Setup(x => x.Add(It.IsAny<ItemMatrizDeDecisao>()));
            var projetoService = new ProjetoService(projetoRepository.Object, alternativaRepository.Object, criterioRepository.Object);
            var alternativasService = new AlternativasService(alternativaRepository.Object, projetoRepository.Object);
            var criterioService = new CriteriosService(criterioRepository.Object, projetoRepository.Object);
            var matrizDeDecisaoService = new MatrizDeDecisaoService(
                matrizDeDecisaoRepository.Object,
                projetoService,
                criterioService,
                alternativasService);
            matrizDeDecisaoService.GerarMatriz(1);
        }

        [TestMethod]
        public void Somatorio_Do_Quadrado_Dos_Valores_Por_Criterio()
        {
            var matrizDeDecisaoService = new MatrizDeDecisaoService(
                null, null, null, null);
            var matriz = _Matriz;
            var somatorio = matrizDeDecisaoService.SomatorioDoQuadradoDosValoresPorCriterio(matriz);
            Assert.IsTrue(somatorio[0].Equals(469));
            Assert.IsTrue(somatorio[1].Equals(166));
            Assert.IsTrue(somatorio[2].Equals(4875000000));
            Assert.IsTrue(somatorio[3].Equals(194));
        }

        [Ignore]
        [TestMethod]
        public void Matriz_Normalizada_Correta()
        {
            //var criterioRepository = new Mock<ICriterioRepository>();
            //criterioRepository.Setup(x => x.Select(It.IsAny<int>())).Returns(CriarCriteriosFake());
            //var criterioService = new CriteriosService(criterioRepository.Object, null);
            //var matrizDeDecisaoRepository = new Mock<IMatrizDeDecisaoRepository>();
            //matrizDeDecisaoRepository.Setup(x => x.Select(It.IsAny<int>())).Returns(_Matriz);
            //var matrizDeDecisaoService = new MatrizDeDecisaoService(
            //    matrizDeDecisaoRepository.Object, null, criterioService, null);
            //var matrizNormalizada = matrizDeDecisaoService.NormalizarMatriz(1);
        }

        [Ignore]
        [TestMethod]
        public void Matriz_Normalizada_Ponderada()
        {
            //var criterioRepository = new Mock<ICriterioRepository>();
            //criterioRepository.Setup(x => x.Select(It.IsAny<int>())).Returns(CriarCriteriosFake());
            //var criterioService = new CriteriosService(criterioRepository.Object, null);
            //var matrizDeDecisaoRepository = new Mock<IMatrizDeDecisaoRepository>();
            //matrizDeDecisaoRepository.Setup(x => x.Select(It.IsAny<int>())).Returns(_Matriz);
            //var matrizDeDecisaoService = new MatrizDeDecisaoService(
            //    matrizDeDecisaoRepository.Object, null, criterioService, null);
            //var matrizNormalizada = matrizDeDecisaoService.NormalizarMatriz(1);
            //var matrizNomalizadaPonderada = matrizDeDecisaoService.MatrizNormalizadaPonderada(matrizNormalizada);
        }

        [Ignore]
        [TestMethod]
        public void Obter_Vetores_De_Maximo_E_Minimo()
        {
            //var criterioRepository = new Mock<ICriterioRepository>();
            //criterioRepository.Setup(x => x.Select(It.IsAny<int>())).Returns(CriarCriteriosFake());
            //var criterioService = new CriteriosService(criterioRepository.Object, null);
            //var matrizDeDecisaoRepository = new Mock<IMatrizDeDecisaoRepository>();
            //matrizDeDecisaoRepository.Setup(x => x.Select(It.IsAny<int>())).Returns(_Matriz);
            //var matrizDeDecisaoService = new MatrizDeDecisaoService(
            //    matrizDeDecisaoRepository.Object, null, criterioService, null);
            //var matrizNormalizada = matrizDeDecisaoService.NormalizarMatriz(1);
            //var matrizNomalizadaPonderada = matrizDeDecisaoService.MatrizNormalizadaPonderada(matrizNormalizada);
            //var vetorIdealPositivo = matrizDeDecisaoService.ObterVetorIdealPositivo(matrizNomalizadaPonderada);
            //var vetorIdealNegativo = matrizDeDecisaoService.ObterVetorIdealNegativo(matrizNomalizadaPonderada);
        }

        [Ignore]
        [TestMethod]
        public void Obter_Distancia_Euclidiana_Do_Vetor_Ideal_Negativo()
        {
            //var criterioRepository = new Mock<ICriterioRepository>();
            //criterioRepository.Setup(x => x.Select(It.IsAny<int>())).Returns(CriarCriteriosFake());
            //var criterioService = new CriteriosService(criterioRepository.Object, null);
            //var matrizDeDecisaoRepository = new Mock<IMatrizDeDecisaoRepository>();
            //matrizDeDecisaoRepository.Setup(x => x.Select(It.IsAny<int>())).Returns(_Matriz);
            //var matrizDeDecisaoService = new MatrizDeDecisaoService(
            //    matrizDeDecisaoRepository.Object, null, criterioService, null);
            //var matrizNormalizada = matrizDeDecisaoService.NormalizarMatriz(1);
            //var matrizNomalizadaPonderada = matrizDeDecisaoService.MatrizNormalizadaPonderada(matrizNormalizada);
            //var vetorIdealPositivo = matrizDeDecisaoService.ObterVetorIdealPositivo(matrizNomalizadaPonderada);
            //var vetorIdealNegativo = matrizDeDecisaoService.ObterVetorIdealNegativo(matrizNomalizadaPonderada);
            //var distanciaEuclidianaDoVetorIdealNegativo = matrizDeDecisaoService.ObterDistanciaEuclidianaDoVetorNegativo(vetorIdealNegativo, 1);
        }

        [Ignore]
        [TestMethod]
        public void Obter_Distancia_Euclidiana_Do_Vetor_Ideal_Positivo()
        {
            //var criterioRepository = new Mock<ICriterioRepository>();
            //criterioRepository.Setup(x => x.Select(It.IsAny<int>())).Returns(CriarCriteriosFake());
            //var criterioService = new CriteriosService(criterioRepository.Object, null);
            //var matrizDeDecisaoRepository = new Mock<IMatrizDeDecisaoRepository>();
            //matrizDeDecisaoRepository.Setup(x => x.Select(It.IsAny<int>())).Returns(_Matriz);
            //var matrizDeDecisaoService = new MatrizDeDecisaoService(
            //    matrizDeDecisaoRepository.Object, null, criterioService, null);
            //var matrizNormalizada = matrizDeDecisaoService.NormalizarMatriz(1);
            //var matrizNomalizadaPonderada = matrizDeDecisaoService.MatrizNormalizadaPonderada(matrizNormalizada);
            //var vetorIdealPositivo = matrizDeDecisaoService.ObterVetorIdealPositivo(matrizNomalizadaPonderada);
            //var vetorIdealNegativo = matrizDeDecisaoService.ObterVetorIdealNegativo(matrizNomalizadaPonderada);
            //var distanciaEuclidianaDoVetorIdealPositivo = matrizDeDecisaoService.ObterDistanciaEuclidianaDoVetor(vetorIdealPositivo, 1);
        }

        [Ignore]
        [TestMethod]
        public void ObterResultadoFinal()
        {
            var matrizDeDecisaoRepository = new Mock<IMatrizDeDecisaoRepository>();
            matrizDeDecisaoRepository.Setup(x => x.Select(It.IsAny<int>())).Returns(_Matriz);
            var matrizDeDecisaoService = new MatrizDeDecisaoService(
                matrizDeDecisaoRepository.Object, null, null, null);
            var vetornegativo = new List<double>() { 0.199579, 0.127232, 0.035965 };
            var vetorPositivo = new List<double>() { 0.035965, 0.080239, 0.199579 };
            //var resultadoFinal = matrizDeDecisaoService.GerarResultado(vetorPositivo, vetornegativo);
        }

    }
}
