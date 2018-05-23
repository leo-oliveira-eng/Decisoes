using System.Collections.Generic;
using Core;
using Core.IServices;
using Core.IRepository;
using System;
using System.Linq;
using Core.Enuns;
using System.Collections;

namespace Services
{

    public class MatrizDeDecisaoService : IMatrizDeDecisaoService
    {
        private IMatrizDeDecisaoRepository _matrizDeDecisaoRepository;
        private ICriterioService _criterioService;
        private IAlternativaService _alternativaService;
        private IProjetoService _projetoService;

        public string ErroValidacaoMatriz { get; private set; }

        public MatrizDeDecisaoService(IMatrizDeDecisaoRepository matrizDeDecisaoRepository, IProjetoService projetoService, ICriterioService criterioService, IAlternativaService alternativaService)
        {
            _matrizDeDecisaoRepository = matrizDeDecisaoRepository;
            _criterioService = criterioService;
            _alternativaService = alternativaService;
            _projetoService = projetoService;
        }

        public void ValidarMatriz(List<ItemMatrizDeDecisao> Matriz)
        {
            string erro = string.Empty;
            ErroValidacaoMatriz = string.Empty;
            int cont = 0;
            foreach (var item in Matriz)
            {
                cont++;
                if (!item.Validar(ref erro, ref cont))
                    break;
            }
            ErroValidacaoMatriz = erro;
        }

        public void Adicionar(List<ItemMatrizDeDecisao> Matriz)
        {
            ValidarMatriz(Matriz);
            if (string.IsNullOrEmpty(ErroValidacaoMatriz))
                Matriz.ForEach(item => _matrizDeDecisaoRepository.Add(item));
        }

        public void DeletarAlternativas(int idAlternativa)
        {
            var alternativa = _alternativaService.SelecionarAlternativa(idAlternativa);
            var itensADeletar = _matrizDeDecisaoRepository.Select(alternativa);
            itensADeletar.ForEach(x => _matrizDeDecisaoRepository.Delete(x));
        }

        public void Atualizar(List<ItemMatrizDeDecisao> Matriz)
        {
            ValidarMatriz(Matriz);
            if (string.IsNullOrEmpty(ErroValidacaoMatriz))
                Matriz.ForEach(x => AtualizarItemDaMatriz(x));
        }

        private void AtualizarItemDaMatriz(ItemMatrizDeDecisao itemMatriz)
        {
            var item = _matrizDeDecisaoRepository.SelectItem(itemMatriz.Id);
            item.Valor = itemMatriz.Valor;
            _matrizDeDecisaoRepository.Update(item);
        }

        public void GerarMatriz(int idProjeto)
        {
            if (ExisteMatriz(idProjeto))
                DeletarMatriz(idProjeto);
            var matriz = new List<ItemMatrizDeDecisao>();
            ErroValidacaoMatriz = string.Empty;
            var projeto = _projetoService.SelecionarProjeto(idProjeto);
            var criterios = _criterioService.SelecionarCriterios(idProjeto);
            var alternativas = _alternativaService.SelecionarAlternativas(idProjeto);
            alternativas.ForEach(alternativa =>
                InputDeCriterios(matriz, projeto, criterios, alternativa));
            ValidarMatriz(matriz);
            if (!string.IsNullOrEmpty(ErroValidacaoMatriz))
                throw new ArgumentException(ErroValidacaoMatriz);
            SalvarMatrizNoBancoDeDados(matriz);
        }

        private void DeletarMatriz(int idProjeto)
        {
            var lista = Selecionar(idProjeto);
            lista.ForEach(x => _matrizDeDecisaoRepository.Delete(x));
        }

        private void SalvarMatrizNoBancoDeDados(List<ItemMatrizDeDecisao> matriz)
            => matriz.ForEach(item => _matrizDeDecisaoRepository.Add(item));

        private void InputDeCriterios(List<ItemMatrizDeDecisao> matriz, Projeto projeto, List<Criterio> criterios, Alternativa alternativa)
            => criterios.ForEach(criterio =>
                            InserirItens(matriz, projeto, alternativa, criterio));

        private void InserirItens(List<ItemMatrizDeDecisao> matriz, Projeto projeto, Alternativa alternativa, Criterio criterio)
            => matriz.Add(new ItemMatrizDeDecisao()
            {
                Alternativa = alternativa,
                Criterio = criterio,
                Projeto = projeto,
                Valor = null
            });

