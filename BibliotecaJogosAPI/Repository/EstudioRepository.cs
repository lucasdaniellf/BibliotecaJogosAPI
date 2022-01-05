using BibliotecaJogosAPI.Models;
using BibliotecaJogosAPI.Repository.Context;
using BibliotecaJogosAPI.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaJogosAPI.Repository
{
    public class EstudioRepository : IEstudioRepository
    {
        public bool DeleteEstudio(BibliotecaDBContext _context, int id)
        {
            var estudio = SelectEstudioPorID(_context, id);
            if (estudio != null)
            {
                _context.Estudio.Remove(estudio);
                return SaveChanges(_context);
            }
            return false;
        }

        public bool InsertEstudio(BibliotecaDBContext _context, Estudio estudio)
        {
            _context.Estudio.Add(estudio);
            return SaveChanges(_context);
        }

        public bool SaveChanges(BibliotecaDBContext _context)
        {
            return _context.SaveChanges() > 0;
        }

        public Estudio? SelectEstudioPorID(BibliotecaDBContext _context, int id)
        {
            return _context.Estudio
                .Include(x => x.Jogos)
                .ThenInclude(x => x.Generos)
                .FirstOrDefault(x => x.Id == id) ;
        }

        public IEnumerable<Estudio> SelectEstudioPorNome(BibliotecaDBContext _context, string nome)
        {
            var estudios = from estudio in _context.Estudio.Include(x => x.Jogos).ThenInclude(x => x.Generos)
                           where estudio.Nome.ToLower().Contains(nome.ToLower())
                           select estudio;
            return estudios;
        }

        public IEnumerable<Estudio> SelectTodosEstudios(BibliotecaDBContext _context)
        {
            return _context.Estudio
                .Include(x => x.Jogos)
                .ThenInclude(x => x.Generos)
                .ToList();
        }

        public bool UpdateEstudio(BibliotecaDBContext _context, Estudio estudio)
        {
            return SaveChanges(_context);
        }
    }
}
