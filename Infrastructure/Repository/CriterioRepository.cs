using System.Collections.Generic;
using System.Linq;
using Core;
using Core.Enuns;
using Core.IRepository;

namespace Repository
{
    public class CriterioRepository : ICriterioRepository
    {
        private DecisoesContext _context;

        public CriterioRepository(DecisoesContext context)
        {
            _context = context;
        }

        public void Add(Projeto projeto, TipoDeDados tipoDeDados, TipoDeCriterio tipoDeCriterio, decimal peso, string nome)
        {
            _context.Criterios.Add(new Criterio() {
                Projeto = projeto,
                TipoDeDados = tipoDeDados,
                TipoDeCriterio = tipoDeCriterio,
                Peso = peso,
                Nome = nome
            });
            _context.SaveChanges();
        }

        public void Delete(Criterio criterio)
        {
            _context.Criterios.Remove(criterio);
            _context.SaveChanges();
        }

        public bool Existe(string nome, Projeto projeto)
            => _context.Criterios
                .Any(x => 
                    x.Nome == nome
                    && x.Projeto == projeto
                );

        public int Quantidade(int id)
            => _context.Criterios
                .Where(c => c.Projeto.Id == id)
                .ToList()
                .Count;

        public List<Criterio> Select(int idProjeto)
            => _context.Criterios
                .Where(c => c.Projeto.Id == idProjeto)
                .OrderByDescending(c => c.Id)
                .ToList();

        public List<Criterio> Select(Projeto projeto)
            => _context.Criterios
                .Where(c => c.Projeto == projeto)
                .ToList();

        public Criterio SelectItem(int id)
            => _context.Criterios
                .Where(c => c.Id.Equals(id))
                .FirstOrDefault();

        public void Update(Criterio criterio)
        {
            _context.Criterios.Update(criterio);
            _context.SaveChanges();
        }
    }

}