        public List<ItemMatrizDeDecisao> Selecionar(int idProjeto)
        {
            var projeto = _projetoService.SelecionarProjeto(idProjeto);
            var alternativas = _alternativaService.SelecionarAlternativas(projeto);
            var criterios = _criterioService.SelecionarCriterios(projeto);
            return _matrizDeDecisaoRepository.Select(projeto, alternativas, criterios);
        }

        public ItemMatrizDeDecisao SelecionarItem(int id)
            => _matrizDeDecisaoRepository.SelectItem(id);

        public bool ExisteMatriz(int idProjeto)
            => _matrizDeDecisaoRepository.Exists(idProjeto);

        public List<double> SomatorioDoQuadradoDosValoresPorCriterio(List<ItemMatrizDeDecisao> matriz)
        {
            var lista = new List<double>();
            var criteriosDaMatriz = ObterCriteriosDaMatriz(matriz);
            criteriosDaMatriz.ForEach(criterio =>
                ObterSomatorioDoQuadradoDosValoresDoCriterio(matriz, lista, criterio));
            return lista;
        }

        private void ObterSomatorioDoQuadradoDosValoresDoCriterio(List<ItemMatrizDeDecisao> matriz, List<double> lista, int criterio)
            => lista.Add(
                SomaDosValoresPorCriterios(matriz, criterio));

        private double SomaDosValoresPorCriterios(List<ItemMatrizDeDecisao> matriz, int criterio)
            => matriz
                .Where(x => x.Criterio.Id == criterio)
                .Select(x => Math.Pow(Convert.ToDouble(x.Valor), 2))
                .ToList()
                .Sum();

        private List<int> ObterCriteriosDaMatriz(List<ItemMatrizDeDecisao> matriz)
            => matriz
                .Select(x => x.Criterio.Id)
                .Distinct()
                .ToList();

        public List<ItemMatrizDeDecisao> NormalizarMatriz(int IdProjeto, List<Criterio> criterios)
        {
            var lista = new List<ItemMatrizDeDecisao>();
            _projetoService.SelecionarProjeto(IdProjeto);
            _alternativaService.SelecionarAlternativas(IdProjeto);
            var matriz = _matrizDeDecisaoRepository.Select(IdProjeto);
            var matrizClone = matriz.ConvertAll(x => new ItemMatrizDeDecisao()
            {
                Id = x.Id,
                Alternativa = x.Alternativa,
                Criterio = x.Criterio,
                Projeto = x.Projeto,
                Valor = x.Valor
            });
            var somatorio = SomatorioDoQuadradoDosValoresPorCriterio(matrizClone);
            ObterMatrizNormalizada(lista, matrizClone, somatorio, criterios);
            return lista;
        }

        private void ObterMatrizNormalizada(List<ItemMatrizDeDecisao> lista, List<ItemMatrizDeDecisao> matriz, List<double> somatorio, List<Criterio> criterios)
        {
            for (int i = 1; i < criterios.Count + 1; i++)
            {
                var itensComCriterio = ObterItensComCriterio(matriz, criterios[i - 1].Id);
                ObterValorDeCadaItemSobreRaizDoSomatorio(somatorio, i, itensComCriterio);
                lista.AddRange(itensComCriterio);
            }
        }

        private void ObterValorDeCadaItemSobreRaizDoSomatorio(List<double> somatorio, int i, List<ItemMatrizDeDecisao> itensComCriterio)
            => itensComCriterio
                .ForEach(x => x.Valor = ValorDoItemSobreRaizDoSomatorio(x, somatorio, (i - 1))
                .ToString());

        private List<ItemMatrizDeDecisao> ObterItensComCriterio(List<ItemMatrizDeDecisao> matriz, int i)
            => matriz.Where(x => x.Criterio.Id == i).ToList();

        private double ValorDoItemSobreRaizDoSomatorio(ItemMatrizDeDecisao x, List<double> somatorio, int i)
            => Convert.ToDouble(x.Valor) / Math.Sqrt(somatorio[i]);

        public List<ItemMatrizDeDecisao> MatrizNormalizadaPonderada(List<ItemMatrizDeDecisao> matrizNormalizada, List<Criterio> criterios)
        {
            var lista = new List<ItemMatrizDeDecisao>();
            var idProjeto = matrizNormalizada[0].Projeto.Id;
            var pesosDosCriterios = _criterioService.CriaVetorDePesosPonderados(idProjeto);
            ObterMatrizNormalizadaPonderada(matrizNormalizada, lista, pesosDosCriterios, criterios);
            return lista;
        }

