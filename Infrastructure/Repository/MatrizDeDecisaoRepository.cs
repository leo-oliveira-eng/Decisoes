using System.Collections.Generic;
using System.Linq;
using Core;
using Core.IRepository;

namespace Repository
{
    public class MatrizDeDecisaoRepository : IMatrizDeDecisaoRepository
    {
        private DecisoesContext _context;

        public dynamic Context { get => _context; }

        public MatrizDeDecisaoRepository(DecisoesContext context)
        {
            _context = context;
        }

        public void Add(ItemMatrizDeDecisao Item) {
            _context.MatrizDeDecisoes.Add(Item);
            _context.SaveChanges();
        }

        public void Update(ItemMatrizDeDecisao item) {
            _context.MatrizDeDecisoes.Update(item);
            _context.SaveChanges();
        }

        public void Delete(ItemMatrizDeDecisao matrizDeDecisao)
        {
            _context.MatrizDeDecisoes.Remove(matrizDeDecisao);
            _context.SaveChanges();
        }

        public ItemMatrizDeDecisao SelectItem(int id)
            => _context.MatrizDeDecisoes.Where(x => x.Id == id).FirstOrDefault();

        public List<ItemMatrizDeDecisao> Select(int idProjeto)
            => _context.MatrizDeDecisoes
                .Where(m => m.Projeto.Id == idProjeto)
                .ToList();

        public List<ItemMatrizDeDecisao> Select(Projeto projeto, List<Alternativa> Alternativas, List<Criterio> Criterios)
            => _context.MatrizDeDecisoes
                    .Where(x => 
                        x.Projeto == projeto
                        && Alternativas.Any(a => a.Projeto == projeto)
                        && Criterios.Any(c => c.Projeto == projeto)
                    )
                    .ToList();

        public bool Exists(int idProjeto)
            => _context.MatrizDeDecisoes
                .Any(x => x.Projeto.Id == idProjeto);

        public List<ItemMatrizDeDecisao> Select(int idProjeto, int idAlternativa)
            => _context.MatrizDeDecisoes
                .Where(m => 
                    m.Projeto.Id.Equals(idProjeto)
                    && m.Alternativa.Id.Equals(idAlternativa))
                .ToList();

        public List<ItemMatrizDeDecisao> Select(Alternativa alternativa)
            => _context.MatrizDeDecisoes
                .Where(x => x.Alternativa == alternativa)
                .ToList();

        public List<ItemMatrizDeDecisao> Select(Criterio criterio)
            => _context.MatrizDeDecisoes
                .Where(x => x.Criterio == criterio)
                .ToList();

        public int SelectItem(Projeto projeto, Alternativa alternativa, Criterio criterio)
            => _context.MatrizDeDecisoes
                .Where(x =>
                    x.Projeto == projeto
                    && x.Alternativa == alternativa
                    && x.Criterio == criterio
                )
                .Select(x => x.Id)
                .FirstOrDefault();

        public void Atualizar(int id, double valor)
        {
            var item = _context.MatrizDeDecisoes.Where(x => x.Id == id).FirstOrDefault();
            item.Valor = valor.ToString();
            _context.SaveChanges();
        }
    }

}