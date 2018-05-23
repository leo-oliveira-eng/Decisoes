using System.Collections.Generic;
using System.Linq;
using Core;
using Core.IRepository;

namespace Repository
{
    public class AlternativaRepository : IAlternativaRepository
    {
        private DecisoesContext _context;

        public AlternativaRepository(DecisoesContext context)
        {
            _context = context;
        }

        public void Add(Projeto projeto, string nome)
        {
            _context.Alternativas.Add(new Alternativa() {
                Nome = nome,
                Projeto = projeto
            });
            _context.SaveChanges();
        }

        public void Delete(Alternativa alternativa)
        {
            _context.Alternativas.Remove(alternativa);
            _context.SaveChanges();
        }

        public List<Alternativa> Select(int idProjeto)
            => _context.Alternativas
                .Where(a => a.Projeto.Id == idProjeto)
                .OrderBy(a => a.Nome)
                .ToList();

        public Alternativa SelectAlternativa(int idAlternativa)
            => _context.Alternativas
                .Where(a => a.Id.Equals(idAlternativa))
                .FirstOrDefault();

        public bool Exists(string nome, Projeto projeto)
            => _context.Alternativas
                .Any(x => x.Nome == nome && x.Projeto == projeto);

        public void Update(Alternativa alternativa)
        {
            _context.Alternativas.Update(alternativa);
            _context.SaveChanges();
        }

        public int Quantidade(int id)
            => _context.Alternativas
                .Where(a => a.Projeto.Id == id)
                .ToList()
                .Count;

        public void Excluir(Alternativa alternativa)
        {
            _context.Alternativas.Remove(alternativa);
            _context.SaveChanges();
        }

        public List<Alternativa> Select(Projeto projeto)
            => _context.Alternativas
                .Where(a => a.Projeto == projeto)
                .ToList();
    }

}