        private void ObterMatrizNormalizadaPonderada(List<ItemMatrizDeDecisao> matrizNormalizada, List<ItemMatrizDeDecisao> lista, List<double> pesosDosCriterios, List<Criterio> criterios)
        {
            for (int i = 1; i < criterios.Count + 1; i++)
            {
                var itensComCriterio = ObterItensComCriterio(matrizNormalizada, criterios[i - 1].Id);
                PonderarCadaItemDaMatriz(pesosDosCriterios, i, itensComCriterio);
                lista.AddRange(itensComCriterio);
            }
        }

        private void PonderarCadaItemDaMatriz(List<double> pesosDosCriterios, int i, List<ItemMatrizDeDecisao> itensComCriterio)
            => itensComCriterio
                .ForEach
                    (x => x.Valor = (Convert.ToDouble(x.Valor) * pesosDosCriterios[i - 1])
                .ToString());

        public List<decimal> ObterVetorIdealPositivo(List<ItemMatrizDeDecisao> matrizNormalizadaPonderada, List<Criterio> criterios)
        {
            var lista = new List<decimal>();
            ObterValorIdealPositivoDeTodosOsCriterios(matrizNormalizadaPonderada, lista, criterios);
            return lista;
        }

        private void ObterValorIdealPositivoDeTodosOsCriterios(List<ItemMatrizDeDecisao> matrizNormalizadaPonderada, List<decimal> lista, List<Criterio> criteriosDaMatriz)
        {
            for (var i = 1; i < criteriosDaMatriz.Count + 1; i++)
            {
                var itensComCriterio = ObterItensComCriterio(matrizNormalizadaPonderada, criteriosDaMatriz[i - 1].Id);
                TipoDeCriterio tipoCriterio = ObterTipoDeCriterio(itensComCriterio);
                if (tipoCriterio.Equals(TipoDeCriterio.Beneficio))
                    AdicionarMaximo(lista, itensComCriterio);
                else
                    AdicionarValorMinimo(lista, itensComCriterio);
            }
        }

        private void AdicionarValorMinimo(List<decimal> lista, List<ItemMatrizDeDecisao> itensComCriterio)
            => lista.Add(itensComCriterio
                .Min(x => Convert.ToDecimal(x.Valor)));

        private void AdicionarMaximo(List<decimal> lista, List<ItemMatrizDeDecisao> itensComCriterio)
            => lista.Add(itensComCriterio
                    .Max(x => Convert.ToDecimal(x.Valor)));

        private TipoDeCriterio ObterTipoDeCriterio(List<ItemMatrizDeDecisao> itensComCriterio)
            => itensComCriterio
                .Select(x => x.Criterio.TipoDeCriterio)
                .FirstOrDefault();

        public List<decimal> ObterVetorIdealNegativo(List<ItemMatrizDeDecisao> matrizNormalizadaPonderada, List<Criterio> criterios)
        {
            var lista = new List<decimal>();
            ObterValorIdealNegativoParaCadaCriterio(matrizNormalizadaPonderada, lista, criterios);
            return lista;
        }

        private void ObterValorIdealNegativoParaCadaCriterio(List<ItemMatrizDeDecisao> matrizNormalizadaPonderada, List<decimal> lista, List<Criterio> criteriosDaMatriz)
        {
            for (var i = 1; i < criteriosDaMatriz.Count + 1; i++)
            {
                var itensComCriterio = ObterItensComCriterio(matrizNormalizadaPonderada, criteriosDaMatriz[i - 1].Id);
                var tipoCriterio = ObterTipoDeCriterio(itensComCriterio);
                if (tipoCriterio.Equals(TipoDeCriterio.Beneficio))
                    AdicionarValorMinimo(lista, itensComCriterio);
                else
                    AdicionarMaximo(lista, itensComCriterio);
            }
        }

        public List<double> ObterDistanciaEuclidianaDoVetor(List<ItemMatrizDeDecisao> matrizNormalizadaPonderada, List<decimal> vetorIdeal, int IdProjeto, List<Criterio> criterios, List<Alternativa> alternativas)
        {
            var lista = new List<double>();
            for (int i = 0; i < alternativas.Count; i++)
            {
                var itensComAlternativas = matrizNormalizadaPonderada.Where(x => x.Alternativa.Id == alternativas[i].Id).ToList();
                double somaDosQuadrados = 0;
                GerarQuadradoDasDiferenças(vetorIdeal, itensComAlternativas);
                somaDosQuadrados = SomarQuadradoDasDiferenças(itensComAlternativas);
                lista.Add(Math.Sqrt(somaDosQuadrados));
            }
            return lista;
        }

