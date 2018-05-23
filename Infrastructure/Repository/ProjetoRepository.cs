using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Core.IRepository;

namespace Repository
{
    public class ProjetoRepository : IProjetoRepository
    {
        private DecisoesContext _context;

        public ProjetoRepository(DecisoesContext context)
        {
            _context = context;
        }

        public void Add(string nome, string descricao)
        {
            _context.Projetos.Add(new Projeto() {
                Nome = nome,
                Descricao = descricao,
                DataCadastro = DateTime.Now
            });
            _context.SaveChanges();
        }

        public void Delete(Projeto projeto)
        {
            _context.Projetos.Remove(projeto);
            _context.SaveChanges();
        }

        public List<Projeto> Select()
            => _context.Projetos
                .OrderByDescending(p => p.DataCadastro)
                .ToList();

        public Projeto Select(int Id)
            => _context.Projetos
                .Where(p => p.Id.Equals(Id))
                .FirstOrDefault();

        public List<Projeto> Select(string texto)
            => _context.Projetos
                .Where(p => p.Nome.Contains(texto))
                .OrderByDescending(p=> p.DataCadastro)
                .ToList();

        public bool Exists(string nome)
            => _context.Projetos
                .Where(p => p.Nome.Equals(nome))
                .Any();

        public void Update(Projeto Projeto)
        {
            _context.Projetos.Update(Projeto);
            _context.SaveChanges();
        }
    }
}