        private static double SomarQuadradoDasDiferenças(List<ItemMatrizDeDecisao> alternativas)
            => alternativas.Select(x => (Convert.ToDouble(x.Valor))).Sum();

        private int ObterNumeroDeAlternativasDistinstas(List<ItemMatrizDeDecisao> matrizNomalizadaPonderada)
            => matrizNomalizadaPonderada
                .Select(x => x.Alternativa.Id)
                .Distinct()
                .Count();

        private void GerarQuadradoDasDiferenças(List<decimal> vetorIdealPositivo, List<ItemMatrizDeDecisao> itensComAlternativas)
        {
            for (var j = 0; j < itensComAlternativas.Count; j++)
                itensComAlternativas[j].Valor = (Math.Pow
                    (Convert.ToDouble(itensComAlternativas[j].Valor)
                    - Convert.ToDouble(vetorIdealPositivo[j]), 2))
                    .ToString();
        }

        public void DeletarCriterios(int id)
        {
            var criterio = _criterioService.SelecionarCriterio(id);
            var itens = _matrizDeDecisaoRepository.Select(criterio);
            itens.ForEach(x => _matrizDeDecisaoRepository.Delete(x));
        }

        public void GerarResultado(List<Alternativa> alternativas, List<double> DistanciaEuclidianaDoVetorPositivo, List<double> DistanciaEuclidianaDoVetorNegativo)
        {
            if (DistanciaEuclidianaDoVetorNegativo.Count == DistanciaEuclidianaDoVetorPositivo.Count)
                for (int i = 0; i < alternativas.Count; i++)
                    alternativas[i].Score = CalcularScore(
                        DistanciaEuclidianaDoVetorPositivo, 
                        DistanciaEuclidianaDoVetorNegativo, 
                        i);
            else
                throw new Exception(string.Format("Vetores de distância ideal positiva e negativa possuem comprimentos diferentes"));
        }

        private double CalcularScore(List<double> DistanciaEuclidianaDoVetorPositivo, List<double> DistanciaEuclidianaDoVetorNegativo, int i)
            => DistanciaEuclidianaDoVetorNegativo[i]
                                    / (DistanciaEuclidianaDoVetorNegativo[i] + DistanciaEuclidianaDoVetorPositivo[i]);

        public int Selecionar(Projeto projeto, Alternativa alternativa, Criterio criterio)
            => _matrizDeDecisaoRepository.SelectItem(projeto, alternativa, criterio);

        public void Atualizar(Hashtable Matriz)
        {
            foreach (int item in Matriz.Keys)
                _matrizDeDecisaoRepository.Atualizar(item, Convert.ToDouble(Matriz[item]));
        }

        public List<Alternativa> ProcessarMatriz(int idProjeto)
        {
            var criterios = _criterioService.SelecionarCriterios(idProjeto);
            var alternativas = _alternativaService.SelecionarAlternativas(idProjeto);
            var matrizNormalizada = NormalizarMatriz(idProjeto, criterios);
            var matrizNormalizadaPonderada = MatrizNormalizadaPonderada(matrizNormalizada, criterios);
            var matrizNormalizadaPonderadaClone = matrizNormalizadaPonderada.ConvertAll(x => new ItemMatrizDeDecisao()
            {
                Id = x.Id,
                Alternativa = x.Alternativa,
                Criterio = x.Criterio,
                Projeto = x.Projeto,
                Valor = x.Valor
            });
            var vetorIdealPositivo = ObterVetorIdealPositivo(matrizNormalizadaPonderada, criterios);
            var vetorIdealNegativo = ObterVetorIdealNegativo(matrizNormalizadaPonderadaClone, criterios);
            var distanciaEuclidianaPositiva = ObterDistanciaEuclidianaDoVetor(matrizNormalizadaPonderada, vetorIdealPositivo, idProjeto, criterios, alternativas);
            var distanciaEuclidianaNegativa = ObterDistanciaEuclidianaDoVetor(matrizNormalizadaPonderadaClone, vetorIdealNegativo, idProjeto, criterios, alternativas);
            GerarResultado(alternativas, distanciaEuclidianaPositiva, distanciaEuclidianaNegativa);
            alternativas.ForEach(alternativa => _alternativaService.Atualizar(alternativa));
            return alternativas;
        }
    }

}